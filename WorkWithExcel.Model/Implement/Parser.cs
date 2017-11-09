using System;
using System.Collections.Generic;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.HelpEntity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Model.Common;
using WorkWithExcel.Model.Entity;
using WorkWithExcel.Model.Entity.HelperEntity;

namespace WorkWithExcel.Model.Implement
{
    public class Parser : IParser
    {
        private readonly IGetExcelData _getExcelData;
        private readonly IDataNormalization _dataNormalization;

        public Parser()
        {
            _getExcelData = new GetExcelData();
            _dataNormalization = new DataNormalization();
        }

        public IDataResult<IRowItem> RowParser
            (ExcelWorksheet excelWorksheet, int row, ExcelConfiguration excelConfiguration)
        {
            IDataResult<IRowItem> dataResult =
                new DataResult<IRowItem>() { Success = false };
            IRowItem rowItem = new RowItem();

            List<IColumnItem> columnItems = new List<IColumnItem>();      

            for (int j = excelWorksheet.Dimension.Start.Column;
                j <excelWorksheet.Dimension.End.Column;
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
            (IExcelWorksheetEntity excelWorksheet, ExcelConfiguration excelConfiguration)
        {
            IDataResult<IColumnItem> dataResult =
                new DataResult<IColumnItem>() {Success = false};

            IColumnItem columnItem = new ColumnItem();
            int column = excelWorksheet.CellNo;
            IDataResult<string> resultValue = _getExcelData.GetValue(excelWorksheet);
            int nomertitle = excelConfiguration.DataRowIndex.Title;

            if (!resultValue.Success)
            {
                dataResult.Message = resultValue.Message;
            }

            IExcelWorksheetEntity tmpExcel = new ExcelWorksheetEntity();
            tmpExcel.RowNo = nomertitle;
            tmpExcel.ExcelWorksheet = excelWorksheet.ExcelWorksheet;
            tmpExcel.CellNo = column;
            string nameTitle = _getExcelData.GetValue(tmpExcel).Data;

            if (column == excelConfiguration.DataColumn.Picture.Nomer)
            {
                columnItem.ColumnType = ColumnType.Picture;
                IDataResult<string> colorNameResult =
                    _getExcelData.GetColorValue(excelWorksheet);

                if (colorNameResult.Success)
                {
                    columnItem.Value = colorNameResult.Data;
                }

            }
            else if (column == excelConfiguration.DataColumn.Index.Nomer)
            {
                columnItem.ColumnType = ColumnType.Index;
                columnItem.Value = resultValue.Data;
            }
     
            else if (column == excelConfiguration.DataColumn.Page.Nomer)
            {
                columnItem.ColumnType = ColumnType.Page;
                columnItem.Value = resultValue.Data;
            }
            else if (column == excelConfiguration.DataColumn.Sex.Nomer)
            {
                columnItem.ColumnType = ColumnType.Sex;
                columnItem.Value = resultValue.Data;
            }
            else if (column == excelConfiguration.DataColumn.Section.Nomer)
            {
                columnItem.ColumnType = ColumnType.Section;
                columnItem.Value = resultValue.Data;
            }
            else if (column == excelConfiguration.DataColumn.Language.Nomer)
            {
                columnItem.ColumnType = ColumnType.Language;
                columnItem.Value = resultValue.Data;
            }
            else
            {
                
                nameTitle = _dataNormalization.NormalizeString(nameTitle).Data;
                string configName = excelConfiguration.DataColumn.Section.Name;
                configName = _dataNormalization.NormalizeString(configName).Data;

                if (nameTitle == null)
                {
                    dataResult.Success = false;

                    return dataResult;
                }

                if (nameTitle.Contains(configName))
                {
                    columnItem.ColumnType = ColumnType.SectionTransfer;
                }
                else
                {
                    columnItem.ColumnType = ColumnType.WorldSection;
                }

                columnItem.Value = resultValue.Data;
            }

        
            columnItem.NameTitle = nameTitle;
            dataResult.Data = columnItem;
            dataResult.Success = true;

            return dataResult;
        }

        public int RowCount { get; set; }
    }
}
