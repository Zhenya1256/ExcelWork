﻿using System.Drawing;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.HelpEntity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.Model.Common;
using WorkWithExcel.Model.Entity;

namespace WorkWithExcel.Model.Implement
{
    public class GetExcelData : IGetExcelData
    {
        public IDataResult<string> GetValue(IExcelWorksheetEntity excelWorksheet)
        {
            IDataResult<string> dataResult =
                new DataResult<string>() { Success = false };

            int rowNo = excelWorksheet.RowNo;
            int cellNo = excelWorksheet.CellNo;

            if (excelWorksheet.ExcelWorksheet.Cells[rowNo, cellNo].Value != null)
            {
                string result = excelWorksheet.ExcelWorksheet
                    .Cells[rowNo, cellNo].Value.ToString();
                dataResult.Success = true;
                dataResult.Data = result;

                return dataResult;
            }

            dataResult.Message = 
                MessageHolder.GetErrorMessage(MessageType.IsNullOrEmpty)+"("+ rowNo+"|"+ cellNo + ")";

            return dataResult;
        }

        public IDataResult<string> GetColorValue(IExcelWorksheetEntity excelWorksheetEntity)
        {
            IDataResult<string> dataResult =
                    new DataResult<string>() { Success = false };

            int rowNo = excelWorksheetEntity.RowNo;
            int cellNo = excelWorksheetEntity.CellNo;

            string colorName = excelWorksheetEntity.ExcelWorksheet.
                Cells[rowNo, cellNo].Style.Fill.BackgroundColor.Rgb;

            if (string.IsNullOrEmpty(colorName))
            {
                dataResult.Success = false;

                return dataResult;
            }

            dataResult.Success = true;
            dataResult.Data = colorName;
            //Color color = ColorTranslator.FromHtml("#" + colorName);
            //excelColor.R = color.R;
            //excelColor.G = color.G;
            //excelColor.B = color.B;

            //dataResult.Success = true;
            //dataResult.Data = excelColor;

            return dataResult;
        }
    }
}
