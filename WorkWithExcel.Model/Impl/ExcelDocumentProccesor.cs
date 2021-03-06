﻿using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using WorkWithExcel.Abstract.BL;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.HelpEntity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.Model.Common;
using WorkWithExcel.Model.Entity;
using WorkWithExcel.Model.Entity.HelperEntity;
using System.Linq;
using System.Threading.Tasks;

namespace WorkWithExcel.Model.Impl
{
    public class ExcelDocumentProccesor : IExcelDocumentProccesor
    {
        private readonly IValidata _validata;
        private readonly IGetExcelSheetCongSection _excelSheetCongSection;
        private readonly IParser _parser;
        private readonly IReadExcelData _readExcelData;
        private readonly IDataNormalization _dataNormalization;
        private ExcelConfiguration _excelConfiguration;
        private readonly IParser _parserSection;

        public ExcelDocumentProccesor()
        {
            _validata = new Validata();
            _parser = new Parser();
            _readExcelData = new ReadExcelData();
            _dataNormalization = new DataNormalization();
            _excelSheetCongSection = new ExcelSheetCongSection();
            _parserSection = new ParserSectionPage();
            _excelConfiguration = ConfigurationHolder.ApiConfiguration;
        }

        public IDataResult<IDataSheetResulHolder> Processor(string path)
        {
            IDataResult<IDataSheetResulHolder> dataResult =
                new DataResult<IDataSheetResulHolder>() { Success = false };
            IDataSheetResulHolder dataSheetResulHolder = new DataSheetResulHolder();
            List<IDataSheet> dataSheets = new List<IDataSheet>();
            dataSheetResulHolder.ExcelDocumentType = ExcelDocumentType.WithoutIndexPage;
            IResult result = _validata.ValidataExcelPath(path);

            if (!result.Success)
            {
                dataResult.Message = result.Message;
                dataResult.Success = result.Success;

                return dataResult;
            }

            dataSheetResulHolder.NameExcel = Path.GetFileName(path);

            using (var file = File.Open(path, FileMode.Open))
            {
                using (var xls = new ExcelPackage(file))
                {
                    ExcelWorksheets tempWorksheets = xls.Workbook.Worksheets;

                    IResult excelDocResult = _validata.ValidateExcelPages(tempWorksheets);

                    if (!excelDocResult.Success)
                    {
                        dataResult.Message += excelDocResult.Message;
                        dataResult.Success = false;

                        return dataResult;
                    }

                    foreach (var worksheet in tempWorksheets)
                    {
                        using (var sheet = worksheet)
                        {
                            _parser.RowCount = sheet.Dimension.End.Row;
                            _parserSection.RowCount = sheet.Dimension.End.Row;
                            string sheetName = sheet.Name;

                            IResult isDataResult = _validata.VolidateExcel(sheet);
                            bool success = true;
                            dataResult.Message += MessageHolder.
                                GetErrorMessage(MessageType.NamePage) + sheetName +
                                MessageHolder.GetErrorMessage(MessageType.NewLine);

                            if (!isDataResult.Success)
                            {
                                dataResult.Message += isDataResult.Message;
                                success = false;
                            }

                            string tmpSheetName = _dataNormalization.NormalizeString(sheetName).Data;
                            string configName = _excelConfiguration.NamePage?.Section;
                            configName = _dataNormalization.NormalizeString(configName).Data;
                            IDataResult<IDataSheet> resultDataSheet = new DataResult<IDataSheet>();

                            if (tmpSheetName.Equals(configName))
                            {
                                dataSheetResulHolder.ExcelDocumentType = ExcelDocumentType.IndexPage;
                                IDataResult<Dictionary<ITranslationEntity, List<ITranslationEntity>>>
                                    tempSectionTranslation = GetSectionTranslation(sheet);

                                if (tempSectionTranslation.Success)
                                {
                                    dataSheetResulHolder.IndexTranslates = tempSectionTranslation.Data;
                                    dataResult.Message += tempSectionTranslation.Message;
                                }
                                else
                                {
                                    dataResult.Message += tempSectionTranslation.Message;
                                }
                            }
                            else
                            {
                                IResult resultConfig = _excelSheetCongSection.GetExcelConfig(sheet);

                                if (!resultConfig.Success)
                                {
                                    IDataResult<ExcelConfiguration> excelDataResult =
                                        _excelSheetCongSection.GeneratExcelConfig(sheet);

                                    if (!excelDataResult.Success)
                                    {
                                        dataResult.Message += excelDataResult.Message +
                                            MessageHolder.GetErrorMessage(MessageType.NameSheet) + sheetName;
                                        success = false;
                                    }

                                    _excelConfiguration = excelDataResult.Data;
                                    dataSheetResulHolder.ExcelConfiguration = excelDataResult.Data;
                                }

                                resultDataSheet = HelpProcessor(sheet, success);
                                ProcessErrors(resultDataSheet.Data.RowItemErrors, 
                                    sheetName, 
                                    Path.GetFileNameWithoutExtension(path)).Wait();
                                dataSheets.Add(resultDataSheet.Data);
                            }

                            dataResult.Message += resultDataSheet.Message;
                            dataResult.Message += MessageHolder.GetErrorMessage(MessageType.NewLine); ;
                        }
                    }
                }
            }

            dataResult.Success = true;

            dataSheetResulHolder.DataSheets = dataSheets;
            dataResult.Data = dataSheetResulHolder;

            return dataResult;
        }

        private IDataResult<IDataSheet> HelpProcessor
            (ExcelWorksheet sheet, bool success)
        {
            IDataResult<IDataSheet> dataResult =
                new DataResult<IDataSheet>() { Success = false };

            IDataSheet dataSheet = new DataSheet();
            List<IRowItem> rowItems = new List<IRowItem>();
            List<IRowItemError> errorRowItems = new List<IRowItemError>();
            dataSheet.NameTable = sheet.Name;

            if (success)
            {
                IDataResult<List<IColumnItem>> columnResult = _parser.GetCulumnTitleItem(sheet, _excelConfiguration);

                if (!columnResult.Success)
                {
                    dataResult.Message += columnResult.Message;
                }
                else
                {
                    dataSheet.ColumnTitleItems = columnResult.Data;
                    int end = sheet.Dimension.End.Row;

                    for (int j = sheet.Dimension.Start.Row + 1; j <= end; j++)
                    {
                        IDataResult<IRowItem> rowParserResult =
                            _parser.RowParser(sheet, j, _excelConfiguration);


                        if (!rowParserResult.Success)
                        {    
                            IRowItemError error = (IRowItemError)rowParserResult.Data;

                            if (error.ListColums.All(p => p.Data.BaseEntity.Value == null)
                                && error.ColumnItems.All(p => p.BaseEntity.Value == null))
                            {
                                break;
                            }

                            dataResult.Message += rowParserResult.Message;
                            error.RowNmomer = j;
                            errorRowItems.Add(error);
                        }

                        else
                        {
                            rowItems.Add(rowParserResult.Data);
                        }
                    }
                }
            }

            dataSheet.RowItemErrors = errorRowItems;
            dataSheet.RowItems = rowItems;
            dataResult.Data = dataSheet;
            dataResult.Success = true;

            return dataResult;
        }

        private async Task ProcessErrors (List<IRowItemError> itemErrors,string pageName,string documentName)
        {
            if (!Directory.Exists(documentName))
            {
                Directory.CreateDirectory(documentName);
            }

            var path = Path.Combine(new string[] { documentName, pageName });

            using (FileStream fileStream = File.Create(path.ToString()))
            {
                using (StreamWriter sw = new StreamWriter(fileStream))
                {
                    await sw.WriteLineAsync($"Total errors count on page {pageName} is {itemErrors.Sum(p => p.ListColums.Count())}");
                    await sw.WriteLineAsync($"Total error rows count on page {pageName} is {itemErrors.Count()}");
                    await sw.WriteLineAsync(sw.NewLine);
                    await sw.WriteLineAsync(sw.NewLine);
                    await sw.WriteLineAsync(sw.NewLine);

                    foreach (var row in itemErrors)
                    {

                        await sw.WriteLineAsync($"Error at row number {row.RowNmomer}");
                        await sw.WriteLineAsync(sw.NewLine);
                        await sw.WriteLineAsync($"Total count in row {row.ListColums.Count}");
                        await sw.WriteLineAsync(sw.NewLine);

                        foreach (var colum in row.ListColums)
                        {
                            await sw.WriteLineAsync($"At colum letter : {colum.Message}");
                            await sw.WriteLineAsync(sw.NewLine);
                            await sw.WriteLineAsync($"Field {colum.Data.ColumnType.ToString()} value not exist");
                            await sw.WriteLineAsync(sw.NewLine);
                        }

                        await sw.WriteLineAsync(sw.NewLine);
                        await sw.WriteLineAsync(sw.NewLine);
                    }

                    await sw.FlushAsync();
            }
            }
        }

        private IDataResult<Dictionary<ITranslationEntity, List<ITranslationEntity>>>
            GetSectionTranslation(ExcelWorksheet sheet)
        {
            IDataResult<Dictionary<ITranslationEntity, List<ITranslationEntity>>> dataResult =
                new DataResult<Dictionary<ITranslationEntity, List<ITranslationEntity>>>();
            List<IRowItem> rowItems = new List<IRowItem>();

            for (int j = sheet.Dimension.Start.Row + 1; j <= sheet.Dimension.End.Row; j++)
            {
                IDataResult<IRowItem> rowParserResult =
                    _parserSection.RowParser(sheet, j, _excelConfiguration);

                if (!string.IsNullOrEmpty(rowParserResult.Message))
                {
                    IRowItemError error = new RowItemError();
                    error.ColumnItems = rowParserResult.Data.ColumnItems;
                    dataResult.Message += MessageHolder.GetErrorMessage
                        (MessageType.NotPageSection);
                    dataResult.Message += rowParserResult.Message +
                        MessageHolder.GetErrorMessage(MessageType.NewLine);

                    error.RowNmomer = j;
                    dataResult = _dataNormalization.NormaliseTransliteSection(rowItems);
                    //dataResult.Success = false;

                    return dataResult;
                }

                rowItems.Add(rowParserResult.Data);
            }

            IDataResult<Dictionary<ITranslationEntity, List<ITranslationEntity>>> sectiomNormResult =
                _dataNormalization.NormaliseTransliteSection(rowItems);

            if (!sectiomNormResult.Success)
            {
                dataResult.Message += MessageHolder.GetErrorMessage
                    (MessageType.NotPageSection);
                dataResult.Message += sectiomNormResult.Message;
                dataResult.Success = false;

                return dataResult;
            }

            dataResult.Data = sectiomNormResult.Data;
            dataResult.Success = true;

            return dataResult;
        }
    }
}
