using System.Collections.Generic;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.NormalizeEntity;

namespace WorkWithExcel.Model.Entity.NormalizeEntity
{
    public class  ItemEntity : IItemEntity
    {
        public IRootTranslationItemEntity MainSection { get; set; }
        public List<ITranslationItemEntity> TranslationItemEntitys { get; set; }
    }
}
