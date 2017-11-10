using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithExcel.Abstract.Entity
{
    public interface ITranslationEntity : IBaseEntity
    {
        string Language { get; set; }
    }

    public interface IBaseEntity
    {
        string Value { get; set; }
    }
}
