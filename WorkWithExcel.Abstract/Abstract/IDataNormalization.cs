using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;

namespace WorkWithExcel.Abstract.Abstract
{
    public interface IDataNormalization
    {
        IDataResult<IBaseExelEntety> Normalize
            (Dictionary<string, ITranslateEntity> translateEntities);

        IDataResult<string> NormalizeString(string data);

    }
}
