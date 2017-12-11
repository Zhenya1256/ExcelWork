using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Dal.Repositor.Base;

namespace WorkWithExcel.DAL.Repositor.Base
{
    public class GenericKeyRepository<TKey, TEntity> : 
        IGenericKeyRepository<TKey, TEntity> where TEntity : class
    {
        public GenericKeyRepository(SketchpackDbContext context)
        {
            Context = context;
        }

        public SketchpackDbContext Context { get; }

        public DbSet<TEntity> DbSet => Context.Set<TEntity>();

        public virtual async Task AddAsync(TEntity entity)
        {
             Context.Set<TEntity>().Add(entity);
            await Task.FromResult(0);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Task.FromResult(0);
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity entity)
        {
            TEntity result = Context.Set<TEntity>()
                .Remove(entity);
            return await Task.FromResult(result);
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetByAsync
            (Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await Context.Set<TEntity>()
                .FindAsync(id);
        }

        public virtual async Task<int> GetCountAsync()
        {
            return await Context.Set<TEntity>().CountAsync();
        }

        public virtual Task<int> GetCountAsync
            (Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().CountAsync(predicate);
        }

        public virtual async Task<List<TEntity>> FetchAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<List<TEntity>> FetchByAsync
            (Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate)
                .ToListAsync();
        }

        public virtual async Task<List<TEntity>> PaggingFetchAsync
            (int startIndex, int count)
        {
            return await Context.Set<TEntity>().Skip(startIndex)
                .Take(count).ToListAsync();
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync
            (Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<List<TEntity>> PaggingFetchByAsync
            (Expression<Func<TEntity, bool>> predicate, int startIndex, int count)
        {
            return await Context.Set<TEntity>().Where(predicate)
                .Skip(startIndex).Take(count).ToListAsync();
        }

        public Task SaveAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}
