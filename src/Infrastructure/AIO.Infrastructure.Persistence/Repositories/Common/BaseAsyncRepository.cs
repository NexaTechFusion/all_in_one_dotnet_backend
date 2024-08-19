using AIO.Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AIO.Infrastructure.Persistence.Repositories.Common;

public abstract class BaseAsyncRepository<TEntity> where TEntity : class, IEntity
{
    public readonly ApplicationDbContext DbContext;

    protected BaseAsyncRepository(ApplicationDbContext dbContext, bool onlyRead)
    {
        DbContext = dbContext;
        Entities = DbContext.Set<TEntity>(); 
        Table = onlyRead ? Entities.AsNoTrackingWithIdentityResolution().Where(x => x.DeletedDate == null) : Entities;
    }

    private DbSet<TEntity> Entities { get; }
    protected IQueryable<TEntity> Table { get; init; }

    protected virtual async Task<List<TEntity>> ListAllAsync()
    {
        return await Table.ToListAsync();
    }

    protected virtual async Task AddAsync(TEntity entity)
    {
        await Entities.AddAsync(entity);
    }

    protected virtual async Task MultiAddAsync(TEntity[] entities)
    {
        await Entities.AddRangeAsync(entities);
    }

    protected virtual async Task UpdateAsync(Expression<Func<TEntity, bool>> whereExpression,
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression)
    {
        await Entities.Where(whereExpression).ExecuteUpdateAsync(updateExpression);
    }

    protected virtual Task TrackableUpdateAsync(TEntity entity)
    {
        Entities.Update(entity);
        return Task.CompletedTask;
    }

    protected virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> deleteExpression)
    {
        await Entities.Where(deleteExpression).ExecuteDeleteAsync();
    }

    protected virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        return await Entities.FindAsync(id);
    }
}