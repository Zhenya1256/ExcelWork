using System.Collections.Generic;
using WorkWithExcel.Abstract.Entity;

namespace WorkWithExcel.Model.Entity
{
   public  class BaseExelEntety : IBaseExelEntety
    {
      
        public IDictionary<ITranslationEntity, List<ITranslationEntity>> SectionTranslates { get; set; }
        public IDictionary<IDataExcelEntity, List<ITranslationEntity>> WordTranslates { get; set; }
    }
}
