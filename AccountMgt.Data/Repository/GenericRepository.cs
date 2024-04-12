using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace AccountMgt.Data.Repository
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> predicate, bool trackChanges = false)
        {
            IQueryable<T> result = trackChanges
                ? _dbSet.Where(predicate)
                : _dbSet.Where(predicate).AsNoTracking();
            return result;
        }

        public IQueryable<T> GetAll(bool trackChanges)
        {
            return trackChanges ? _dbSet : _dbSet.AsNoTracking();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            Update(entity);
            await _context.SaveChangesAsync();
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task BulkUpdate(Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setProperty)
        {
            await _dbSet.ExecuteUpdateAsync(setProperty);
        }

        public async Task BulkUpdate(Expression<Func<T, bool>> query, 
            Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setProperty)
        {
            await _dbSet.Where(query).ExecuteUpdateAsync(setProperty);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(Expression<Func<T, bool>> predicate)
        {
            await _dbSet.Where(predicate).ExecuteDeleteAsync();
        }

        public bool DeleteById(object id)
        {
            var record = _dbSet.Find(id);
            if (record == null) return false;

            _dbSet.Remove(record);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(object id)
        {
            var result = DeleteById(id);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task DeleteAsync(T entity)
        {
            Delete(entity);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
