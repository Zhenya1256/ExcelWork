using System.Collections.Generic;
using WorkWithExcel.Abstract.Entity;

namespace WorkWithExcel.Model.Entity
{
   public class TranslateSectionEntity : ITranslateSectionEntity
    {
        public Dictionary<string, string> TranslateSection { get; set; }
    }
}
