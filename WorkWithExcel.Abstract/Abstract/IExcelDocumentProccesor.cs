using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;

namespace WorkWithExcel.Abstract.Abstract
{
    public interface IExcelDocumentProccesor
    {
        IDataResult<IDataSheetResulHolder> Processor(string path);
    }
}
