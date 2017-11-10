using System.Collections.Generic;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.Model.Entity
{
    public class ExcelColor : IExcelColor
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public string Value { get; set; }
    }
}
