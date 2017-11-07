using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithExcel.Abstract.Entity
{
    public interface IDataSheet
    {
        List<IRowItem> RowItems { get; set; }
        List<IRowItemError> RowItemErrors { get; set; }
        IBaseExelEntety BaseExelEntety { get; set; }
        int RowCount { get; set; }
        string NameTable { get; set; }
    }
}
