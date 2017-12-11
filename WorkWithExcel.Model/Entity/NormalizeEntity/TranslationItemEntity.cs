using System.Collections.Generic;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.NormalizeEntity;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.Model.Entity.NormalizeEntity
{
    public  class TranslationItemEntity : BaseTranslationItemEntity, ITranslationItemEntity
    {
        public List<ITranslationEntity> WordTranslations { get; set; }
    }
}
