using System.Linq.Expressions;

namespace VoltMeter.Domain.Abstractions;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll();
    IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    T First(Expression<Func<T, bool>> predicate, bool isTrackingActive = true);
    Task<T> FirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, bool isTrackingActive = true);
    T FirstOrDefault(Expression<Func<T, bool>> predicate, bool isTrackingActive = true);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, bool isTrackingActive = true);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    void Add(T entity);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void AddRange(ICollection<T> entities);
    Task AddRangeAsync(ICollection<T> entities, CancellationToken cancellationToken = default);
    void Update(T entity);
    void UpdateRange(ICollection<T> entities);
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Delete(T entity);
    void DeleteRange(ICollection<T> entities);
    Task DeleteByExpressionAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
}