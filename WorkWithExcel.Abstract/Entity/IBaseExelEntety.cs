using System.Collections.Generic;

namespace WorkWithExcel.Abstract.Entity
{
   public interface IBaseExelEntety
   {
       Dictionary<ITranslateSectionEntity, ITranslateEntity> TranslateEntities { get; set; }
   }
}
