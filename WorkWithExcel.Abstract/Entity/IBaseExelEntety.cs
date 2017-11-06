using System.Collections.Generic;

namespace WorkWithExcel.Abstract.Entity
{
    public interface IBaseExelEntety
    {
        Dictionary<ITranslateSectionEntity, ITranslateEntity> TranslateEntitys { get; set; }
        Dictionary<ITranslateSectionEntity, ITranslateEntity> ErrorTranslateEntitys { get; set; }
        //New
        IDictionary<string, List<ITranslationEntity>> Translates { get; set; }
    }
}
