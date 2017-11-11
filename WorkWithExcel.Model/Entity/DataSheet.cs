using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Entity;

namespace WorkWithExcel.Model.Entity
{
    public class DataSheet : IDataSheet
    {
        public List<IColumnItem> ColumnTitleItems { get; set; }
        public List<IRowItem> RowItems { get; set; }
        public List<IRowItemError> RowItemErrors { get; set; }
        public ExcelWorksheet ExcelWorksheet { get; set; }
        public int RowCount { get; set; }
        public string NameTable { get; set; }
    }
}
