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
        public Dictionary<ITranslateSectionEntity, IDataExcelEntity>
            TranslateEntitys { get; set; }

        public Dictionary<ITranslateSectionEntity, IDataExcelEntity> ErrorTranslateEntitys { get; set; }
        public IDictionary<ITranslationEntity, List<ITranslationEntity>> SectionTranslates { get; set; }
        public IDictionary<IDataExcelEntity, List<ITranslationEntity>> WordTranslates { get; set; }
    }
}
