using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace WorkWithExcel.Abstract.Entity.HelpEntity
{
    public interface IExcelWorksheetEntity
    {
        ExcelWorksheet ExcelWorksheet { get; set; }
        int RowNo { get; set; }
        int CellNo { get; set; }
    }
}
