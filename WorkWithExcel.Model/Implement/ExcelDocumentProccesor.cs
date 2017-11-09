using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.Model.Common;
using WorkWithExcel.Model.Entity;

namespace WorkWithExcel.Model.Implement
{
    public class ExcelDocumentProccesor : IExcelDocumentProccesor
    {
        private readonly IValidata _validata;
        private readonly IGetExcelSheetCongSection _excelSheetCongSection;
        private readonly IParser _parser;
        private readonly IDataNormalization _dataNormalization;
        private ExcelConfiguration _excelConfiguration;
        private readonly IParser _parserSection;

        public ExcelDocumentProccesor()
        {
            _validata = new Validata();
            _parser = new Parser();
            _dataNormalization = new DataNormalization();
            _excelSheetCongSection = new GetExcelSheetCongSection();
            _parserSection = new ParserSectionPage();
            _excelConfiguration = ConfigurationHolder.ApiConfiguration;
        }

        public IDataResult<IDataSheetResulHolder> Processor(string path)
        {
            IDataResult<IDataSheetResulHolder> dataResult =
                new DataResult<IDataSheetResulHolder>() { Success = false };
            IDataSheetResulHolder dataSheetResulHolder = new DataSheetResulHolder();
            List<IDataSheet> dataSheets = new List<IDataSheet>();

            IResult result = _validata.ValidataExcelPath(path);

            if (!result.Success)
            {
                dataResult.Message = result.Message;
                dataResult.Success = result.Success;

                return dataResult;
            }

            using (var file = File.Open(path, FileMode.Open))
            {
                using (var xls = new ExcelPackage(file))
                {
                    foreach (var worksheet in xls.Workbook.Worksheets)
                    {
                        using (var sheet = worksheet)
                        {
                            _parser.RowCount = sheet.Dimension.End.Row;
                            _parserSection.RowCount = sheet.Dimension.End.Row;
                            string sheetName = sheet.Name;

                            IResult isDataResult = _validata.VolidateExcel(sheet);
                            bool success = true;
                            dataResult.Message += MessageHolder.
                                GetErrorMessage(MessageType.NamePage) + sheetName + "\n";

                            if (!isDataResult.Success)
                            {
                                dataResult.Message += isDataResult.Message;
                                success = false;
                            }

                            string tmpSheetName = _dataNormalization.NormalizeString(sheetName).Data;
                            string configName = _excelConfiguration.NamePage.Section;
                            configName = _dataNormalization.NormalizeString(configName).Data;
                            IDataResult<IDataSheet> resultDataSheet;

                            if (tmpSheetName.Equals(configName))
                            {
                                resultDataSheet = HelpProcessor
                                    (sheet, success, ExcelDocumentType.Section);
                            }
                            else
                            {
                                IResult resultConfig = _excelSheetCongSection.GetExcelConfig(sheet);
                   
                                if (!resultConfig.Success)
                                {
                                    IDataResult<ExcelConfiguration> excelDataResult =
                                        _excelSheetCongSection.GenerationExcelConfig(sheet);

                                    if (!excelDataResult.Success)
                                    {
                                        dataResult.Message += excelDataResult.Message + "імя: " + sheetName;
                                        success = false;
                                    }

                                    _excelConfiguration = excelDataResult.Data;
                                }

                                resultDataSheet = HelpProcessor(sheet, success, ExcelDocumentType.None);
                            }

                            dataResult.Message += resultDataSheet.Message;
                            dataResult.Message += "\n";

                            dataSheets.Add(resultDataSheet.Data);
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
            (ExcelWorksheet sheet, bool success, ExcelDocumentType type)
        {
            IDataResult<IDataSheet> dataResult =
                new DataResult<IDataSheet>() { Success = false };

            IDataSheet dataSheet = new DataSheet();
            List<IRowItem> rowItems = new List<IRowItem>();
            List<IRowItemError> errorRowItems = new List<IRowItemError>();
            IBaseExelEntety baseExelEntety = new BaseExelEntety();
            baseExelEntety.SectionTranslates = new Dictionary<ITranslationEntity, List<ITranslationEntity>>();
            baseExelEntety.WordTranslates = new Dictionary<IDataExcelEntity, List<ITranslationEntity>>();
            dataSheet.NameTable = sheet.Name;

            if (success)
            {
                for (int j = sheet.Dimension.Start.Row + 1; j <= sheet.Dimension.End.Row; j++)
                {
                    IDataResult<IRowItem> rowParserResult = new DataResult<IRowItem>();
                    switch (type)
                    {
                        case ExcelDocumentType.None:
                            rowParserResult = _parser.RowParser(sheet, j, _excelConfiguration);
                            break;
                        case ExcelDocumentType.Section:
                            rowParserResult = _parserSection.RowParser(sheet, j, _excelConfiguration);
                            break;
                    }

                    if (rowParserResult.Message != null)
                    {
                        IRowItemError error = new RowItemError();
                        error.ColumnItems = rowParserResult.Data.ColumnItems;
                        dataResult.Message += rowParserResult.Message;
                        error.RowNmomer = j;
                        errorRowItems.Add(error);
                    }
                    //  else
                    //   {
                    rowItems.Add(rowParserResult.Data);
                    //  }
                }

                IDataResult<Dictionary<ITranslationEntity, List<ITranslationEntity>>> normalizeSectionResult =
                    _dataNormalization.NormaliseTransliteSection(rowItems);

                if (normalizeSectionResult.Success)
                {
                    baseExelEntety.SectionTranslates = normalizeSectionResult.Data;
                }

                IDataResult<Dictionary<IDataExcelEntity, List<ITranslationEntity>>> normalizeWprdResult =
                    _dataNormalization.NormaliseTransliteWord(rowItems);

                if (normalizeWprdResult.Success)
                {
                    baseExelEntety.WordTranslates = normalizeWprdResult.Data;
                }
            }

            dataSheet.BaseExelEntety = baseExelEntety;
            dataSheet.RowItemErrors = errorRowItems;
            dataSheet.RowItems = rowItems;
            dataResult.Data = dataSheet;
            dataResult.Success = true;

            return dataResult;
        }

    }
}
