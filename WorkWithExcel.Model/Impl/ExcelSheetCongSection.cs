using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using WorkWithExcel.Abstract.BL;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;
using WorkWithExcel.Abstract.Entity.HelpEntity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.Model.Common;
using WorkWithExcel.Model.Entity.HelperEntity;

namespace WorkWithExcel.Model.Impl
{
    public class ExcelSheetCongSection : IGetExcelSheetCongSection
    {
        private readonly IReadExcelData _readExcelData;
        private readonly IDataNormalization _dataNormalization;
        private readonly ExcelConfiguration _excelConfiguration;

        public ExcelSheetCongSection()
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

            foreach (var data in _excelConfiguration.DataColumn.Datas)
            {            
                tmpEntity.CellNo = data.Nomer;
                results.Add(HelpGetConfig(data, tmpEntity));
            }

            result.Success = results.All(p => p.Success);

            return result;
        }

        public IDataResult<ExcelConfiguration> GeneratExcelConfig(ExcelWorksheet excelWorksheet)
        {
            IDataResult<ExcelConfiguration> dataResult =
                new DataResult<ExcelConfiguration>();
            ExcelConfiguration excelConfiguration = new ExcelConfiguration();
            excelConfiguration.DataColumn = new DataColumn();
            IExcelWorksheetEntity tmpEntity = new ExcelWorksheetEntity();
            tmpEntity.ExcelWorksheet = excelWorksheet;
            tmpEntity.RowNo = _excelConfiguration.DataRowIndex.Title;

            List<IDataResult<Data>> dataResults = new List<IDataResult<Data>>();

            int endColumn = excelWorksheet.Dimension.End.Column;

            StringBuilder messegeBuilder = new StringBuilder();

            foreach (var data in _excelConfiguration.DataColumn.Datas)
            {
                IDataResult<Data> tmResultHelper = HelperGenerat(data, tmpEntity, endColumn);

                if (!tmResultHelper.Success)
                {
                    messegeBuilder.Append(data.Name + MessageHolder.GetErrorMessage(MessageType.Space));
                }
                dataResults.Add(tmResultHelper);
               
            }
            excelConfiguration.DataColumn.Datas = dataResults.Select(p => p.Data).ToList();

            if (dataResults.Any(p => p.Success == false))
            {
                dataResult.Success = false;
                dataResult.Message = MessageHolder.
                              GetErrorMessage(MessageType.NotIsTitle) + 
                              MessageHolder.GetErrorMessage(MessageType.Space)
                              +messegeBuilder +
                               MessageHolder.GetErrorMessage(MessageType.BackBracket);

                return dataResult;
            }

            excelConfiguration.DataRowIndex = _excelConfiguration.DataRowIndex;
            excelConfiguration.NameColumnSection = _excelConfiguration.NameColumnSection;
            dataResult.Data = excelConfiguration;
            dataResult.Success = true;

            return dataResult;
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


        private IDataResult<Data> HelperGenerat
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
