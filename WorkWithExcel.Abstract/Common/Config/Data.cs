using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.Abstract.Common.Config
{
    public class Data
    {
        public int Nomer { get; set; }
        public  string Name { get; set; }
        public bool MustExist { get; set; }
        public int ColumnType { get; set; }
    }
}
