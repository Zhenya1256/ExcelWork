using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Entity.HelpEntity;

namespace WorkWithExcel.BL.Entity.HelperEntity
{
    public class ExcelWorksheetEntity : IExcelWorksheetEntity
    {
        public ExcelWorksheet ExcelWorksheet { get; set; }
        public int RowNo { get; set; }
        public int CellNo { get; set; }
    }
}
