
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
using WorkWithExcel.Abstract.Entity.NormalizeEntity;
using WorkWithExcel.Model.Impl;
using WorkWithExcel.DAL.Initial;

namespace WorkWithExel
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "exp1.xlsx";

            //   INormalizeData excelDocument = new NormalizeData();
            //IDataResult<IBaseEntityCategory> entity = excelDocument.Normalize(path);
            using (ExcelDocumentProcessor excelDocumentProcessor = new ExcelDocumentProcessor())
                excelDocumentProcessor.InitDb(path);
            //string words = WriteWord(entity.Data);

            Console.ReadKey();
        }

        private static void WriteTxt(string value,string name)
        {
            var file = File.Create(name);
            byte[] butes = Encoding.UTF8.GetBytes(value);
            file.Write(butes, 0, butes.Length);
            file.Close();
        }

        private static string WriteWord(IBaseEntityCategory entity)
        {
            StringBuilder result = new StringBuilder();
            using (StreamWriter sw = new StreamWriter("Words.txt", true))
            {
                foreach (var list in entity.Categotis.Values)
                {
                    if (list.ItemEntity?.TranslationItemEntitys != null)
                        foreach (var s in list.ItemEntity?.TranslationItemEntitys)
                        {
                            foreach (var val in s.WordTranslations.Where(p => p.Language == "en"))
                            {
                                sw.WriteLine(val.Value);
                            }
                        }
                }
            }

            return result.ToString();
        }

        private static string WriteCateg(IBaseEntityCategory entity)
        {
            StringBuilder result = new StringBuilder();
            //var file = File.Create("Categories.txt");
            using (StreamWriter sw = new StreamWriter("Categories.txt", true))
            {
                foreach (ITranslationEntity stri in entity.Categotis.Keys)
                {
                   // file.Write(Encoding.UTF8.GetBytes(stri.Value), 0, Encoding.UTF8.GetBytes(stri.Value).Length);
                    sw.WriteLine(stri.Value);
                }
            }

            return result.ToString();
        }
    }
}
