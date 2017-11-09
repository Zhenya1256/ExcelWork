﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
   public class ParserSectionPage : IParser
    {
        private readonly IGetExcelData _getExcelData;
        private readonly IDataNormalization _dataNormalization;

        public ParserSectionPage()
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
                j <= excelWorksheet.Dimension.End.Column;
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
            IDataResult<string> resultValue = _getExcelData.GetValue(worksheetEntity);
            int nomertitle = excelConfiguration.DataRowIndex.Title;

            if (!resultValue.Success)
            {
                dataResult.Message = resultValue.Message;
            }

            IExcelWorksheetEntity tmpExcel = new ExcelWorksheetEntity();
            tmpExcel.RowNo = nomertitle;
            tmpExcel.ExcelWorksheet = worksheetEntity.ExcelWorksheet;
            tmpExcel.CellNo = column;

            IDataResult<string> resultNameTitle = _getExcelData.GetValue(tmpExcel);

            if (!resultNameTitle.Success)
            {
                dataResult.Message = MessageHolder.GetErrorMessage(MessageType.NotNameTitle);
            }

            string nameTitle = resultNameTitle.Data;
            nameTitle = _dataNormalization.NormalizeString(nameTitle).Data;

            string titleConfig = excelConfiguration.DataColumn.Section.Name;
            titleConfig = _dataNormalization.NormalizeString(titleConfig).Data;

            if (nameTitle.Equals(titleConfig))
            {
                columnItem.ColumnType = ColumnType.Section;
                columnItem.NameTitle = nameTitle;

                if (!resultValue.Success)
                {
                    dataResult.Message = resultValue.Message;

                    return dataResult;
                }
                columnItem.Value = resultValue.Data;
            }
            else
            {
                columnItem.ColumnType = ColumnType.SectionTransfer;
                columnItem.NameTitle = nameTitle;

                if (!resultValue.Success)
                {
                    dataResult.Message = resultValue.Message;

                    return dataResult;
                }

                columnItem.Value = resultValue.Data;
            }

            dataResult.Data = columnItem;
            dataResult.Success = true;

            return dataResult;
        }

        public int RowCount { get; set; }
    }
}