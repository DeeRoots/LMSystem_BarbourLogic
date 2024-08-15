using LMS.Data;
using LMS.Repositories.General;

namespace LMS.Repositories.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {

        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool disposed = false;


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _context.Dispose();
            }
            this.disposed = true;
        }


        public IGeneralRepository<T> GeneralRepository<T>() where T : class
        {
            IGeneralRepository<T> repository = new GeneralRepository<T>(_context);
            return repository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
