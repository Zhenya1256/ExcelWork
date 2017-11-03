using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Entity;

namespace WorkWithExcel.BL.Entety
{
   public  class BaseExelEntety : IBaseExelEntety
    {
        public Dictionary<ITranslateSectionEntity, ITranslateEntity>
            TranslateEntities { get; set; }
    }
}
