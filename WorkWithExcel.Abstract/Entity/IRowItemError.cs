using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Common;

namespace WorkWithExcel.Abstract.Entity
{
   public interface IRowItemError :IRowItem
    {
        int RowNmomer { get; set; }
        List<IDataResult<IColumnItem>> ListColums { get; set; }
    }
}
