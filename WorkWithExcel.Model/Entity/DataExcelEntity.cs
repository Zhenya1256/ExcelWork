using System.Collections.Generic;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.Model.Entity
{
   public class DataExcelEntity : IDataExcelEntity
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
