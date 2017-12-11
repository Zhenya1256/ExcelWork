using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.NormalizeEntity;

namespace WorkWithExcel.Model.Entity.NormalizeEntity
{
    public class BaseTranslationItemEntity : IBaseTranslationItemEntity
    {
        public string SexType { get; set; }
        public string Index { get; set; }
        public string Page { get; set; }
        public IExcelColor ExcelColor { get; set; }
    }
}