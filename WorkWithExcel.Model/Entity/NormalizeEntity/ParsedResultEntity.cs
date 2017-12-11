using System.Collections.Generic;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.NormalizeEntity;

namespace WorkWithExcel.Model.Entity.NormalizeEntity
{
    public  class ParsedResultEntity : IParsedResultEntity
    {
        public List<ITranslationEntity> CategoryTranslate { get; set; }
        public IItemEntity ItemEntity { get; set; }
    }
}
