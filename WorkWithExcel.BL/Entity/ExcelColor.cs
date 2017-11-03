using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.BL.Entity
{
    public class ExcelColor : IExcelColor
    {
        public string Index { get; set; }
        public string PageNomer { get; set; }
        public SexType SexType { get; set; }
        public Dictionary<string, string> TranslateDictionary { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
    }
}
