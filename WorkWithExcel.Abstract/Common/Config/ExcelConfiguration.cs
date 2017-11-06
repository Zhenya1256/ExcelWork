using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithExcel.Abstract.Common.Config
{
    public class ExcelConfiguration
    {
        public DataColumnIndex DataColumnIndex { get; set; }
        public DataRowIndex DataRowIndex { get; set; }
        public DataColumnName DataColumnName { get; set; }
        public DataColumn DataColumn { get; set; }
    }
}
