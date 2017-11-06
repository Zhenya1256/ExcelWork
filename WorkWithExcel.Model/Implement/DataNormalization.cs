using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.Model.Common;

namespace WorkWithExcel.Model.Implement
{
    public class DataNormalization : IDataNormalization
    {
        public IDataResult<IBaseExelEntety> Normalize(Dictionary<string, ITranslateEntity> translateEntities)
        {
            throw new NotImplementedException();
        }

        public IDataResult<string> NormalizeString(string data)
        {
            IDataResult<string> dataResult =
                new DataResult<string>() {Success = false};

            if (string.IsNullOrEmpty(data))
            {
                dataResult.Message = MessageHolder.
                    GetErrorMessage(MessageType.IsNullOrEmpty);

                return dataResult;
            }
           
            data = Replace(data, " ");
            data = data.ToLower();
            dataResult.Success = true;
            dataResult.Data = data;

            return dataResult;
        }

        private string Replace(string data, string sym)
        {
            while (data.Contains(sym))
            {
                data = data.Replace(sym, "");
            }

            return data;
        }
    }
}
