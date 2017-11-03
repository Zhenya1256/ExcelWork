using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Entity;

namespace WorkWithExcel.BL.Entity
{
   public class TranslateSectionEntity : ITranslateSectionEntity
    {
        public Dictionary<string, string> TranslateSection { get; set; }
    }
}
