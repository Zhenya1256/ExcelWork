using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;

namespace WorkWithExcel.Abstract.BL
{
    public interface IExcelDocumentProccesor
    {
        IDataResult<IDataSheetResulHolder> Processor(string path);
    }
}
