using OfficeOpenXml;
using WorkWithExcel.Abstract.Common;

namespace WorkWithExcel.Abstract.Abstract
{
   public interface IDataSheetResulHolder
   {
       IResult AppendColumn(ExcelWorksheet excelWorksheet, int column);
       IResult AppendRow(ExcelWorksheet excelWorksheet, int row);
       IResult AppendSheet(ExcelWorksheet excelWorksheet);
    }
}
