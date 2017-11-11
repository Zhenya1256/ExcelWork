using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace WorkWithExcel.Abstract.Entity
{
    public interface IDataSheet
    {
        List<IColumnItem> ColumnTitleItems { get; set; }
        List<IRowItem> RowItems { get; set; }
        List<IRowItemError> RowItemErrors { get; set; }
        int RowCount { get; set; }
        string NameTable { get; set; }
    }
}
