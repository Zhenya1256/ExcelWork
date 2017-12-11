using System.Collections.Generic;
using System.Linq;
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

namespace WorkWithExcel.Model.Impl
{
    public class ParserSectionPage : IParser
    {
        private readonly IReadExcelData _readExcelData;
        private readonly IDataNormalization _dataNormalization;

        public ParserSectionPage()
        {
            _readExcelData = new ReadExcelData();
            _dataNormalization = new DataNormalization();
        }

        public IDataResult<IRowItem> RowParser
            (ExcelWorksheet excelWorksheet, int row, ExcelConfiguration excelConfiguration)
        {
            IDataResult<IRowItem> dataResult =
                new DataResult<IRowItem>() { Success = false };
            IRowItem rowItem = new RowItem();

            List<IColumnItem> columnItems = new List<IColumnItem>();
            int end  = excelWorksheet.Dimension.Columns;

            for (int j = excelWorksheet.Dimension.Start.Column;
                j <=end;
                j++)
            {
                IExcelWorksheetEntity tmpEntity = new ExcelWorksheetEntity();
                tmpEntity.CellNo = j;
                tmpEntity.RowNo = row;
                tmpEntity.ExcelWorksheet = excelWorksheet;

                IDataResult<IColumnItem> getDataResult =
                    ColumnParser(tmpEntity, excelConfiguration);
                dataResult.Message += getDataResult.Message;

                if (getDataResult.Success)
                {
                    columnItems.Add(getDataResult.Data);
                }
            }

            rowItem.ColumnItems = columnItems;
            dataResult.Data = rowItem;
            dataResult.Success = true;

            return dataResult;
        }

        public IDataResult<IColumnItem> ColumnParser
            (IExcelWorksheetEntity worksheetEntity, ExcelConfiguration excelConfiguration)
        {
            IDataResult<IColumnItem> dataResult =
                new DataResult<IColumnItem>() { Success = false };

            IColumnItem columnItem = new ColumnItem();
            int column = worksheetEntity.CellNo;
            IDataResult<string> resultValue = _readExcelData.GetValue(worksheetEntity);
            int nomertitle = excelConfiguration.DataRowIndex.Title;

            if (!resultValue.Success)
            {
                dataResult.Message = resultValue.Message;
            }

            IExcelWorksheetEntity tmpExcel = new ExcelWorksheetEntity();
            tmpExcel.RowNo = nomertitle;
            tmpExcel.ExcelWorksheet = worksheetEntity.ExcelWorksheet;
            tmpExcel.CellNo = column;

            IDataResult<string> resultNameTitle = _readExcelData.GetValue(tmpExcel);

            if (!resultNameTitle.Success)
            {
                dataResult.Message = MessageHolder.GetErrorMessage(MessageType.NotNameTitle);
            }

            string nameTitle = resultNameTitle.Data;
            nameTitle = _dataNormalization.NormalizeString(nameTitle).Data;

            string titleConfig = excelConfiguration.DataColumn.Datas?
                .FirstOrDefault(p=>p.ColumnType==(int)ColumnType.Section)?.Name;
            titleConfig = _dataNormalization.NormalizeString(titleConfig).Data;
            ITranslationEntity translationEntity = new TranslationEntity();
            translationEntity.Value = resultValue.Data;
            string mainLanguage = excelConfiguration.NameColumnSection.MainLanguage;
            mainLanguage = _dataNormalization.NormalizeString(mainLanguage).Data;
            string excelLanguage = null;

            if (titleConfig != null)
            {
                excelLanguage = nameTitle.Replace(titleConfig, string.Empty);
            }


            if (nameTitle.Equals(titleConfig) || (excelLanguage!=null && excelLanguage.Equals(mainLanguage)))
            {
                columnItem.ColumnType = ColumnType.Section;
                translationEntity.Language = 
                    LanguageHolder.GetISOCodes(mainLanguage, _dataNormalization);

                if (!resultValue.Success)
                {
                    dataResult.Message = resultValue.Message;

                    return dataResult;
                }

                columnItem.BaseEntity = translationEntity;
            }
            else
            {
                columnItem.ColumnType = ColumnType.SectionTransfer;

                translationEntity.Language =
                    LanguageHolder.GetISOCodes(excelLanguage, _dataNormalization);

                if (!resultValue.Success)
                {
                    dataResult.Message = resultValue.Message;

                    return dataResult;
                }

                columnItem.BaseEntity = translationEntity;
            }

            dataResult.Data = columnItem;
            dataResult.Success = true;

            return dataResult;
        }

        public IDataResult<List<IColumnItem>> GetCulumnTitleItem
            (ExcelWorksheet sheet, ExcelConfiguration excelConfiguration)
        {
            throw new System.NotImplementedException();
        }

        public int RowCount { get; set; }
    }
}
