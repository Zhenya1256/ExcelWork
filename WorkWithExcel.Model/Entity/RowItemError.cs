using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;

namespace WorkWithExcel.Model.Entity
{
    public class RowItemError : IRowItemError
    {
        public int RowNmomer { get; set; }
        public List<IDataResult<IColumnItem>> ListColums { get; set; }
        public List<IColumnItem> ColumnItems { get; set; }
    }
}
