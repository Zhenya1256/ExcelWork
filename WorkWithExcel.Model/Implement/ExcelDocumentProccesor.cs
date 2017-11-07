using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;
using WorkWithExcel.Abstract.Entity;
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
        private ExcelConfiguration _excelConfiguration;

        public ExcelDocumentProccesor()
        {
            _validata = new Validata();
            _parser = new Parser();
            _excelSheetCongSection = new GetExcelSheetCongSection();
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

                            IResult isDataResult = _validata.VolidateExcel(sheet);

                            if (!isDataResult.Success)
                            {
                                dataResult.Message = isDataResult.Message;

                                return dataResult;
                            }

                            IResult resultConfig = _excelSheetCongSection.GetExcelConfig(sheet);

                            if (!resultConfig.Success)
                            {
                                IDataResult<ExcelConfiguration> excelDataResult =
                                    _excelSheetCongSection.GenerationExcelConfig(sheet);

                                if (!excelDataResult.Success)
                                {
                                    dataResult.Message = excelDataResult.Message;

                                    return dataResult;
                                }

                                _excelConfiguration = excelDataResult.Data;
                            }

                            IDataResult<IDataSheet> resultDataSheet = HelpProcessor(sheet);

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

        private IDataResult<IDataSheet> HelpProcessor(ExcelWorksheet sheet)
        {
            IDataResult<IDataSheet> dataResult =
                new DataResult<IDataSheet>() {Success = false};
            
            IDataSheet dataSheet = new DataSheet();
            List<IRowItem> rowItems = new List<IRowItem>();
            List<IRowItemError> errorRowItems = new List<IRowItemError>();
            IBaseExelEntety baseExelEntety = new BaseExelEntety();
            baseExelEntety.SectionTranslates = new Dictionary<string, List<ITranslationEntity>>();
            baseExelEntety.WordTranslates = new Dictionary<string, List<ITranslationEntity>>();

            for (int j = sheet.Dimension.Start.Row + 1; j <= sheet.Dimension.End.Row; j++)
            {
                IDataResult<IRowItem> rowParserResult = _parser.RowParser(sheet, j, _excelConfiguration);

                if (rowParserResult.Message != null)
                {
                    IRowItemError error = new RowItemError();
                    error.ColumnItems = rowParserResult.Data.ColumnItems;
                    errorRowItems.Add(error);
                }
                else
                {
                    rowItems.Add(rowParserResult.Data);
                }
            }

            dataSheet.NameTable = sheet.Name;
            dataSheet.BaseExelEntety = baseExelEntety;
            dataSheet.RowItemErrors = errorRowItems;
            dataSheet.RowItems = rowItems;
            dataResult.Data = dataSheet;
            dataResult.Success = true;

            return dataResult;
        }
    }
}
