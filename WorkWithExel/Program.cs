
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using WorkWithExcel.Abstract.BL;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Model.Impl;
using WorkWithExcel.DAL.Initial;

namespace WorkWithExel
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "exp1.xlsx";

            //IExcelDocumentProccesor excelDocument = new ExcelDocumentProccesor();
            //excelDocument.Processor(path);
            Init init = new Init();
            init.InitBD(path);


            Console.ReadKey();
        }
    }
}
