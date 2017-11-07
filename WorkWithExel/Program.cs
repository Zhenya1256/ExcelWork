
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.BL.Entety;
using WorkWithExcel.BL.Impl;
using WorkWithExcel.Model.Implement;

namespace WorkWithExel
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "exp.xlsx";

            IExcelDocumentProccesor excelDocument = new ExcelDocumentProccesor();
            excelDocument.Processor(path);
            //section.GetType().GetProperties()

            //using (var file = File.Open(path, FileMode.Open))
            //{
            //    using (var xls = new ExcelPackage(file))
            //    {
            //        using (var sheet = xls.Workbook.Worksheets.FirstOrDefault())
            //        {
            //            section.GenerationConfig(sheet);
            //        }
            //    }
            //}
            //IValidata validate =  new Validata();
            //Division  div = new Division(validate);

            //IDataResult<IBaseExelEntety> dataResult = div.GetComponent(path);

            //foreach (var item in dataResult.Data.TranslateEntitys)
            //{
            //    int i = 0;
            //    foreach (var section in item.Key.TranslateSection)
            //    {
            //        i++;
            //      Console.WriteLine(i+")"+section.Key+" "+ section.Value);   
            //    }

            //}

            Console.ReadKey();
        }
    }
}
