using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity.HelpEntity;

namespace WorkWithExcel.Abstract.BL
{
   public interface IReadExcelData
   {
       IDataResult<string> GetValue(IExcelWorksheetEntity excelWorksheet);
       IDataResult<string> GetColorValue(IExcelWorksheetEntity excelWorksheetEntity);
   }
}
