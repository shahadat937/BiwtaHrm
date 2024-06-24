using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Add(T entity);
        Task<bool> Exists(int id);
        Task Update(T entity);
        Task Delete(T entity);

        T FindOne(Expression<Func<T, bool>> predicate);

        T FindOne(Expression<Func<T, bool>> predicate, List<string> includeProperties);

        bool Exists(Expression<Func<T, bool>> predicate);

        IQueryable<T> AsQueryable();

        IQueryable<T> Where(Expression<Func<T, bool>> predicate);

        int Count();

        int Count(Expression<Func<T, bool>> predicate);


        T FinedOneInclude(Expression<Func<T, bool>> predicate, params string[] include);
        IQueryable<T> FilterWithInclude(Expression<Func<T, bool>> predicate, params string[] include);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindOneAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindOneAsync(Expression<Func<T, bool>> predicate, params string[] include);
        Task<ICollection<T>> FilterAsync(Expression<Func<T, bool>> predicate);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
