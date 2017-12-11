using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity.NormalizeEntity;

namespace WorkWithExcel.Abstract.BL
{
   public interface INormalizeData
   {
       IDataResult<IBaseEntityCategory> Normalize(string path);
       bool GatLang(int LangId);
   }
}
