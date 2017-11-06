using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity.HelpEntity;

namespace WorkWithExcel.Abstract.Abstract
{
   public interface IGetExcelData
   {
       IDataResult<string> GetValue(IExcelWorksheetEntity excelWorksheet);
   }
}
