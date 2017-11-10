using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.HelpEntity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.Model.Common;

namespace WorkWithExcel.Model.Implement
{
    public class Validata : IValidata
    {
        public IResult VolidateExcel
            (ExcelWorksheet excelWorksheet)
        {

            IResult result = new Result() {Success = false};

            if (excelWorksheet.Dimension.End.Row < 2)
            {
                result.Message = MessageHolder.GetErrorMessage
                    (MessageType.DocumentIsEmpty);

                return result;
            }
            result.Success = true;

            return result;
        }

        public IResult ValidateExcelPages(ExcelWorksheets excelWorksheets)
        {
            IResult result = new Result() {Success = false};

            if (!excelWorksheets.Any())
            {
                result.Message = MessageHolder.GetErrorMessage(MessageType.DocumentIsEmpty);

                return result;
            }
            result.Success = true;

            return result;
        }

        public IResult ValidataExcelPath(string path)
        {
            IResult result = new Result() { Success = false };
            if (string.IsNullOrEmpty(path))
            {
                result.Message = MessageHolder.
                    GetErrorMessage(MessageType.FileIsempty);

                return result;
            }

            if (Path.GetExtension(path) != ".xlsx")
            {
                result.Message = MessageHolder.
                    GetErrorMessage(MessageType.NotFormat);

                return result;
            }

            result.Success = true;

            return result;
        }

     
    }
}
