using Microsoft.EntityFrameworkCore;
using ProjectPersonal.Application.Common.Interfaces.Repository;
namespace ProjectPersonal.Infrastructure.Repository
{
    public class Unitofwork<T> : IUnitofwork<T> where T : class
    {
        private readonly QlcoffeeContext _context;
        private bool _disposed = false;
        public DbSet<T> Entities { get; }
        public Unitofwork(QlcoffeeContext context)
        {
            _context = context;
        }
        public IRepositoryBaseAsync<T, K> GetRepository<T, K>() where T : class
        {
            return new RepositoryBaseAsync<T, K>(_context);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
