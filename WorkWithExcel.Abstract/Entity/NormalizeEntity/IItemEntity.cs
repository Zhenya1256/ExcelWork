using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithExcel.Abstract.Entity.NormalizeEntity
{
   public  interface IItemEntity
    {
        IRootTranslationItemEntity MainSection { get; set; }
        List<ITranslationItemEntity> TranslationItemEntitys { get; set; }
    }
}
