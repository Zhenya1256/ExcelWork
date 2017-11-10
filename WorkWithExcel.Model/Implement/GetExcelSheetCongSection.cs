using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;
using WorkWithExcel.Abstract.Entity.HelpEntity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.Model.Common;
using WorkWithExcel.Model.Entity.HelperEntity;


namespace WorkWithExcel.Model.Implement
{
    public class GetExcelSheetCongSection : IGetExcelSheetCongSection
    {
        private readonly IReadExcelData _readExcelData;
        private readonly IDataNormalization _dataNormalization;
        private readonly ExcelConfiguration _excelConfiguration;
        
        public GetExcelSheetCongSection()
        {
            _readExcelData = new ReadExcelData();
            _dataNormalization = new DataNormalization();
            _excelConfiguration = ConfigurationHolder.ApiConfiguration;
        }

        public IResult GetExcelConfig(ExcelWorksheet excelWorksheet)
        {
            IResult result = new Result() { Success = false };
            List<IResult> results = new List<IResult>();

            IExcelWorksheetEntity tmpEntity = new ExcelWorksheetEntity();
            tmpEntity.ExcelWorksheet = excelWorksheet;
            tmpEntity.RowNo = _excelConfiguration.DataRowIndex.Title;
            //
            Data data = _excelConfiguration.DataColumn.Index;
            tmpEntity.CellNo = data.Nomer;
            results.Add(HelpGetConfig(data, tmpEntity));
            //
            data = _excelConfiguration.DataColumn.Page;
            tmpEntity.CellNo = data.Nomer;
            results.Add(HelpGetConfig(data, tmpEntity));
            //
            data = _excelConfiguration.DataColumn.Language;
            tmpEntity.CellNo = data.Nomer;
            results.Add(HelpGetConfig(data, tmpEntity));
            //
            data = _excelConfiguration.DataColumn.Picture;
            tmpEntity.CellNo = data.Nomer;
            results.Add(HelpGetConfig(data, tmpEntity));
            //
            data = _excelConfiguration.DataColumn.Sex;
            tmpEntity.CellNo = data.Nomer;
            results.Add(HelpGetConfig(data, tmpEntity));
            //
            data = _excelConfiguration.DataColumn.Section;
            tmpEntity.CellNo = data.Nomer;
            results.Add(HelpGetConfig(data, tmpEntity));


            result.Success = results.All(p => p.Success);

            return result;
        }

        private IResult HelpGetConfig
            (Data data, IExcelWorksheetEntity tmpEntity)
        {
            IResult result = new Result() { Success = false };
            IDataResult<string> tmpDataResultValue =
                _readExcelData.GetValue(tmpEntity);
            string nameTitle = tmpDataResultValue.Data;
            nameTitle =
                _dataNormalization.NormalizeString(nameTitle).Data;
            string congiTitle =
                _dataNormalization.NormalizeString(data.Name).Data;

            if (congiTitle.Equals(nameTitle))
            {
                result.Success = true;
            }

            return result;
        }

        public IDataResult<ExcelConfiguration>
            GenerationExcelConfig(ExcelWorksheet excelWorksheet)
        {
            IDataResult<ExcelConfiguration> dataResult =
                new DataResult<ExcelConfiguration>();
            ExcelConfiguration excelConfiguration = new ExcelConfiguration();
            excelConfiguration.DataColumn = new DataColumn();


            IExcelWorksheetEntity tmpEntity = new ExcelWorksheetEntity();
            tmpEntity.ExcelWorksheet = excelWorksheet;
            tmpEntity.RowNo = _excelConfiguration.DataRowIndex.Title;
            //
            List<IDataResult<Data>> dataResults = new List<IDataResult<Data>>();

            int endColumn = excelWorksheet.Dimension.End.Column;
            //
            StringBuilder messegeBuilder= new StringBuilder();
            Data data = _excelConfiguration.DataColumn.Index;
            IDataResult<Data> tmResultHelper = HelperGeneration(data, tmpEntity, endColumn);

            if (!tmResultHelper.Success)
            {
                messegeBuilder.Append(data.Name + " ");
            }

            dataResults.Add(tmResultHelper);
            excelConfiguration.DataColumn.Index = tmResultHelper.Data;
            //
            data = _excelConfiguration.DataColumn.Language;
            tmResultHelper = HelperGeneration(data, tmpEntity, endColumn);

            if (!tmResultHelper.Success)
            {
                messegeBuilder.Append(data.Name + " ");
            }

            dataResults.Add(tmResultHelper);
            excelConfiguration.DataColumn.Language = tmResultHelper.Data;
            //
            data = _excelConfiguration.DataColumn.Page;
            tmResultHelper = HelperGeneration(data, tmpEntity, endColumn);

            if (!tmResultHelper.Success)
            {
                messegeBuilder.Append(data.Name + " ");
            }

            dataResults.Add(tmResultHelper);
            excelConfiguration.DataColumn.Page = tmResultHelper.Data;
            //
            data = _excelConfiguration.DataColumn.Picture;
            tmResultHelper = HelperGeneration(data, tmpEntity, endColumn);

            if (!tmResultHelper.Success)
            {
                messegeBuilder.Append(data.Name + " ");
            }

            dataResults.Add(tmResultHelper);
            excelConfiguration.DataColumn.Picture = tmResultHelper.Data;
            //
            data = _excelConfiguration.DataColumn.Section;
            tmResultHelper = HelperGeneration(data, tmpEntity, endColumn);

            if (!tmResultHelper.Success)
            {
                messegeBuilder.Append(data.Name + " ");
            }

            dataResults.Add(tmResultHelper);
            excelConfiguration.DataColumn.Section = tmResultHelper.Data;
            //
            data = _excelConfiguration.DataColumn.Sex;
            tmResultHelper = HelperGeneration(data, tmpEntity, endColumn);

            if (!tmResultHelper.Success)
            {
                messegeBuilder.Append(data.Name + " ");
            }

            dataResults.Add(tmResultHelper);
            excelConfiguration.DataColumn.Sex = tmResultHelper.Data;

            if (dataResults.Any(p =>p.Success == false))
            {
                dataResult.Success = false;
                dataResult.Message = MessageHolder.
                    GetErrorMessage(MessageType.NotIsTitle)+" "+ messegeBuilder+")";

                return dataResult;
            }
            excelConfiguration.DataRowIndex = _excelConfiguration.DataRowIndex;
            dataResult.Data = excelConfiguration;
            dataResult.Success = true;

            return dataResult;
        }

        private IDataResult<Data> HelperGeneration
            (Data data, IExcelWorksheetEntity tmpEntity, int endColumn)
        {
            IDataResult<Data> dataResult =
                new DataResult<Data>() { Success = false };
            string nameTitle = data.Name;
            nameTitle =
                _dataNormalization.NormalizeString(nameTitle).Data;

            for (int i = 1; i <= endColumn; i++)
            {
                tmpEntity.CellNo = i;
                IDataResult<string> tmpDataResultValue =
                    _readExcelData.GetValue(tmpEntity);

                if (!tmpDataResultValue.Success)
                {
                    continue;
                }

                string nameExcelTitle = tmpDataResultValue.Data;
                nameExcelTitle = _dataNormalization.NormalizeString(nameExcelTitle).Data;
                nameTitle =
                    _dataNormalization.NormalizeString(nameTitle).Data;


                if (nameExcelTitle.Equals(nameTitle))
                {
                    data.Nomer = i;
                    dataResult.Success = true;
                    dataResult.Data = data;

                    return dataResult;
                }

            }

            return dataResult;
        }
    }
}
