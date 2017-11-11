using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.Model.Implement
{
    public class DataSheetResulHolder : IDataSheetResulHolder
    {
        //IBaseExelEntety BaseExelEntety { get; set; }
       
        public IResult AppendColumn(ExcelWorksheet excelWorksheet, int column)
        {
            throw new NotImplementedException();
        }

        public IResult AppendRow(ExcelWorksheet excelWorksheet, int row)
        {
            throw new NotImplementedException();
        }

        public IResult AppendSheet()
        {
            throw new NotImplementedException();
        }

        public Dictionary<ITranslationEntity, List<ITranslationEntity>> IndexTranslates { get; set; }
        public ExcelConfiguration ExcelConfiguration { get; set; }//спитити бо кофіги для кожного листа різіні!
        public string NameExcel { get; set; }
        public ExcelDocumentType ExcelDocumentType { get; set; }

        public List<IDataSheet> DataSheets { get; set; }
        
    }
}
