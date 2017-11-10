using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.Model.Common;
using WorkWithExcel.Model.Entity;

namespace WorkWithExcel.Model.Implement
{
    public class DataNormalization : IDataNormalization
    {
        public IDataResult<IBaseExelEntety> Normalize(Dictionary<string, IDataExcelEntity> translateEntities)
        {
            throw new NotImplementedException();
        }

        public IDataResult<string> NormalizeString(string data)
        {
            IDataResult<string> dataResult =
                new DataResult<string>() { Success = false };

            if (string.IsNullOrEmpty(data))
            {
                dataResult.Message = MessageHolder.
                    GetErrorMessage(MessageType.IsNullOrEmpty);

                return dataResult;
            }

            data = Remove(data, "/");
            data = Replace(data, " ");
            data = data.ToLower();
            dataResult.Success = true;
            dataResult.Data = data;

            return dataResult;
        }

        //public IDataResult<Dictionary<IDataExcelEntity, List<ITranslationEntity>>>
        //    NormaliseTransliteWord(List<IRowItem> listRowItems)
        //{
        //    Dictionary<IDataExcelEntity, List<ITranslationEntity>> transltionDictionary;
        //    IDataResult<Dictionary<IDataExcelEntity, List<ITranslationEntity>>> resultTranslations =
        //        new DataResult<Dictionary<IDataExcelEntity, List<ITranslationEntity>>>();

        //    IDataResult<Dictionary<IDataExcelEntity, List<ITranslationEntity>>> dataResultSection =
        //        HelperNormaliseTransliteWord(listRowItems);

        //    if (!dataResultSection.Success)
        //    {
        //        resultTranslations.Message = dataResultSection.Message;
        //        resultTranslations.Success = false;

        //        return resultTranslations;
        //    }

        //    transltionDictionary = dataResultSection.Data;

        //    resultTranslations.Success = true;
        //    resultTranslations.Data = transltionDictionary;

        //    return resultTranslations;
        //}

        public IDataResult<Dictionary<ITranslationEntity, List<ITranslationEntity>>>
            NormaliseTransliteSection(List<IRowItem> listRowItems)
        {
            Dictionary<ITranslationEntity, List<ITranslationEntity>> transltionDictionary;
            IDataResult<Dictionary<ITranslationEntity, List<ITranslationEntity>>> resultTranslations =
                new DataResult<Dictionary<ITranslationEntity, List<ITranslationEntity>>>();
            IDataResult<Dictionary<ITranslationEntity, List<ITranslationEntity>>> dataResultWord =
                HelperNormaliseTransliteSection(listRowItems);

            if (!dataResultWord.Success)
            {
                resultTranslations.Message = dataResultWord.Message;
                resultTranslations.Success = false;

                return resultTranslations;
            }

            transltionDictionary = dataResultWord.Data;

            resultTranslations.Success = true;
            resultTranslations.Data = transltionDictionary;

            return resultTranslations;
        }

        private IDataResult<Dictionary<ITranslationEntity, List<ITranslationEntity>>>
            HelperNormaliseTransliteSection(List<IRowItem> listRowItems)
        {
            Dictionary<ITranslationEntity, List<ITranslationEntity>> sections =
                 new Dictionary<ITranslationEntity, List<ITranslationEntity>>();

            IDataResult<Dictionary<ITranslationEntity, List<ITranslationEntity>>> dataResult =
                new DataResult<Dictionary<ITranslationEntity, List<ITranslationEntity>>>();

            foreach (var rowItem in listRowItems)
            {
                List<ITranslationEntity> translationEntities = new List<ITranslationEntity>();
                ITranslationEntity translationKey = new TranslationEntity();

                foreach (var columnItem in rowItem.ColumnItems)
                {
                    ITranslationEntity entity = columnItem.BaseEntity as ITranslationEntity;

                    switch (columnItem.ColumnType)
                    {
                        case ColumnType.Section:

                            if (entity != null)
                            {
                                translationKey.Value = entity.Value;
                                translationKey.Language = entity.Language;
                            }
                            break;

                        case ColumnType.SectionTransfer:
                            ITranslationEntity tmpEntity = new TranslationEntity();
                            tmpEntity.Language = entity.Language;
                            tmpEntity.Value = entity.Value;
                            translationEntities.Add(tmpEntity);
                            break;

                    }
                }

                sections.Add(translationKey, translationEntities);
            }
            dataResult.Success = true;
            dataResult.Data = sections;

            return dataResult;
        }

        //private IDataResult<Dictionary<IDataExcelEntity, List<ITranslationEntity>>>
        //    HelperNormaliseTransliteWord(List<IRowItem> listRowItems)
        //{
        //    Dictionary<IDataExcelEntity, List<ITranslationEntity>> words =
        //        new Dictionary<IDataExcelEntity, List<ITranslationEntity>>();

        //    IDataResult<Dictionary<IDataExcelEntity, List<ITranslationEntity>>> dataResult =
        //        new DataResult<Dictionary<IDataExcelEntity, List<ITranslationEntity>>>();

        //    foreach (var rowItem in listRowItems)
        //    {
        //        List<ITranslationEntity> translationEntities = new List<ITranslationEntity>();
        //        IDataExcelEntity translationKey = new DataExcelEntity();

        //        foreach (var columnItem in rowItem.ColumnItems)
        //        {
        //            switch (columnItem.ColumnType)
        //            {
        //                case ColumnType.Language:
        //                    translationKey.Value = columnItem.Value;
        //                    translationKey.NameTitle = columnItem.NameTitle;
        //                    break;
        //                case ColumnType.Index:
        //                    translationKey.Index = columnItem.Value;
        //                    break;
        //                case ColumnType.Page:
        //                    translationKey.PageNomer = columnItem.Value;
        //                    break;
        //                case ColumnType.Picture:
        //                    IExcelColor excelColor = new ExcelColor();

        //                    if (!string.IsNullOrEmpty(columnItem.Value))
        //                    {
        //                        Color color = ColorTranslator.FromHtml("#" + columnItem.Value);
        //                        excelColor.R = color.R;
        //                        excelColor.G = color.G;
        //                        excelColor.B = color.B;
        //                        translationKey.ExcelColor = excelColor;
        //                    }
        //                    else
        //                    {
        //                        translationKey.PathImage = columnItem.Value;
        //                    }
        //                    break;
        //                case ColumnType.Sex:

        //                    if (columnItem.Value != null)
        //                    {
        //                        foreach (var sexTypeValue in Enum.GetValues(typeof(SexType)))
        //                        {
        //                            if (sexTypeValue.ToString().ToLower().Equals(columnItem.Value.ToLower()))
        //                            {
        //                                translationKey.SexType = (SexType)sexTypeValue;
        //                                break;
        //                            }
        //                        }
        //                    }
        //                    break;
        //                case ColumnType.WorldSection:
        //                    ITranslationEntity tmpEntity = new TranslationEntity();
        //                    tmpEntity.NameTitle = columnItem.NameTitle;
        //                    tmpEntity.Value = columnItem.Value;
        //                    translationEntities.Add(tmpEntity);

        //                    break;
        //            }
        //        }

        //        if (translationEntities.Any() && translationKey.NameTitle != null)
        //        {
        //            words.Add(translationKey, translationEntities);
        //        }
        //    }
        //    dataResult.Success = true;
        //    dataResult.Data = words;

        //    return dataResult;
        //}

        private string Replace(string data, string sym)
        {
            while (data.Contains(sym))
            {
                data = data.Replace(sym, "");
            }

            return data;
        }

        private string Remove(string data, string sym)
        {
            int index = data.IndexOf(sym, StringComparison.Ordinal);

            if (index > -1)
            {
                data = data.Remove(index);
            }

            return data;
        }
    }
}
