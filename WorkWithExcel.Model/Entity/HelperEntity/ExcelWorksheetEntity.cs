using OfficeOpenXml;
using WorkWithExcel.Abstract.Entity.HelpEntity;

namespace WorkWithExcel.Model.Entity.HelperEntity
{
    public class ExcelWorksheetEntity : IExcelWorksheetEntity
    {
        public ExcelWorksheet ExcelWorksheet { get; set; }
        public int RowNo { get; set; }
        public int CellNo { get; set; }
    }
}
