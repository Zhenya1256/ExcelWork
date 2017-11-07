using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.HelpEntity;

namespace WorkWithExcel.Abstract.Abstract
{
    public interface IParser
    {
        IDataResult<IRowItem> RowParser(ExcelWorksheet excelWorksheet,int row, ExcelConfiguration excelConfiguration);
        IDataResult<IColumnItem> ColumnParser(IExcelWorksheetEntity worksheetEntity, ExcelConfiguration excelConfiguration);
        int RowCount { get; set; }

    }
}
