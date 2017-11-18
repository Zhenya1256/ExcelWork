using System.Collections.Generic;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;

namespace WorkWithExcel.Abstract.BL
{
    public interface IDataNormalization
    {
        IDataResult<string> NormalizeString(string data);

        IDataResult<Dictionary<ITranslationEntity, List<ITranslationEntity>>>
            NormaliseTransliteSection(List<IRowItem> listRowItems);
    }
}
