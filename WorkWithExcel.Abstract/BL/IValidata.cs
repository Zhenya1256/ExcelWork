using OfficeOpenXml;
using WorkWithExcel.Abstract.Common;

namespace WorkWithExcel.Abstract.BL
{
   public interface IValidata
   {
       IResult ValidataExcelPath(string path);
       IResult VolidateExcel
           (ExcelWorksheet excelWorksheet);
       IResult ValidateExcelPages(ExcelWorksheets excelWorksheets);
   }
}
