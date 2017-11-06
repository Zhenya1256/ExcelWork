using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.Abstract.Entity
{
    public interface LoadItemEntity
    {
        ColumnType ColumnType { get; set; }

    }
}
