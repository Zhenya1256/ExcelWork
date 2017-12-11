using System.Collections.Generic;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.NormalizeEntity;

namespace WorkWithExcel.Model.Entity.NormalizeEntity
{
    public class BaseEntityCategory : IBaseEntityCategory
    {
        public Dictionary<ITranslationEntity, IParsedResultEntity> Categotis { get; set; }
        public bool IsMainLang { get; set; }
    }
}
