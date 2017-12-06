using System;
using System.Collections.Generic;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.Model.Impl
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
        public ExcelConfiguration ExcelConfiguration { get; set; }
        public string NameExcel { get; set; }
        public ExcelDocumentType ExcelDocumentType { get; set; }
        public List<IDataSheet> DataSheets { get; set; }
        
    }
}
