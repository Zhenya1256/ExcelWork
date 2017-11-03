using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Common;

namespace WorkWithExcel.BL.Common
{
    public class Result :IResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
