using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithExcel.Abstract.Entity
{
   public interface IRowItemError
    {
        List<IColumnItem> ColumnItems { get; set; }
    }
}
