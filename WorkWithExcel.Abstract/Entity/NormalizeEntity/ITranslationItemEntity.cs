using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.Abstract.Entity.NormalizeEntity
{
    public interface IBaseTranslationItemEntity
    {
        string SexType { get; set; }
        string Index { get; set; }
        string Page { get; set; }
        IExcelColor ExcelColor { get; set; }
    }

    public interface IRootTranslationItemEntity : IBaseTranslationItemEntity
    {
        ITranslationEntity WordTranslation { get; set; }
    }

    public interface ITranslationItemEntity : IBaseTranslationItemEntity
    {
        List<ITranslationEntity> WordTranslations { get; set; }

    }
}
