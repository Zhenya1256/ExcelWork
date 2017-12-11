using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OfficeOpenXml;
using WorkWithExcel.Abstract.BL;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.HelpEntity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.Model.Common;
using WorkWithExcel.Model.Entity;
using WorkWithExcel.Model.Entity.HelperEntity;

namespace WorkWithExcel.Model.Impl
{
    public class Parser : IParser
    {
        private readonly IReadExcelData _readExcelData;
        private readonly IDataNormalization _dataNormalization;

        public Parser()
        {
            _readExcelData = new ReadExcelData();
            _dataNormalization = new DataNormalization();
        }

        public IDataResult<IRowItem> RowParser
            (ExcelWorksheet excelWorksheet, int row, ExcelConfiguration excelConfiguration)
        {
            IDataResult<IRowItem> dataResult =
                new DataResult<IRowItem>() { Success = false };
            IRowItem rowItem = new RowItem();
            IRowItemError error = new RowItemError();

            List<IColumnItem> columnItems = new List<IColumnItem>();
            List<IDataResult<IColumnItem>> errorColumnItems = new List<IDataResult<IColumnItem>>();
            int start = excelWorksheet.Dimension.Start.Column;
            int end = excelWorksheet.Dimension.Columns;

            for (int j = start; j <end; j++)
            {
                IExcelWorksheetEntity tmpEntity = new ExcelWorksheetEntity();
                tmpEntity.CellNo = j;
                tmpEntity.RowNo = row;
                //
                IExcelWorksheetEntity titleEntity = new ExcelWorksheetEntity();
                titleEntity.RowNo = excelConfiguration.DataRowIndex.Title;
                titleEntity.CellNo = j;
                titleEntity.ExcelWorksheet = excelWorksheet;

                IDataResult<string> nametitleResilt = _readExcelData.GetValue(titleEntity);

                if (!nametitleResilt.Success)
                {
                    break;
                }
                tmpEntity.ExcelWorksheet = excelWorksheet;

                IDataResult<IColumnItem> getDataResult =
                    ColumnParser(tmpEntity, excelConfiguration);

                dataResult.Message += getDataResult.Message;

                if (getDataResult.Success)
                {
                    //if (!string.IsNullOrEmpty(getDataResult.Message))
                    //{
                    //    getDataResult.Success = false;
                    //    errorColumnItems.Add(getDataResult);
                    //}
                    //else
                    {
                        columnItems.Add(getDataResult.Data);
                    }
                }
                else
                {
                    errorColumnItems.Add(getDataResult);
                }
            }
            error.ListColums = errorColumnItems;

            if (errorColumnItems.Any())
            {
                dataResult.Data = error;
                dataResult.Success = false;

                return dataResult;
            }

            rowItem.ColumnItems = columnItems;
            dataResult.Data = rowItem;
            dataResult.Success = true;

            return dataResult;
        }

        public IDataResult<IColumnItem> ColumnParser
            (IExcelWorksheetEntity excelWorksheet, ExcelConfiguration excelConfiguration)
        {
            IDataResult<IColumnItem> dataResult =
                new DataResult<IColumnItem>() { Success = false };

            IColumnItem columnItem = new ColumnItem();
            int column = excelWorksheet.CellNo;
            IDataResult<string> resultValue = _readExcelData.GetValue(excelWorksheet);
            int nomertitle = excelConfiguration.DataRowIndex.Title;

            IExcelWorksheetEntity tmpExcel = new ExcelWorksheetEntity();
            tmpExcel.RowNo = nomertitle;
            tmpExcel.ExcelWorksheet = excelWorksheet.ExcelWorksheet;
            tmpExcel.CellNo = column;
            string nameTitle = _readExcelData.GetValue(tmpExcel).Data;
            string configNameSection = excelConfiguration.DataColumn.Datas
                .FirstOrDefault(p => p.ColumnType == (int)ColumnType.Section)?.Name;
            configNameSection = _dataNormalization.NormalizeString(configNameSection).Data;
            columnItem.BaseEntity = new BaseEntity();
            bool isResult = false;

            foreach (var data in excelConfiguration.DataColumn.Datas)
            {
                if (column == data.Nomer)
                {
                    if (!resultValue.Success)
                    {
                        if (data.MustExist)
                        {
                            dataResult.Message += resultValue.Message;
                        }
                    }

                    columnItem.ColumnType = (ColumnType)data.ColumnType;
                    columnItem.BaseEntity = GetBaseEntity
                        (excelWorksheet, resultValue.Data, column, excelConfiguration);
                    isResult = true;
                }

            }
            if (!isResult)
            {
                nameTitle = _dataNormalization.NormalizeString(nameTitle).Data;

          
                if (nameTitle == null)
                {
                    //TODO
                    dataResult.Success = true;

                    return dataResult;
                }

                if (nameTitle.Contains(configNameSection))
                {
                    columnItem.ColumnType = ColumnType.SectionTransfer;
                    nameTitle = nameTitle.Replace(configNameSection, string.Empty);
                }
                else
                {
                    columnItem.ColumnType = ColumnType.WorldSection;
                }
                dataResult.Message += resultValue.Message;
                string language = LanguageHolder.GetISOCodes(nameTitle, _dataNormalization);

                ITranslationEntity entity = new TranslationEntity();
                entity.Language = language;
                entity.Value = resultValue.Data;
                columnItem.BaseEntity = entity;
            }

            columnItem.ColumNumber = column;
            dataResult.Data = columnItem;
            dataResult.Success = true;


            return dataResult;
        }

        private IBaseEntity GetBaseEntity(IExcelWorksheetEntity excelWorksheet,
            string data, int column, ExcelConfiguration excelConfiguration)
        {
            IBaseEntity baseEntity = new BaseEntity() { Value = data };

            if (column == excelConfiguration.DataColumn
                    .Datas.FirstOrDefault(p => p.ColumnType == (int)ColumnType.Section)?.Nomer
                    || column == excelConfiguration.DataColumn.Datas
                    .FirstOrDefault(p => p.ColumnType == (int)ColumnType.Language)?.Nomer)
            {
                string name = excelConfiguration.NameColumnSection.MainLanguage;
                ITranslationEntity entity = new TranslationEntity();
                entity.Language = LanguageHolder.GetISOCodes(name, _dataNormalization);
                entity.Value = data;

                return entity;
            }

            if (column == excelConfiguration.DataColumn.Datas
                    .FirstOrDefault(p => p.ColumnType == (int)ColumnType.Picture)?.Nomer)
            {
                IDataResult<string> colorNameResult =
                    _readExcelData.GetColorValue(excelWorksheet);
                IExcelColor excelColor = new ExcelColor();
                if (colorNameResult.Success)
                {
                    Color color = ColorTranslator.FromHtml("#" + colorNameResult.Data);
                    excelColor.R = color.R;
                    excelColor.G = color.G;
                    excelColor.B = color.B;
                }

                return excelColor;
            }

            return baseEntity;
        }

        public IDataResult<List<IColumnItem>> GetCulumnTitleItem
            (ExcelWorksheet sheet, ExcelConfiguration excelConfiguration)
        {
            IDataResult<List<IColumnItem>> columnsResult =
                new DataResult<List<IColumnItem>>() { Success = true };
            List<IColumnItem> columnItems = new List<IColumnItem>();

            int start = sheet.Dimension.Start.Column;
            int end = sheet.Dimension.Columns;
            int row = excelConfiguration.DataRowIndex.Title;
            IExcelWorksheetEntity entity = new ExcelWorksheetEntity();
            entity.ExcelWorksheet = sheet;
            entity.RowNo = row;

            for (int i = start; i < end; i++)
            {
                entity.CellNo = i;
                IDataResult<string> nametitleResilt = _readExcelData.GetValue(entity);

                if (!nametitleResilt.Success)
                {
                    break;
                }

                IColumnItem column = new ColumnItem();
                column.BaseEntity = new BaseEntity();
                column.BaseEntity.Value = nametitleResilt.Data;
                column.ColumNumber = i;
                column.ColumnType = ColumnType.None;
                columnItems.Add(column);
            }

            columnsResult.Data = columnItems;

            return columnsResult;       
    }

    public int RowCount { get; set; }
    }
}
