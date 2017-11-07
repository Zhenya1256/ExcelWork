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

               columnItems.Add(getDataResult.Data);

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
            int row = excelWorksheet.RowNo;
            int column = excelWorksheet.CellNo;
            IDataResult<string> resultValue = _getExcelData.GetValue(excelWorksheet);

            if (!resultValue.Success)
            {
                dataResult.Message = resultValue.Message;
            }

            if (column == excelConfiguration.DataColumn.Picture.Nomer)
            {
                columnItem.ColumnType = ColumnType.Picture;
            }
            else if (column == excelConfiguration.DataColumn.Index.Nomer)
            {
                columnItem.ColumnType = ColumnType.Index;
            }
     
            else if (column == excelConfiguration.DataColumn.Page.Nomer)
            {
                columnItem.ColumnType = ColumnType.Page;
            }
            else if (column == excelConfiguration.DataColumn.Sex.Nomer)
            {
                columnItem.ColumnType = ColumnType.Sex;
            }
            else if (column == excelConfiguration.DataColumn.Section.Nomer)
            {
                columnItem.ColumnType = ColumnType.Section;
            }
            else if (column == excelConfiguration.DataColumn.Language.Nomer)
            {
                columnItem.ColumnType = ColumnType.Language;
            }
            else
            {
                excelWorksheet.RowNo = excelConfiguration.DataRowIndex.Title;
                IDataResult < string > titleNameResult= _getExcelData.GetValue(excelWorksheet);

                //if (!titleNameResult.Success)
                //{
                //    dataResult.Message = titleNameResult.Message;

                //    return dataResult;
                //}

                string titleName = titleNameResult.Data;
                titleName = _dataNormalization.NormalizeString(titleName).Data;
                string configName = excelConfiguration.DataColumn.Section.Name;
                configName = _dataNormalization.NormalizeString(configName).Data;

                if (titleName.Contains(configName))
                {
                    columnItem.ColumnType = ColumnType.SectionTransfer;
                }
                else
                {
                    columnItem.ColumnType = ColumnType.WorldSection;
                }
            }

            columnItem.Value = resultValue.Data;
            dataResult.Data = columnItem;
            
            return dataResult;
        }

        public int RowCount { get; set; }
    }
}
