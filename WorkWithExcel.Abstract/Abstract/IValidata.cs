using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.HelpEntity;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.Abstract.Abstract
{
   public interface IValidata
   {
       IResult ValidataExcelPath(string path);
       IResult VolidateExcel
           (ExcelWorksheet excelWorksheet);
       IResult ValidateExcelPages(ExcelWorksheets excelWorksheets);
   }
}
