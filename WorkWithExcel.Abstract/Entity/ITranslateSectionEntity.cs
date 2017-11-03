using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithExcel.Abstract.Entity
{
   public interface ITranslateSectionEntity
    {
        Dictionary<string,string> TranslateSection { get; set; }
    }
}
