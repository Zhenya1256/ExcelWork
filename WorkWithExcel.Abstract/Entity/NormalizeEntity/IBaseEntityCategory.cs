using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithExcel.Abstract.Entity.NormalizeEntity
{
    public interface IBaseEntityCategory
    {
        Dictionary<ITranslationEntity,IParsedResultEntity> Categotis { get; set; }
        bool IsMainLang { get; set; }
    }
}
