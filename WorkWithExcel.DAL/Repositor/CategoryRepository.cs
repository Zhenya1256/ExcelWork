using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Dal.Repositor;
using WorkWithExcel.DAL.Repositor.Base;
using WorkWithExcel.Model.Entity.DalEntity;

namespace WorkWithExcel.DAL.Repositor
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository
            (SketchpackDbContext context) : base(context)
        {
        }
    }
}
