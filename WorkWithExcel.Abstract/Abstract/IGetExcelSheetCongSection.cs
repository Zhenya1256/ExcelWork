using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;

namespace WorkWithExcel.Abstract.Abstract
{
    public interface IGetExcelSheetCongSection
    {
        IResult GetExcelConfig(ExcelWorksheet excelWorksheet);

        IDataResult<ExcelConfiguration>
            GeneratExcelConfig(ExcelWorksheet excelWorksheet);
    }
}
