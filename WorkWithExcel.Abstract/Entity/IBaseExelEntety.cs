using System.Collections.Generic;

namespace WorkWithExcel.Abstract.Entity
{
    public interface IBaseExelEntety
    {
        //New
        IDictionary<ITranslationEntity, List<ITranslationEntity>> SectionTranslates { get; set; }
        IDictionary<IDataExcelEntity, List<ITranslationEntity>> WordTranslates { get; set; }
    }
}
