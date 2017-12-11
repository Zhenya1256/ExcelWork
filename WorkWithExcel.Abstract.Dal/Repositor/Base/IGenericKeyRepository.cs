using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WorkWithExcel.Abstract.Dal.Repositor.Base
{
    public interface IGenericKeyRepository<TKey, TEntity>
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<int> GetCountAsync();
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> FetchAsync();
        Task<List<TEntity>> FetchByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> PaggingFetchAsync(int startIndex, int count);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> PaggingFetchByAsync(Expression<Func<TEntity, bool>> predicate,
            int startIndex, int count);
        Task SaveAsync();
    }
}
