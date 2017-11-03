using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Services;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.HelpEntity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.BL.Common;
using WorkWithExcel.BL.Entety;
using WorkWithExcel.BL.Entity;
using WorkWithExcel.BL.Entity.HelperEntity;

namespace WorkWithExcel.BL.Impl
{
    public class Division
    {
        private readonly IValidata _validata;

        public Division(IValidata validata)
        {
            _validata = validata;
        }

        public IDataResult<IBaseExelEntety> GetComponent(string path)
        {
            IDataResult<IBaseExelEntety> dataResult =
                new DataResult<IBaseExelEntety>();

            IResult result = _validata.ValidataExcel(path);

            if (!result.Success)
            {
                dataResult.Message = result.Message;
                dataResult.Success = result.Success;

                return dataResult;
            }

            IBaseExelEntety baseExelEntety = new BaseExelEntety();
            Dictionary<ITranslateSectionEntity, ITranslateEntity> dictionary = 
                new Dictionary<ITranslateSectionEntity, ITranslateEntity>();
            ExelConfiguration exelConfiguration = 
                ConfigurationHolder.ApiConfiguration;

            using (var file = File.Open(path, FileMode.Open))
            {
                using (var xls = new ExcelPackage(file))
                {
                    using (var sheet = xls.Workbook.Worksheets.FirstOrDefault())
                    {

                        for (int j = sheet.Dimension.Start.Row + 1; j <= sheet.Dimension.End.Row; j++)
                        {
                            // if (sheet.Cells[j, exelConfiguration.Section].Value == null) continue;

                            IExcelColor trackingHandler = new ExcelColor();

                            IExcelWorksheetEntity tmEntity = new ExcelWorksheetEntity();
                            tmEntity.ExcelWorksheet = sheet;
                            tmEntity.RowNo = j;
                            tmEntity.CellNo = exelConfiguration.DataColumn.Index;

                            trackingHandler.Index  =_validata.GetValue(tmEntity).Data;
                            tmEntity.CellNo = exelConfiguration.DataColumn.PageNomer;
                            trackingHandler.PageNomer = _validata.GetValue(tmEntity).Data;
                            
                            tmEntity.CellNo = exelConfiguration.DataColumn.Picture;

                            IDataResult<IExcelColor> dataResultColor =
                                _validata.GetColorValue(tmEntity);

                            if (dataResultColor.Success)
                            {
                                trackingHandler.R = dataResultColor.Data.R;
                                trackingHandler.B = dataResultColor.Data.B;
                                trackingHandler.G = dataResultColor.Data.G;
                            }
                            tmEntity.CellNo = exelConfiguration.DataColumn.Sex;
                            IDataResult<SexType> dataSexType = _validata.GetSexType(tmEntity);

                            if (!dataSexType.Success)
                            {
                                dataResult.Message += dataSexType.Message;
                            }
                            else
                            {
                                trackingHandler.SexType = dataSexType.Data;
                            }
                            tmEntity.CellNo = exelConfiguration.DataColumn.English;

                            IDataResult<Dictionary<string, string>> dataTranslate =
                                _validata.GetTranslateEntity(tmEntity);

                            if (!dataTranslate.Success)
                            {
                                dataResult.Message += dataTranslate.Message;
                            }
                            else
                            {
                                trackingHandler.TranslateDictionary = dataTranslate.Data;
                            }

                            ITranslateSectionEntity sectionEntity = new TranslateSectionEntity();

                            tmEntity.CellNo = exelConfiguration.DataColumn.SectionChiness;

                            IDataResult<Dictionary<string, string>> dataSectionTranslate =
                                _validata.GetTranslateEntity(tmEntity);

                            if (!dataSectionTranslate.Success)
                            {
                                dataResult.Message += dataSectionTranslate.Message;
                            }
                            else
                            {
                                sectionEntity.TranslateSection = dataSectionTranslate.Data;
                            }

                            dictionary.Add(sectionEntity,trackingHandler);
                        }     
                    }
                }
            }
            baseExelEntety.TranslateEntities = dictionary;
            dataResult.Success = true;
            dataResult.Data = baseExelEntety;

            return dataResult;
        }     
    }
}
