using System;
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
using WorkWithExcel.Abstract.Entity.HelpEntity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.Model.Common;

namespace WorkWithExcel.Model.Implement
{
    public class Validata : IValidata
    {
        private readonly IGetExcelData _getExcelData;

        public Validata()
        {
            _getExcelData = new GetExcelData();
        }

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

        public IResult ValidataExcel(string path)
        {
            throw new NotImplementedException();
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

        public IDataResult<string> GetValue(IExcelWorksheetEntity excelWorksheetEntity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<SexType> GetSexType(IExcelWorksheetEntity excelWorksheetEntity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Dictionary<string, string>> GetTranslateEntity(IExcelWorksheetEntity excelWorksheetEntity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<IExcelColor> GetColorValue(IExcelWorksheetEntity excelWorksheetEntity)
        {
            throw new NotImplementedException();
        }
    }
}
