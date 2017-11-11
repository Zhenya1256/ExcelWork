using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.Abstract.Abstract
{
    public interface IDataNormalization
    {
        IDataResult<string> NormalizeString(string data);

        IDataResult<Dictionary<ITranslationEntity, List<ITranslationEntity>>>
            NormaliseTransliteSection(List<IRowItem> listRowItems);
    }
}
