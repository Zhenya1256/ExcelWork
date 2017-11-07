using System.Collections.Generic;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Common;

namespace WorkWithExcel.Abstract.Entity
{
   public interface IDataSheetResulHolder
   {
       //IResult AppendColumn(ExcelWorksheet excelWorksheet, int column);
       //IResult AppendRow(ExcelWorksheet excelWorksheet, int row);
       //IResult AppendSheet(ExcelWorksheet excelWorksheet);
       List<IDataSheet> DataSheets { get; set; }
    //   IBaseExelEntety BaseExelEntety { get; set; }
   }
}
