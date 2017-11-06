using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
        private readonly IGetExcelData _getExcelData;
        private readonly IDataNormalization _dataNormalization;
        private readonly ExcelConfiguration _excelConfiguration;

        public GetExcelSheetCongSection()
        {
            _getExcelData = new GetExcelData();
            _dataNormalization = new DataNormalization();
            _excelConfiguration = ConfigurationHolder.ApiConfiguration;
        }


        public IResult GetConfig(ExcelWorksheet excelWorksheet)
        {
            IResult result = new Result() { Success = false };
            DataColumn dataColumnIndex = _excelConfiguration.DataColumn;
            IExcelWorksheetEntity tmpEntity = new ExcelWorksheetEntity();
            tmpEntity.ExcelWorksheet = excelWorksheet;
            tmpEntity.RowNo = _excelConfiguration.DataRowIndex.Title;
            List<IResult> results = new List<IResult>();

            foreach (var valueProperty in dataColumnIndex.GetType().GetProperties())
            {
                Data data = (Data) valueProperty.GetValue(dataColumnIndex, null);
                tmpEntity.CellNo = data.Nomer;

                IDataResult<string> tmpDataResultValue = _getExcelData.GetValue(tmpEntity);

                //if (!tmpDataResultValue.Success)
                //{
                //    result.Message = tmpDataResultValue.Message;

                //    return result;
                //}

                string nameTitle = tmpDataResultValue.Data;
                IDataResult<string> resulrNormalizeString = 
                    _dataNormalization.NormalizeString(nameTitle);

                //if (!resulrNormalizeString.Success)
                //{
                //    result.Message = resulrNormalizeString.Message;

                //    return result;
                //}

                nameTitle = resulrNormalizeString.Data;

                string congiTitle = _dataNormalization.NormalizeString(data.Name).Data;
                IResult tmptResult = new Result();

                if (congiTitle.Equals(nameTitle))
                {
                    tmptResult.Success = true;                 
                    results.Add(tmptResult);
                }
                else
                {
                    tmptResult.Success = false;
                    results.Add(tmptResult);
                }

            }

            result.Success = !results.Any(p => p.Success== false);

            return result;
        }

        public IDataResult<ExcelConfiguration> GenerationConfig(ExcelWorksheet excelWorksheet)
        {
            IResult getConResult = GetConfig(excelWorksheet);
            IDataResult < ExcelConfiguration > dataResult = 
                new DataResult<ExcelConfiguration>();

            if (getConResult.Success)
            {
                dataResult.Success = true;
                dataResult.Data = _excelConfiguration;

                return dataResult;
            }

            IExcelWorksheetEntity tmpEntity = new ExcelWorksheetEntity();
            tmpEntity.ExcelWorksheet = excelWorksheet;
            tmpEntity.RowNo = _excelConfiguration.DataRowIndex.Title;
            DataColumn dataColumn = _excelConfiguration.DataColumn;
            ExcelConfiguration excelConfiguration = new ExcelConfiguration();
            excelConfiguration.DataColumn = new DataColumn();
            
            foreach (var valueProperty in dataColumn.GetType().GetProperties())
            {
                Data confiData = (Data) valueProperty.GetValue(dataColumn, null);
                string confiName = confiData.Name;
                confiName = _dataNormalization.NormalizeString(confiName).Data;
                bool genration = false;

                foreach (var valueNomer in dataColumn.GetType().GetProperties())
                {
                    Data confiDataNomer = (Data)valueNomer.GetValue(dataColumn, null);
                    int nomer = confiDataNomer.Nomer;

                    tmpEntity.CellNo = nomer;


                    IDataResult<string> tmpDataResultValue = _getExcelData.GetValue(tmpEntity);

                    if (!tmpDataResultValue.Success)
                    {
                        dataResult.Message = tmpDataResultValue.Message;
                        dataResult.Success = false;

                        return dataResult;
                    }

                    string nameTitle = tmpDataResultValue.Data;
                    string resulrNormalizeString =
                        _dataNormalization.NormalizeString(nameTitle).Data;

                    if (resulrNormalizeString.Equals(confiName))
                    {
                        // string str = dataColumn.Index.ToString();

                        switch (valueProperty.Name)
                        {
                            case "Index":
                                excelConfiguration.DataColumn.Index = new Data();
                                excelConfiguration.DataColumn.Index.Name = resulrNormalizeString;
                                excelConfiguration.DataColumn.Index.Nomer = nomer;
                                break;
                            case "Picture":
                                excelConfiguration.DataColumn.Picture = new Data();
                                excelConfiguration.DataColumn.Picture.Name = resulrNormalizeString;
                                excelConfiguration.DataColumn.Picture.Nomer = nomer;
                                break;
                            case "Page":
                                excelConfiguration.DataColumn.Page = new Data();
                                excelConfiguration.DataColumn.Page.Name = resulrNormalizeString;
                                excelConfiguration.DataColumn.Page.Nomer = nomer;
                                break;
                            case "Section":
                                excelConfiguration.DataColumn.Section = new Data();
                                excelConfiguration.DataColumn.Section.Name = resulrNormalizeString;
                                excelConfiguration.DataColumn.Section.Nomer = nomer;
                                break;
                            case "Sex":
                                excelConfiguration.DataColumn.Sex = new Data();
                                excelConfiguration.DataColumn.Sex.Name = resulrNormalizeString;
                                excelConfiguration.DataColumn.Sex.Nomer = nomer;
                                break;
                            case "Language":
                                excelConfiguration.DataColumn.Language = new Data();
                                excelConfiguration.DataColumn.Language.Name = resulrNormalizeString;
                                excelConfiguration.DataColumn.Language.Nomer = nomer;
                                break;
                        }

                        genration = true;
                    }
                }

                if (!genration)
                {
                    dataResult.Success = false;
                    dataResult.Message = MessageHolder.GetErrorMessage(MessageType.IsNullOrEmpty);
                }
            }
            dataResult.Success = true;
           dataResult.Data = excelConfiguration;

            return dataResult;
        }
    }
}
