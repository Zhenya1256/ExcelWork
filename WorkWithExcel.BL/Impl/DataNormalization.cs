using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.BL.Impl
{
   public class DataNormalization : IDataNormalization
    {
        public IDataResult<IBaseExelEntety> Normalize(Dictionary<string, IDataExcelEntity> translateEntities)
        {
            throw new NotImplementedException();
        }

        public IDataResult<string> NormalizeString(string data)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Dictionary<IDataExcelEntity, List<ITranslationEntity>>> NormaliseTransliteWord(List<IRowItem> listRowItems)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Dictionary<ITranslationEntity, List<ITranslationEntity>>> NormaliseTransliteSection(List<IRowItem> listRowItems)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Dictionary<IDataExcelEntity, List<ITranslationEntity>>> NormaliseTranslite(List<IRowItem> listRowItems, ColumnType type)
        {
            throw new NotImplementedException();
        }
    }
}
