using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.Model.Entity
{
    public class ColumnItem : IColumnItem
    {
        public ColumnType ColumnType { get; set; }
        public string Value { get; set; }
        public string NameTitle { get; set; }
    }
}
