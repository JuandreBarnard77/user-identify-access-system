using System.Linq.Expressions;

namespace UserIdentityAccess.Application.Interfaces;
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task SaveChangesAsync();
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);

    Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetWithIncludesAsync(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes);
}
