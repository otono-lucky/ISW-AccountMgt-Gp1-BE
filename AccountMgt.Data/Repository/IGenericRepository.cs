using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace AccountMgt.Data.Repository
{
    public interface IGenericRepository<T>
    {
        T Add(T entity);
        Task<T> AddAsync(T entity);
        Task BulkUpdate(Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setProperty);
        Task BulkUpdate(Expression<Func<T, bool>> query, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setProperty);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        bool DeleteById(object id);
        Task<bool> DeleteByIdAsync(object id);
        void DeleteRange(IEnumerable<T> entities);
        Task DeleteRangeAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll(bool trackChanges);
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> predicate, bool trackChanges = false);
        void Update(T entity);
        Task UpdateAsync(T entity);
        void UpdateRange(IEnumerable<T> entities);
        Task UpdateRangeAsync(IEnumerable<T> entities);
    }
}
