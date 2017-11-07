using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Entity;

namespace WorkWithExcel.Model.Entity
{
    public class DataSheet : IDataSheet
    {
        public List<IRowItem> RowItems { get; set; }
        public List<IRowItemError> RowItemErrors { get; set; }
        public IBaseExelEntety BaseExelEntety { get; set; }
        public int RowCount { get; set; }
        public string NameTable { get; set; }
    }
}
