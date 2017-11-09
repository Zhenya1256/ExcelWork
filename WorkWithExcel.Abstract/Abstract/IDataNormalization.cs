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
        IDataResult<IBaseExelEntety> Normalize
            (Dictionary<string, IDataExcelEntity> translateEntities);

        IDataResult<string> NormalizeString(string data);

         IDataResult<Dictionary<IDataExcelEntity, List<ITranslationEntity>>>
            NormaliseTransliteWord(List<IRowItem> listRowItems);

        IDataResult<Dictionary<ITranslationEntity, List<ITranslationEntity>>>
            NormaliseTransliteSection(List<IRowItem> listRowItems);

    }
}
