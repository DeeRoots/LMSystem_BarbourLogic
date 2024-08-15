using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repositories.General
{
    public interface IGeneralRepository<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> GetAll(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           bool disabledTracking = true
           );

        T GetById(object id);

        Task<T> GetByIdAsync(
            Expression<Func<T, bool>> filter = null,
             Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
             bool disabledTracking = true
            );

        void Add(T item);
        Task<T> AddAsync(T item);
        void AddRange(List<T> items);

        void Update(T item);
        Task<T> UpdateAsync(T item);

        void Delete(T item);
        Task<T> DeleteAsync(T item);
        void DeleteRange(List<T> items);

        bool Exists(Expression<Func<T, bool>> filter = null);
    }
}
