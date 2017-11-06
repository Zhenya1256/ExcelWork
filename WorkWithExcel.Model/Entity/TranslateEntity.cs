using System.Collections.Generic;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.Model.Entity
{
   public class TranslateEntity : ITranslateEntity
    {
        public string Index { get; set; }
        public string PageNomer { get; set; }
        public SexType SexType { get; set; }
        public Dictionary<string, string> TranslateDictionary { get; set; }
    }
}
