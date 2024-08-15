using LMS.Data;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LMS.Repositories.General
{
    public class GeneralRepository<T> : IDisposable, IGeneralRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;

        public GeneralRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
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


        public void Add(T item)
        {
            dbSet.Add(item);
        }

        public async Task<T> AddAsync(T item)
        {
            dbSet.Add(item);
            return item;
        }

        public void AddRange(List<T> items)
        {
            dbSet.AddRange(items);
        }

        public void Delete(T item)
        {
            if (_context.Entry(item).State == EntityState.Detached)
                dbSet.Attach(item);

            dbSet.Remove(item);
        }

        public async Task<T> DeleteAsync(T item)
        {
            if (_context.Entry(item).State == EntityState.Detached)
                dbSet.Attach(item);

            dbSet.Remove(item);

            return item;
        }

        public void DeleteRange(List<T> items)
        {
            dbSet.RemoveRange(items);
        }

        public bool Exists(Expression<Func<T, bool>> filter = null)
        {
            return dbSet.Any(filter);
        }

        public async Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disabledTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (disabledTracking)
                query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            if (include != null)
                query = include(query);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            else
                return await query.ToListAsync();
        }

        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disabledTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (disabledTracking)
                query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync();

        }

        public void Update(T item)
        {
            dbSet.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task<T> UpdateAsync(T item)
        {
            dbSet.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            return item;
        }
    }
}
