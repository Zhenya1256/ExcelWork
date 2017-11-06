using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;

namespace WorkWithExcel.Model.Implement
{
    public class DataSheetResulHolder : IDataSheetResulHolder
    {
        public IResult AppendColumn(ExcelWorksheet excelWorksheet, int column)
        {
            throw new NotImplementedException();
        }

        public IResult AppendRow(ExcelWorksheet excelWorksheet, int row)
        {
            throw new NotImplementedException();
        }

        public IResult AppendSheet(ExcelWorksheet excelWorksheet)
        {
            throw new NotImplementedException();
        }
    }
}
