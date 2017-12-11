using System.Collections.Generic;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.HelpEntity;

namespace WorkWithExcel.Abstract.BL
{
    public interface IParser
    {
        int RowCount { get; set; }
        IDataResult<IRowItem> RowParser(ExcelWorksheet excelWorksheet,
            int row, ExcelConfiguration excelConfiguration);
        IDataResult<IColumnItem> ColumnParser(IExcelWorksheetEntity worksheetEntity,
            ExcelConfiguration excelConfiguration);

        IDataResult<List<IColumnItem>> GetCulumnTitleItem
            (ExcelWorksheet sheet, ExcelConfiguration excelConfiguration);
    }
}
