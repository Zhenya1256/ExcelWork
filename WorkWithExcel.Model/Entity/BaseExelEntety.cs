using System.Collections.Generic;
using WorkWithExcel.Abstract.Entity;

namespace WorkWithExcel.Model.Entity
{
   public  class BaseExelEntety : IBaseExelEntety
    {
        public Dictionary<ITranslateSectionEntity, IDataExcelEntity>
            TranslateEntitys { get; set; }

        public Dictionary<ITranslateSectionEntity, IDataExcelEntity> ErrorTranslateEntitys { get; set; }
        public IDictionary<string, List<ITranslationEntity>> SectionTranslates { get; set; }
        public IDictionary<string, List<ITranslationEntity>> WordTranslates { get; set; }
    }
}
