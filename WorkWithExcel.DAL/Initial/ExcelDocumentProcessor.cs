using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.BL;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Dal.Repositor;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.NormalizeEntity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.DAL.Repositor;
using WorkWithExcel.Model.Common;
using WorkWithExcel.Model.Entity.DalEntity;
using WorkWithExcel.Model.Impl;

namespace WorkWithExcel.DAL.Initial
{
    public class ExcelDocumentProcessor : IDisposable
    {
        private SketchpackDbContext _dbContext;
        private readonly INormalizeData _normalizeData;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryTranslationRepository _categoryTranslationRepository;
        private readonly IImageDescriptionRepository _imageDescriptionRepository;
        private readonly IImageDictionaryRepository _imageDictionaryRepository;
        private readonly ILangDictionaryRepository _langDictionaryRepository;


        public ExcelDocumentProcessor()
        {
            _normalizeData = new NormalizeData();
            _dbContext = new SketchpackDbContext();
            _categoryRepository = new CategoryRepository(_dbContext);
            _categoryTranslationRepository = new CategoryTranslationRepository(_dbContext);
            _imageDescriptionRepository = new ImageDescriptionRepository(_dbContext);
            _imageDictionaryRepository = new ImageDictionaryRepository(_dbContext);
            _langDictionaryRepository = new LangDictionaryRepository(_dbContext);
        }

        public async Task InitDb(string path)
        {
            IDataResult<IBaseEntityCategory> dataResult = _normalizeData.Normalize(path);

            return;
            if (!dataResult.Success)
            {
                return;
            }

            IBaseEntityCategory excelProcessingEntity = dataResult.Data;

            foreach (KeyValuePair<ITranslationEntity, IParsedResultEntity> parsedResultEntity in
                excelProcessingEntity.Categotis)
            {
                string fullName =
                    LanguageHolder.GetLanguage(parsedResultEntity.Key.Language);

                if (fullName != null)
                {
                    await AppendLangDictionary(parsedResultEntity.Key.Language, fullName);
                    IDataResult<int> checkCategoryExistResult =
                        await CheckCategoryExist(parsedResultEntity.Key);
                    int categoryId;

                    if (checkCategoryExistResult.Success)
                    {
                        categoryId = checkCategoryExistResult.Data;
                    }
                    else
                    {
                        var category = new Category()
                        {
                            ImgPath = ""
                        };

                        await _categoryRepository.AddAsync(category);
                        await _categoryRepository.SaveAsync();
                        categoryId = category.Id;

                    }

                    IDataResult<List<string>> existingLanguageResult =
                        await CheckLanguagesExists(categoryId);

                    List<string> existingLanguage = existingLanguageResult.Data;

                    if (existingLanguage != null &&
                        existingLanguage.All(p => p != parsedResultEntity.Key.Language))
                    {
                        await _categoryTranslationRepository.AddAsync(new CategoryTranslation()
                        {
                            CategoryId = categoryId,
                            IsMainTranslation = true,
                            LangDictionaryId = parsedResultEntity.Key.Language,
                            CategoryName = parsedResultEntity.Key.Value
                        });

                        await _categoryTranslationRepository.SaveAsync();
                    }

                    foreach (ITranslationEntity translationEntity in parsedResultEntity.Value.CategoryTranslate)
                    {
                        if (existingLanguage != null &&
                            //existingLanguage.Any() && 
                            existingLanguage.All(p => p != translationEntity.Language))
                        {
                            await _categoryTranslationRepository.AddAsync(new CategoryTranslation()
                            {
                                CategoryId = categoryId,
                                IsMainTranslation = false,
                                LangDictionaryId = translationEntity.Language,
                                CategoryName = translationEntity.Value
                            });

                            await _categoryTranslationRepository.SaveAsync();

                        }

                    }


                    //проверь сдесь значения на налл Вордтранслейшен именно я пока курить 

                    IDataResult<int> checImageDictionaryExistResult = new DataResult<int>();

                    if (parsedResultEntity.Value?.ItemEntity?.MainSection?.WordTranslation != null)
                    {
                        checImageDictionaryExistResult =
                            await CheckImgDictionaryExist(parsedResultEntity.Value.ItemEntity.MainSection
                                .WordTranslation);

                        int imagDictionaryId;

                        if (checImageDictionaryExistResult.Success)
                        {
                            imagDictionaryId = checkCategoryExistResult.Data;
                        }
                        else
                        {
                            var imageDictionary = new ImageDictionary()
                            {
                                DisplayAtProgram = true,
                                CategoryId = categoryId,
                                RootImageId = null
                            };

                            await _imageDictionaryRepository.AddAsync(imageDictionary);
                            await _imageDictionaryRepository.SaveAsync();
                            imagDictionaryId = imageDictionary.Id;

                        }

                        IDataResult<List<string>> existingImageLanguageResult =
                            await CheckImageLanguagesExists(imagDictionaryId);

                        List<string> existingImgLangs = existingImageLanguageResult.Data;

                        if (parsedResultEntity.Value?.ItemEntity?.MainSection?.WordTranslation != null)
                        {
                            fullName =
                                LanguageHolder.GetLanguage(parsedResultEntity.Value.ItemEntity.MainSection
                                    .WordTranslation
                                    .Language);

                            //if (fullName != null)
                            //{
                            //    await AppendLangDictionary(
                            //        parsedResultEntity.Value.ItemEntity.MainSection.WordTranslation.Language, fullName);
                            //    if (existingImgLangs != null &&
                            //        existingImgLangs.All(p =>
                            //            p != parsedResultEntity.Value.ItemEntity.MainSection.WordTranslation.Language))
                            //    {
                            //        var mainImageDescriptionItem =
                            //            parsedResultEntity.Value.ItemEntity.MainSection;
                            //        await _imageDescriptionRepository.AddAsync(new ImageDescription()
                            //        {
                            //            ImageDictionaryId = imagDictionaryId,
                            //            IsMainTranslation = true,
                            //            LangDictionaryId = mainImageDescriptionItem.WordTranslation.Language,
                            //            Description = mainImageDescriptionItem.WordTranslation.Value
                            //        });

                            //        await _imageDescriptionRepository.SaveAsync();
                            //    }
                            //}

                            foreach (var imgDictionaryItem in parsedResultEntity.Value.ItemEntity.TranslationItemEntitys)
                            {
                                ImageDictionary itemForUpdate =
                                    await _imageDictionaryRepository.GetByIdAsync(imagDictionaryId);

                                itemForUpdate.SexType = imgDictionaryItem.SexType;
                                await _imageDictionaryRepository.UpdateAsync(itemForUpdate);

                                await _imageDictionaryRepository.SaveAsync();

                                foreach (ITranslationEntity translationEntity in imgDictionaryItem.WordTranslations)
                                {
                                    fullName =
                                        LanguageHolder.GetLanguage(translationEntity.Language);
                                    if (fullName != null)
                                    {
                                        await AppendLangDictionary(
                                            translationEntity.Language, fullName);
                                        if (existingImgLangs != null &&
                                            existingImgLangs.All(p =>
                                                p != translationEntity.Language)
                                        )
                                        {
                                            if (translationEntity.Language != parsedResultEntity.Value.ItemEntity
                                                    .MainSection.WordTranslation.Language)
                                            {
                                                await _imageDescriptionRepository.AddAsync(new ImageDescription()
                                                {
                                                    ImageDictionaryId = imagDictionaryId,
                                                    IsMainTranslation = false,
                                                    LangDictionaryId = translationEntity.Language,
                                                    Description = translationEntity.Value
                                                });
                                            }
                                            else
                                            {
                                                await _imageDescriptionRepository.AddAsync(new ImageDescription()
                                                {
                                                    ImageDictionaryId = imagDictionaryId,
                                                    IsMainTranslation = true,
                                                    LangDictionaryId = translationEntity.Language,
                                                    Description = translationEntity.Value
                                                });
                                            }

                                            await _imageDescriptionRepository.SaveAsync();
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }

        private async Task<IDataResult<int>> CheckImgDictionaryExist(ITranslationEntity enVariant)
        {
            ImageDescription imageDescription = await _imageDescriptionRepository.FirstOrDefaultAsync(p =>
                p.IsMainTranslation && p.Description == enVariant.Value);

            return new DataResult<int>()
            {
                Success = imageDescription != null,
                Data = imageDescription?.ImageDictionaryId ?? 0
            };
        }

        private async Task<IDataResult<List<string>>> CheckImageLanguagesExists(int imageDictionaryId)
        {
            List<ImageDescription> res =
                await _imageDescriptionRepository.FetchByAsync(p =>
                    p.ImageDictionaryId == imageDictionaryId);
            List<string> langIds = res?.Select(p => p.LangDictionaryId).ToList();

            return new DataResult<List<string>>()
            {
                Success = langIds != null,
                Data = langIds
            };
        }


        private async Task<IDataResult<int>> CheckCategoryExist(ITranslationEntity enVariant)
        {
            CategoryTranslation category = await _categoryTranslationRepository.FirstOrDefaultAsync(p =>
                p.IsMainTranslation && p.CategoryName == enVariant.Value);

            return new DataResult<int>()
            {
                Success = category != null,
                Data = category?.CategoryId ?? 0
            };
        }


        private async Task<IDataResult<List<string>>> CheckLanguagesExists(int categoryId)
        {
            List<CategoryTranslation> res =
                await _categoryTranslationRepository.FetchByAsync(p =>
                    p.CategoryId == categoryId);
            List<string> langIds = res?.Select(p => p.LangDictionaryId).ToList();

            return new DataResult<List<string>>()
            {
                Success = langIds != null,
                Data = langIds
            };
        }

        private async Task AppendLangDictionary(string id, string longLangName)
        {
            LangDictionary langEntity =
                await _langDictionaryRepository.GetByIdAsync(id);

            if (langEntity == null)
            {

                await _langDictionaryRepository.AddAsync(new LangDictionary()
                {
                    Id = id,
                    LongName = longLangName
                });

                await _langDictionaryRepository.SaveAsync();
            }
        }


        //private LangDictionary FindLang(string shortLang)
        //{
        //    using (ExcelContext excelContext = new ExcelContext())
        //    {
        //        return excelContext.LangDictionarys.FirstOrDefault(p => p.ShortName == shortLang);
        //    }
        //}


        //public void InitBD(string path)
        //{
        //    using (ExcelContext excelContext = new ExcelContext())
        //    {
        //        foreach (var keyValPair in LanguageHolder.LanguageDictionary)
        //        {

        //            excelContext.LangDictionarys.Add(new LangDictionary()
        //            {
        //                LongName = keyValPair.Value,
        //                ShortName = keyValPair.Key
        //            });
        //        }

        //        excelContext.SaveChanges();
        //    }


        //    IDataResult<IDataSheetResulHolder> result = _documentProccesor.Processor(path);
        //    //Dictionary < ITranslationEntity,List < ParsedResultEntity >> -base entity for category(section)
        //    //ParsedResultEntity -
        //    //    List < ITranslationEntity > CategoryTranslate

        //    //List < ItemEntity >
        //    //    ItemEntity

        //    //TranslationEntity Main

        //    //Lils < TranslationItemEntity > CurrentTranslations


        //    //TranslationItemEntity : TranslationEntity
        //    ////sexType
        //    ////


        //    //SiteMainLang - en

        //    //bool IsMain = LangID == SiteMainLang // true 

        //    using (ExcelContext excelContext = new ExcelContext())
        //    {
        //        if (result.Success)
        //        {
        //            Category categoryTranslation = new Category();
        //            //LangDictionary langDictionary = new LangDictionary();
        //            //Category translation = new Category();
        //            //ImageDictionary imageDictionary = new ImageDictionary();
        //            //imageDictionary
        //            int langId = 0;

        //            foreach (var item in result.Data.IndexTranslates)
        //            {
        //                Category category = new Category() {ImgPath = ""};
        //                excelContext.Categorys.Add(category);
        //                excelContext.SaveChanges();
        //                categoryTranslation.CategoryName = item.Key.Value;
        //                categoryTranslation.CategoryId = category.Id;
        //                categoryTranslation.LangDictionaryId = FindLang(item.Key.Language).Id;
        //                excelContext.SaveChanges();

        //                foreach (var value in item.Value)
        //                {
        //                    //langDictionary.ShortName = value.Language;
        //                    //langDictionary.LongName = LanguageHolder.GetLanguage(value.Language);
        //                    //langDictionary.Id = langId;

        //                    category = new Category() {ImgPath = ""};
        //                    excelContext.Categorys.Add(category);
        //                    excelContext.SaveChanges();
        //                    categoryTranslation.CategoryName = value.Value;
        //                    categoryTranslation.CategoryId = category.Id;
        //                    categoryTranslation.LangDictionaryId = FindLang(value.Language).Id;
        //                    excelContext.SaveChanges();
        //                }
        //            }

        //            foreach (var sheet in result.Data.DataSheets)
        //                    {
        //                        foreach (var rowItem in sheet.RowItems)
        //                        {
        //                            List<Tuple<int,string>> wordsWithLangs = 
        //                                new List<Tuple<int, string>>();
        //                            //int langIdForWords;
        //                            foreach (var column in rowItem.ColumnItems)
        //                            {
        //                                if (column.ColumnType == ColumnType.WorldSection)
        //                                {
        //                                    ITranslationEntity translationItem = 
        //                                        column.BaseEntity as ITranslationEntity;
        //                                    wordsWithLangs.Add(new Tuple<int, string>(FindLang(translationItem.Language).Id,
        //                                        translationItem.Value));    
        //                                }
        //                            }
        //                            foreach (Tuple<int, string> wordsWithLang in wordsWithLangs)
        //                            {
        //                                ImageDictionary imageDictionary = new ImageDictionary();
        //                                imageDictionary.CategoryTranslationId = categoryTranslation.Id;
        //                                imageDictionary.DisplayAtProgram = true;
        //                                imageDictionary.RootImageId = null;
        //                                excelContext.ImageDictionarys.Add(imageDictionary);
        //                                excelContext.SaveChanges();

        //                                ImageDescription imageDescription = new ImageDescription();
        //                                imageDescription.Description = wordsWithLang.Item2;
        //                                imageDescription.LangDictionaryId = wordsWithLang.Item1;
        //                                imageDescription.ImageDictionaryId = imageDescription.Id;

        //                                //imageDescription.
        //                                //imageDescription.Id = langId;
        //                                excelContext.ImageDescriptions.Add(imageDescription);
        //                                excelContext.SaveChanges();
        //                            }
        //                        }
        //                    }



        //                //imageDictionary.ImageDescriptions = discrptions;


        //            //foreach (var sheet in result.Data.DataSheets)
        //            //{
        //            //    foreach (var rowItem in sheet.RowItems)
        //            //    {
        //            //        foreach (var column in rowItem.ColumnItems)
        //            //        {

        //            //        }
        //            //    }
        //            //}
        //            //  imageDictionary.Category.Category.

        //            //excelContext.ImageDescriptions.Add(imageDescription)
        //        }


        //    }

        //}


        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
