using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithExcel.Abstract.Entity
{
    public interface IExcelColor : IBaseEntity
    {
        int R { get; set; }
        int G { get; set; }
        int B { get; set; }
    }
}
