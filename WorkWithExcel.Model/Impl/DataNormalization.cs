using System;
using System.Collections.Generic;
using WorkWithExcel.Abstract.BL;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.Model.Common;
using WorkWithExcel.Model.Entity;

namespace WorkWithExcel.Model.Impl
{
    public class DataNormalization : IDataNormalization
    {
     
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

        private string Replace(string data, string sym)
        {
            while (data.Contains(sym))
            {
                data = data.Replace(sym, string.Empty);
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
