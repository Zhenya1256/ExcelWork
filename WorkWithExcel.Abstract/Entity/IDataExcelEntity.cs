using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.Abstract.Entity
{
    public interface IDataExcelEntity:IExcelColor,ITranslationEntity,ITranslateImgEntity
    {
        string Index { get; set; }
        string PageNomer { get; set; }
        SexType SexType { get; set; }
        
    }
}
