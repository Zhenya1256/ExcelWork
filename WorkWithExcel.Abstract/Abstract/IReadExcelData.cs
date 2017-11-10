using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.HelpEntity;

namespace WorkWithExcel.Abstract.Abstract
{
   public interface IReadExcelData
   {
       IDataResult<string> GetValue(IExcelWorksheetEntity excelWorksheet);
       IDataResult<string> GetColorValue(IExcelWorksheetEntity excelWorksheetEntity);
   }
}
