using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity.HelpEntity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.Model.Common;

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
    }
}
