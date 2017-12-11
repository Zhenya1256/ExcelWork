using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Entity.NormalizeEntity;

namespace WorkWithExcel.Model.Entity.NormalizeEntity
{
    public class RootTranslationItemEntity : BaseTranslationItemEntity, IRootTranslationItemEntity
    {
        public ITranslationEntity WordTranslation { get; set; }
    }
}