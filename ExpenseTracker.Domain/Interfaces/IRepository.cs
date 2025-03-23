using ExpenseTracker.Shared;
using System.Linq.Expressions;

namespace ExpenseTracker.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<Result> AddAsync(T entity);
    Task<Result> UpdateAsync(T entity);
    Task<Result> DeleteAsync(Guid id);

    // Methods for querying with predicates
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
}
