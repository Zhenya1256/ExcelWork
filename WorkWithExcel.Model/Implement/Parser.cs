using System.Collections.Generic;
using System.Drawing;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.HelpEntity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.Model.Common;
using WorkWithExcel.Model.Entity;
using WorkWithExcel.Model.Entity.HelperEntity;

namespace WorkWithExcel.Model.Implement
{
    public class Parser : IParser
    {
        private readonly IReadExcelData _readExcelData;
        private readonly IDataNormalization _dataNormalization;

        public Parser()
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
            int start = excelWorksheet.Dimension.Start.Column;
            int end = excelWorksheet.Dimension.End.Column;

            for (int j = start; j <= end; j++)
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
            (IExcelWorksheetEntity excelWorksheet, ExcelConfiguration excelConfiguration)
        {
            IDataResult<IColumnItem> dataResult =
                new DataResult<IColumnItem>() { Success = false };

            IColumnItem columnItem = new ColumnItem();
            int column = excelWorksheet.CellNo;
            IDataResult<string> resultValue = _readExcelData.GetValue(excelWorksheet);
            int nomertitle = excelConfiguration.DataRowIndex.Title;

            if (!resultValue.Success)
            {
                dataResult.Message = resultValue.Message;
            }

            IExcelWorksheetEntity tmpExcel = new ExcelWorksheetEntity();
            tmpExcel.RowNo = nomertitle;
            tmpExcel.ExcelWorksheet = excelWorksheet.ExcelWorksheet;
            tmpExcel.CellNo = column;
            string nameTitle = _readExcelData.GetValue(tmpExcel).Data;
            string configNameSection = excelConfiguration.DataColumn.Section.Name;
            configNameSection = _dataNormalization.NormalizeString(configNameSection).Data;
            columnItem.BaseEntity = new BaseEntity();

            if (column == excelConfiguration.DataColumn.Picture.Nomer)
            {
                columnItem.ColumnType = ColumnType.Picture;
                IDataResult<string> colorNameResult =
                    _readExcelData.GetColorValue(excelWorksheet);
                IExcelColor excelColor = new ExcelColor();
                if (colorNameResult.Success)
                {
                    Color color = ColorTranslator.FromHtml("#" + colorNameResult.Data);
                    excelColor.R = color.R;
                    excelColor.G = color.G;
                    excelColor.B = color.B;
                }

                columnItem.BaseEntity = excelColor;
            }
            else if (column == excelConfiguration.DataColumn.Index.Nomer)
            {
                columnItem.ColumnType = ColumnType.Index;
                columnItem.BaseEntity.Value = resultValue.Data;
            }

            else if (column == excelConfiguration.DataColumn.Page.Nomer)
            {
                columnItem.ColumnType = ColumnType.Page;
                columnItem.BaseEntity.Value = resultValue.Data;
            }
            else if (column == excelConfiguration.DataColumn.Sex.Nomer)
            {
                columnItem.ColumnType = ColumnType.Sex;
                columnItem.BaseEntity.Value = resultValue.Data;
            }
            else if (column == excelConfiguration.DataColumn.Section.Nomer)
            {
                columnItem.ColumnType = ColumnType.Section;
                string name = excelConfiguration.NameColumnSection.MainLanguage;
                ITranslationEntity entity = new TranslationEntity();
                entity.Language =
                    LanguageHolder.GetISOCodes(name, _dataNormalization);
                entity.Value = resultValue.Data;
                columnItem.BaseEntity = entity;
            }
            else if (column == excelConfiguration.DataColumn.Language.Nomer)
            {
                columnItem.ColumnType = ColumnType.Language;
                string name = excelConfiguration.NameColumnSection.MainLanguage;
                ITranslationEntity entity = new TranslationEntity();
                entity.Language = LanguageHolder.GetISOCodes(name, _dataNormalization);
                entity.Value = resultValue.Data;
                columnItem.BaseEntity = entity;
            }
            else
            {
                nameTitle = _dataNormalization.NormalizeString(nameTitle).Data;

                if (nameTitle == null)
                {
                    dataResult.Success = false;

                    return dataResult;
                }

                if (nameTitle.Contains(configNameSection))
                {
                    columnItem.ColumnType = ColumnType.SectionTransfer;
                    nameTitle = nameTitle.Replace(configNameSection, string.Empty);
                }
                else
                {
                    columnItem.ColumnType = ColumnType.WorldSection;
                }

                string language = LanguageHolder.GetISOCodes(nameTitle, _dataNormalization);

                ITranslationEntity entity = new TranslationEntity();
                entity.Language = language;
                entity.Value = resultValue.Data;
                columnItem.BaseEntity = entity;


            }

            columnItem.ColumNumber = column;
            dataResult.Data = columnItem;
            dataResult.Success = true;

            return dataResult;
        }

        public int RowCount { get; set; }
    }
}
