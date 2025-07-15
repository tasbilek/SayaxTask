using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace VoltMeter.Domain.Abstractions;

public class Repository<TEntity, TContext> : IRepository<TEntity> where TEntity : class where TContext : DbContext
{
    private readonly TContext _context;

    private DbSet<TEntity> Entity;

    public Repository(TContext context)
    {
        _context = context;
        Entity = _context.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        Entity.Add(entity);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
    {
        await Entity.AddAsync(entity, cancellationToken);
    }

    public void AddRange(ICollection<TEntity> entities)
    {
        Entity.AddRange(entities);
    }

    public async Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
    {
        await Entity.AddRangeAsync(entities, cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await Entity.AnyAsync(expression, cancellationToken);
    }

    public void Delete(TEntity entity)
    {
        Entity.Remove(entity);
    }

    public async Task DeleteByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default(CancellationToken))
    {
        TEntity entity = await Entity.Where(expression).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        Entity.Remove(entity);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        TEntity entity = await Entity.FindAsync(id);
        Entity.Remove(entity);
    }

    public void DeleteRange(ICollection<TEntity> entities)
    {
        Entity.RemoveRange(entities);
    }

    public IQueryable<TEntity> GetAll()
    {
        return Entity.AsNoTracking().AsQueryable();
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default(CancellationToken), bool isTrackingActive = true)
    {
        return (!isTrackingActive) ? (await Entity.Where(expression).AsNoTracking().FirstOrDefaultAsync(cancellationToken))
                                   : (await Entity.Where(expression).FirstOrDefaultAsync(cancellationToken));
    }

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
    {
        return Entity.AsNoTracking().Where(expression).AsQueryable();
    }

    public void Update(TEntity entity)
    {
        Entity.Update(entity);
    }

    public void UpdateRange(ICollection<TEntity> entities)
    {
        Entity.UpdateRange(entities);
    }

    public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool isTrackingActive = true)
    {
        if (isTrackingActive)
        {
            return Entity.FirstOrDefault(expression);
        }

        return Entity.AsNoTracking().FirstOrDefault(expression);
    }
    public TEntity First(Expression<Func<TEntity, bool>> expression, bool isTrackingActive = true)
    {
        if (isTrackingActive)
        {
            return Entity.First(expression);
        }

        return Entity.AsNoTracking().First(expression);
    }

    public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default(CancellationToken), bool isTrackingActive = true)
    {
        if (isTrackingActive)
        {
            return await Entity.FirstAsync(expression, cancellationToken);
        }

        return await Entity.AsNoTracking().FirstAsync(expression, cancellationToken);
    }
}
