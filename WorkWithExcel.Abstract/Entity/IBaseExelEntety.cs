using System.Collections.Generic;

namespace WorkWithExcel.Abstract.Entity
{
    public interface IBaseExelEntety
    {
        Dictionary<ITranslateSectionEntity, IDataExcelEntity> TranslateEntitys { get; set; }
        Dictionary<ITranslateSectionEntity, IDataExcelEntity> ErrorTranslateEntitys { get; set; }
        //New
        IDictionary<string, List<ITranslationEntity>> SectionTranslates { get; set; }
        IDictionary<string, List<ITranslationEntity>> WordTranslates { get; set; }
    }
}
