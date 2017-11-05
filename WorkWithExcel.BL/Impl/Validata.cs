using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using WorkWithExcel.Abstract.Abstract;
using WorkWithExcel.Abstract.Common;
using WorkWithExcel.Abstract.Common.Config;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.HelpEntity;
using WorkWithExcel.Abstract.Enums;
using WorkWithExcel.Abstract.Holder;
using WorkWithExcel.BL.Common;
using WorkWithExcel.BL.Entity;
using WorkWithExcel.BL.Entity.HelperEntity;

namespace WorkWithExcel.BL.Impl
{
    public class Validata : IValidata
    {
        private readonly ExelConfiguration _exelConfiguration;

        public Validata()
        {
            _exelConfiguration =
                ConfigurationHolder.ApiConfiguration;
        }

        public IResult ValidataExcel(string path)
        {
            IResult result = new Result() { Success = false };
            if (string.IsNullOrEmpty(path))
            {
                result.Message = MessageHolder.
                    GetErrorMessage(MessageType.FileIsempty);

                return result;
            }

            if (Path.GetExtension(path) != ".xlsx")
            {
                result.Message = MessageHolder.
                    GetErrorMessage(MessageType.NotFormat);

                return result;
            }

            result.Success = true;

            return result;
        }

        public IDataResult<string> GetValue(IExcelWorksheetEntity excelWorksheetEntity)
        {
            string result = string.Empty;
            IDataResult<string> dataResult =
                new DataResult<string>() { Success = false };

            int rowNo = excelWorksheetEntity.RowNo;
            int cellNo = excelWorksheetEntity.CellNo;

            if (excelWorksheetEntity.ExcelWorksheet.Cells[rowNo, cellNo].Value != null)
            {
                result = excelWorksheetEntity.ExcelWorksheet
                    .Cells[rowNo, cellNo].Value.ToString();

                result = RemoveEmpty(result);

                dataResult.Success = true;
                dataResult.Data = result;

                return dataResult;
            }

            return dataResult;
        }

        public IDataResult<IExcelColor> GetColorValue(IExcelWorksheetEntity excelWorksheetEntity)
        {
            IDataResult<IExcelColor> dataResult =
                new DataResult<IExcelColor>() {Success = false};
            IExcelColor excelColor = new ExcelColor();

            int rowNo = excelWorksheetEntity.RowNo;
            int cellNo = excelWorksheetEntity.CellNo;
           
            string colorName = excelWorksheetEntity.ExcelWorksheet.
                Cells[rowNo, cellNo].Style.Fill.BackgroundColor.Rgb;

            if (string.IsNullOrEmpty(colorName))
            {
                dataResult.Success = false;

                return dataResult;
            }

         
            Color color = ColorTranslator.FromHtml("#"+colorName);
            excelColor.R = color.R;
            excelColor.G = color.G;
            excelColor.B = color.B;

            dataResult.Success = true;
            dataResult.Data = excelColor;

            return dataResult;
        }

        public IDataResult<SexType> GetSexType(IExcelWorksheetEntity excelWorksheetEntity)
        {
            int rowNo = excelWorksheetEntity.RowNo;

            IDataResult<string> type = GetValue(excelWorksheetEntity);

            IDataResult<SexType> dataResult =
                new DataResult<SexType>() { Success = false };

            if (!type.Success)
            {
                dataResult.Data = SexType.None;
                dataResult.Message = MessageHolder.
                      GetErrorMessage(MessageType.NotSexType) + rowNo +"\n";

                return dataResult;
            }

            foreach (var sexTypeValue in Enum.GetValues(typeof(SexType)))
            {
                if (sexTypeValue.ToString().ToLower().Equals(type.Data.ToLower()))
                {
                    dataResult.Success = true;
                    dataResult.Data = (SexType)sexTypeValue;

                    return dataResult;
                }
            }
            dataResult.Data = SexType.None;
            dataResult.Message = MessageHolder.
                GetErrorMessage(MessageType.NotSexType) + rowNo+'\n';

            return dataResult;
        }

        public IDataResult<Dictionary<string,string>>
            GetTranslateEntity(IExcelWorksheetEntity excelWorksheetEntity)
        {
            int rowNo = excelWorksheetEntity.RowNo;
            int cellNo = excelWorksheetEntity.CellNo;
            ExcelWorksheet sheet = excelWorksheetEntity.ExcelWorksheet;

            IDataResult<Dictionary<string, string>> dataResult =
                new DataResult<Dictionary<string, string>>() { Success = false };

            Dictionary<string, string> translateDictionary =
                new Dictionary<string, string>();

            int endCoumn = sheet.Dimension.End.Column;

            if (_exelConfiguration.DataColumn.SectionChiness == cellNo)
            {
                endCoumn--;
            IExcelWorksheetEntity tmpEntity = new ExcelWorksheetEntity();
            tmpEntity.RowNo = _exelConfiguration.DataRow.Title;
            tmpEntity.CellNo = _exelConfiguration.DataColumn.Section;
                tmpEntity.ExcelWorksheet = sheet;
            IDataResult<string> dataTitle = GetValue(tmpEntity);

            if (!dataTitle.Success)
            {
                dataResult.Message += MessageHolder.GetErrorMessage(MessageType.NotNameTitle)
                        + _exelConfiguration.DataColumn.Section+ "\n";
                dataResult.Success = false;

                return dataResult;
            }

            string title = dataTitle.Data;

            tmpEntity.RowNo = rowNo;
            tmpEntity.CellNo = _exelConfiguration.DataColumn.Section;
            dataTitle = GetValue(tmpEntity);

            if (translateDictionary.ContainsKey(title))
            {
                dataResult.Message += MessageHolder.
                                          GetErrorMessage(MessageType.AlreadyAddLanguage) + cellNo+ "\n";
                dataResult.Success = false;

                return dataResult;
            }

            translateDictionary.Add(title, dataTitle.Data);
        }

            for (int i = cellNo; i <= endCoumn; i = i + 2)
            {
                IExcelWorksheetEntity tmpEntity = new ExcelWorksheetEntity();
                tmpEntity.RowNo = _exelConfiguration.DataRow.Title;
                tmpEntity.CellNo = i;
                tmpEntity.ExcelWorksheet = sheet;
                IDataResult<string> dataTitle = GetValue(tmpEntity);
               
                if (!dataTitle.Success)
                {
                    dataResult.Message += MessageHolder.GetErrorMessage(MessageType.NotNameTitle) + i+ "\n";
                    dataResult.Success = false;

                    return dataResult;
                }

                string title = dataTitle.Data;

                tmpEntity.RowNo = rowNo;
                tmpEntity.CellNo = i;
                dataTitle = GetValue(tmpEntity);

             
                if (translateDictionary.ContainsKey(title))
                {
                    dataResult.Message += MessageHolder.
                        GetErrorMessage(MessageType.AlreadyAddLanguage) + i+ "\n";
                    dataResult.Success = false;

                    return dataResult;
                }
                //if (!dataTitle.Success)
                //{
                //    dataResult.Message += MessageHolder.
                //               GetErrorMessage(MessageType.NotTranslate) + i+ "\n";             
                //}


                translateDictionary.Add(title, dataTitle.Data);
            }

            dataResult.Data = translateDictionary;
            dataResult.Success = true;

            return dataResult;
        }

        private string RemoveEmpty(string title)
        {

            while (title.Contains(" "))
            {
                title = title.Replace(" ", "");
            }

            return title;
        }
    }
}
