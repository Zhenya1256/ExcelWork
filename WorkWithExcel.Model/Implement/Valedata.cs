using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.Model.Common;

namespace WorkWithExcel.Model.Implement
{
    public class Valedata
    {
        private readonly IGetExcelData _getExcelData;

        public Valedata()
        {
            _getExcelData = new GetExcelData();
        }

        public IResult VolidateExcel
            (ExcelWorksheet excelWorksheet)
        {

            IResult result = new Result() {Success = false};

            if (excelWorksheet.Dimension.Start.Row < 2)
            {
                result.Message = MessageHolder.GetErrorMessage
                    (MessageType.DocumentIsEmpty);

                return result;
            }


            return result;
        }
    }
}
