using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.BL.Entity
{
   public class TranslateEntity : IDataExcelEntity
    {
        public string NameTitle { get; set; }
        public string Value { get; set; }
        public string PathImage { get; set; }
        public string Index { get; set; }
        public string PageNomer { get; set; }
        public SexType SexType { get; set; }
        public IExcelColor ExcelColor { get; set; }
    }
}
