using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;

namespace WorkWithExcel.BL.Impl
{
   public class DataNormalization : IDataNormalization
    {
        public IDataResult<IBaseExelEntety> Normalize(Dictionary<string, ITranslateEntity> translateEntities)
        {
            throw new NotImplementedException();
        }
    }
}
