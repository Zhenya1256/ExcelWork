using System.Collections.Generic;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.Abstract.Entity
{
    public interface IDataSheetResulHolder
    {
        IResult AppendColumn(ExcelWorksheet excelWorksheet, int column);
        IResult AppendRow(ExcelWorksheet excelWorksheet, int row);
        IResult AppendSheet();
        List<IDataSheet> DataSheets { get; set; }
        Dictionary<ITranslationEntity, List<ITranslationEntity>> IndexTranslates { get; set; }
        ExcelConfiguration ExcelConfiguration { get; set; }
        string NameExcel { get; set; }
        ExcelDocumentType ExcelDocumentType { get; set; }
    }
}
