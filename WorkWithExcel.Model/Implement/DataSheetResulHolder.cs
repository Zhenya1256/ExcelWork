using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;

namespace WorkWithExcel.Model.Implement
{
    public class DataSheetResulHolder : IDataSheetResulHolder
    {
        //IBaseExelEntety BaseExelEntety { get; set; }
        IDictionary<ITranslationEntity, List<ITranslationEntity>> iNDEXTranslates { get; set; }

        public IResult AppendColumn(ExcelWorksheet excelWorksheet, int column)
        {
            throw new NotImplementedException();
        }

        public IResult AppendRow(ExcelWorksheet excelWorksheet, int row)
        {
            throw new NotImplementedException();
        }

        public IResult AppendSheet(ExcelWorksheet excelWorksheet)
        {
            throw new NotImplementedException();
        }

        public List<IDataSheet> DataSheets { get; set; }
        
    }
}
