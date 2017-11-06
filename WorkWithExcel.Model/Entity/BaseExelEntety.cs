using System.Collections.Generic;
using WorkWithExcel.Abstract.Entity;

namespace WorkWithExcel.Model.Entity
{
   public  class BaseExelEntety : IBaseExelEntety
    {
        public Dictionary<ITranslateSectionEntity, ITranslateEntity>
            TranslateEntitys { get; set; }

        public Dictionary<ITranslateSectionEntity, ITranslateEntity> ErrorTranslateEntitys { get; set; }
    }
}
