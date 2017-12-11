using System.Collections.Generic;
using System.Linq;
using WorkWithExcel.Abstract.BL;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.NormalizeEntity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.Model.Common;
using WorkWithExcel.Model.Entity.NormalizeEntity;

namespace WorkWithExcel.Model.Impl
{
    public class NormalizeData : INormalizeData
    {
        private readonly IExcelDocumentProccesor _documentProccesor;
        private readonly ExcelConfiguration _excelConfiguration;
        private readonly IDataNormalization _dataNormalization;

        public NormalizeData()
        {
            _documentProccesor = new ExcelDocumentProccesor();
            _excelConfiguration = ConfigurationHolder.ApiConfiguration;
            _dataNormalization = new DataNormalization();
        }

        public IDataResult<IBaseEntityCategory> Normalize(string path)
        {
            IDataResult<IBaseEntityCategory> result = new DataResult<IBaseEntityCategory>();
            IBaseEntityCategory baseEntityCategory = new BaseEntityCategory();

            Dictionary<ITranslationEntity, IParsedResultEntity> sectionsDictionary =
                new Dictionary<ITranslationEntity, IParsedResultEntity>();

            IDataResult<IDataSheetResulHolder> dataResult = _documentProccesor.Processor(path);

            if (!dataResult.Success)
            {
                result.Success = true;

                return result;
            }

            IDataSheetResulHolder dataSheet = dataResult.Data;

            switch (dataSheet.ExcelDocumentType)
            {
                case ExcelDocumentType.IndexPage:
                    sectionsDictionary = HelpIndexPage(dataSheet,dataSheet.IndexTranslates);
                    break;

                case ExcelDocumentType.WithoutIndexPage:
                    sectionsDictionary = HelpWithoutIndexPage(dataSheet);
                    break;
            }

            baseEntityCategory.Categotis = sectionsDictionary;
            baseEntityCategory.IsMainLang = GatLang(0);
            result.Data = baseEntityCategory;
            result.Success = true;

            return result;
        }

        public bool GatLang(int langId)
        {
            string shotName = LanguageHolder.GetISOCodes
                (_excelConfiguration.NameColumnSection.MainLanguage, _dataNormalization);
            //TODO

            return true;
        }

        private Dictionary<ITranslationEntity, IParsedResultEntity>
            HelpIndexPage(IDataSheetResulHolder dataSheet,
            Dictionary<ITranslationEntity, List<ITranslationEntity>> translates)
        {
            Dictionary<ITranslationEntity, IParsedResultEntity> sectionsDictionary =
                new Dictionary<ITranslationEntity, IParsedResultEntity>();

            foreach (var keyValue in translates)
            {
                IItemEntity temItemEntities = new ItemEntity();
                IParsedResultEntity parsedResult = new ParsedResultEntity();

                parsedResult.CategoryTranslate = keyValue.Value;
                List<ITranslationItemEntity> translationItemEntitys =
                    new List<ITranslationItemEntity>();

                foreach (var sheet in dataSheet.DataSheets)
                {
                    foreach (var row in sheet.RowItems)
                    {
                        IColumnItem columnItem =
                            row.ColumnItems.FirstOrDefault(p => p.ColumnType == ColumnType.Section);
                        ITranslationEntity tempEntity = (ITranslationEntity)columnItem?.BaseEntity;

                        if (tempEntity?.Value != null && keyValue.Key.Value!=null && 
                            tempEntity.Value.Equals(keyValue.Key.Value))
                        {
                            ITranslationItemEntity teTranslationItemEntity =
                                new TranslationItemEntity();
                            List<ITranslationEntity> translationEntities =
                                new List<ITranslationEntity>();

                            foreach (var column in row.ColumnItems)
                            {

                                switch (column.ColumnType)
                                {
                                    case ColumnType.Index:
                                        teTranslationItemEntity.Index = column.BaseEntity.Value;
                                        break;
                                    case ColumnType.Page:
                                        teTranslationItemEntity.Page = column.BaseEntity.Value;
                                        break;
                                    case ColumnType.Sex:
                                        teTranslationItemEntity.SexType = column.BaseEntity.Value;
                                        break;
                                    case ColumnType.Picture:
                                        teTranslationItemEntity.ExcelColor = (IExcelColor)column.BaseEntity;
                                        break;
                                    case ColumnType.Language:
                                        translationEntities.Add((ITranslationEntity)column.BaseEntity);
                                        break;
                                    case ColumnType.WorldSection:
                                        translationEntities.Add((ITranslationEntity)column.BaseEntity);
                                        break;
                                }
                            }

                            teTranslationItemEntity.WordTranslations = translationEntities;
                            translationItemEntitys.Add(teTranslationItemEntity);
                        }
                    }  
                }

                if (translationItemEntitys.Any())
                {
                    temItemEntities.MainSection = new RootTranslationItemEntity();
                    temItemEntities.MainSection.WordTranslation = keyValue.Key;
                    temItemEntities.TranslationItemEntitys = translationItemEntitys;
                }

                parsedResult.ItemEntity = temItemEntities;
                sectionsDictionary.Add(keyValue.Key, parsedResult);
            }

            return sectionsDictionary;
        }

        private Dictionary<ITranslationEntity,IParsedResultEntity>
            HelpWithoutIndexPage(IDataSheetResulHolder dataSheet)
        {
            Dictionary< ITranslationEntity , List< ITranslationEntity >> dictionarySections = 
                new Dictionary<ITranslationEntity, List<ITranslationEntity>>();

            foreach (var sheet in dataSheet.DataSheets)
            {
                foreach (var rowItem in sheet.RowItems)
                {
                    ITranslationEntity translation =(ITranslationEntity) rowItem.ColumnItems
                        .FirstOrDefault(p=>p.ColumnType==ColumnType.Section)?.BaseEntity; 

                    List < ITranslationEntity > translationEntities = 
                        new List<ITranslationEntity>();

                    foreach (var columnItem in rowItem.ColumnItems)
                    {
                        if (columnItem.ColumnType == ColumnType.SectionTransfer)
                        {
                            translationEntities.Add((ITranslationEntity)columnItem.BaseEntity);
                        }
                    }

                    if (translation != null)
                    {
                        if (!dictionarySections.ContainsKey(translation))
                        {
                            dictionarySections.Add(translation, translationEntities);
                        }
                    }
                }
            }

            Dictionary<ITranslationEntity,IParsedResultEntity> sectionsDictionary 
                = HelpIndexPage(dataSheet, dictionarySections);

            return sectionsDictionary;
        }
    }
}
