using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Dal.Repositor.Base;

namespace WorkWithExcel.DAL.Repositor.Base
{
    public class GenericRepository<TEntity> : GenericKeyRepository<int, TEntity>,
        IGenericRepository<TEntity> where TEntity : class
    {

        public GenericRepository(SketchpackDbContext context) : base(context)
        {
        }

        public override async Task<TEntity> GetByIdAsync(int id)
        {
            return id <= 0 ? null : await Context.Set<TEntity>()
                .FindAsync(id);
        }
    }
}
