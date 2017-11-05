
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.BL.Entety;
using WorkWithExcel.BL.Impl;

namespace WorkWithExel
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "exp.xlsx";

            IValidata validate =  new Validata();
            Division  div = new Division(validate);

            IDataResult<IBaseExelEntety> dataResult = div.GetComponent(path);

            foreach (var item in dataResult.Data.TranslateEntitys)
            {
                int i = 0;
                foreach (var section in item.Key.TranslateSection)
                {
                    i++;
                  Console.WriteLine(i+")"+section.Key+" "+ section.Value);   
                }

            }

            Console.ReadKey();
        }
    }
}
