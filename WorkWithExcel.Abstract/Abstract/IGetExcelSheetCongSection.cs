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
        IResult GetConfig
            (ExcelWorksheet excelWorksheet);
        IDataResult<ExcelConfiguration> GenerationConfig
            (ExcelWorksheet excelWorksheet);

    }
}
