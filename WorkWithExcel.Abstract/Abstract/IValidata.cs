using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.HelpEntity;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.Abstract.Abstract
{
   public interface IValidata
   {
       IResult ValidataExcel(string path);
       IDataResult<string> GetValue(IExcelWorksheetEntity excelWorksheetEntity);
       IDataResult<SexType> GetSexType(IExcelWorksheetEntity excelWorksheetEntity);

       IDataResult<Dictionary<string, string>>
           GetTranslateEntity(IExcelWorksheetEntity excelWorksheetEntity);

       IDataResult<IExcelColor> GetColorValue(IExcelWorksheetEntity excelWorksheetEntity);
   }
}
