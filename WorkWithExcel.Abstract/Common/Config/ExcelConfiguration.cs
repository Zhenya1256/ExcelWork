using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithExcel.Abstract.Common.Config
{
    public class ExcelConfiguration
    {
        public DataRowIndex DataRowIndex { get; set; }
        public DataColumn DataColumn { get; set; }
        public NamePage NamePage { get; set; }
        public NameColumnSection NameColumnSection { get; set; }
    }
}
