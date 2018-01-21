using System.Text;
using WorkWithExcel.Abstract.BL;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity.HelpEntity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.Model.Common;

namespace WorkWithExcel.Model.Impl
{
    public class ReadExcelData : IReadExcelData
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

            dataResult.Message = "("+AlphabetHolder.GetSmbol(cellNo)+") "+
                MessageHolder.GetErrorMessage
                (MessageType.IsNullOrEmpty);
                //  MessageHolder.GetErrorMessage(MessageType.FrontBracket) + 
                //"("+rowNo+")" +;
                     //MessageHolder.GetErrorMessage(MessageType.BackBracket);

            return dataResult;
        }

        //public string ErrorMessage(string message)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    int i = 1;

        //    while(message!=null)
        //    {
        //        if (message.Contains("(" + i + ")"))
        //        {
                    
        //        }
        //        else
        //        {
        //            i++;
        //        }
        //    }
        //}

        public IDataResult<string> GetColorValue
            (IExcelWorksheetEntity excelWorksheetEntity)
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
          
            return dataResult;
        }
    }
}
