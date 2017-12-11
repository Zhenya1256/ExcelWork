using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithExcel.Abstract.Entity.NormalizeEntity
{
    public interface IParsedResultEntity
    {
        List<ITranslationEntity> CategoryTranslate { get; set; }
       IItemEntity ItemEntity { get; set; }
    }
}
