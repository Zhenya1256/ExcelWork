using OfficeOpenXml;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;

namespace WorkWithExcel.Abstract.BL
{
    public interface IGetExcelSheetCongSection
    {
        IResult GetExcelConfig(ExcelWorksheet excelWorksheet);

        IDataResult<ExcelConfiguration>
            GeneratExcelConfig(ExcelWorksheet excelWorksheet);
    }
}
