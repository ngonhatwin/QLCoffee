using Dapper;
using Microsoft.EntityFrameworkCore;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using System.Data;
using System.Linq.Expressions;
namespace ProjectPersonal.Infrastructure.Repository
{
    public class RepositoryBaseAsync< T, K> : IRepositoryBaseAsync< T, K> 
        where T : class
    {
        private readonly QlcoffeeContext _context;
        protected IDbConnection Connection => _context.Database.GetDbConnection();

        public RepositoryBaseAsync(QlcoffeeContext context)
        {
            _context = context;
        }
        public Task CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            return Task.CompletedTask;
        }
        public Task UpdateAsync(K id, T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }
        public Task DeleteManyAsync(IEnumerable<T> entity)
        {
            _context.Set<T>().RemoveRange(entity);
            return Task.CompletedTask;
        }
        public IQueryable<T> FindAll(bool trackChanges = false) =>
            !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();

        public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindAll(trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperties) => current.Include(includeProperties));
            return items;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false)
        {
            return !trackChanges ? _context.Set<T>().Where(expression).AsNoTracking() : _context.Set<T>().Where(expression);
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindByCondition(expression, trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperties) => current.Include(includeProperties));
            return items;
        }
        public async Task<IEnumerable<T>> QueryAsync(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            var connection = Connection;

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open(); 
            }

            return await connection.QueryAsync<T>(sql, param, commandType: commandType); // Thực thi QueryAsync của Dapper
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();
        public async Task RollBackTransactionAsync() => await _context.Database.RollbackTransactionAsync();
        public async Task EndTransactionAsync()
        {
            await SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();
        }


    }
}
