using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.BL;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.DAL.Entity;
using WorkWithExcel.Model.Impl;

namespace WorkWithExcel.DAL.Initial
{
    public class Init
    {
        private readonly IExcelDocumentProccesor _documentProccesor;
        public Init()
        {
           

            _documentProccesor = new ExcelDocumentProccesor();
           

        }

        public void InitBD(string path)
        {

            IDataResult<IDataSheetResulHolder> result = _documentProccesor.Processor(path);

            using (ExcelContext excelContext = new ExcelContext())
            {
                if (result.Success)
                {
                    CategoryTranslation category = new CategoryTranslation();
                    LangDictionary langDictionary = new LangDictionary();
                    ImageDescription imageDescription = new ImageDescription();
                    CategoryTranslation translation = new CategoryTranslation();
                    ImageDictionary imageDictionary = new ImageDictionary();
                    //imageDictionary
                    int langId = 0;

                    foreach (var item in result.Data.IndexTranslates)
                    {
                        translation.CategoryName = item.Key.Value;
                        ICollection<ImageDescription> discrptions = new List<ImageDescription>();


                        foreach (var value in item.Value)
                        {
                            langDictionary.ShortName = value.Language;
                            langDictionary.LongName = LanguageHolder.GetLanguage(value.Language);
                            langDictionary.Id = langId;
                            imageDescription.Description = value.Value;
                            imageDescription.LangDictionaryId = langId;
                            imageDescription.Id = langId;
                            langId++;
                            discrptions.Add(imageDescription);
                        }
                        imageDictionary.ImageDescriptions = discrptions;
                    }

                    foreach (var sheet in result.Data.DataSheets)
                    {
                        foreach (var rowItem in sheet.RowItems)
                        {
                            foreach (var column in rowItem.ColumnItems)
                            {
                                
                            }
                        }
                    }
                    //  imageDictionary.CategoryTranslation.Category.

                    //excelContext.ImageDescriptions.Add(imageDescription)
                }


            }
            
        }
    }
}
