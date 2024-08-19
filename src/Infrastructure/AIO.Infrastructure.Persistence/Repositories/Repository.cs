using AIO.Domain.Shared.Contracts.Persistence.Repository;
using AIO.Domain.Shared.Entities;
using AIO.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;

namespace AIO.Infrastructure.Persistence.Repositories;

internal class Repository<TEntity> : BaseAsyncRepository<TEntity>, IRepository<TEntity> where TEntity : class, IEntity
{
    public Repository(ApplicationDbContext dbContext, bool onlyRead) : base(dbContext, onlyRead)
    {
    }

    /// <summary>
    /// Query the repository
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> where)
    {
        IQueryable<TEntity> query = Table;
        if (where != null)
            query = query.Where(where);
        return await query.ToListAsync();
    }

    /// <summary>
    /// Query the repository
    /// </summary>
    /// <param name="where"></param>
    /// <param name="includesList"></param>
    /// <returns></returns>
    public async Task<List<TEntity>> Query(
        Expression<Func<TEntity, bool>> where,
        List<string> includesList)
    {
        IQueryable<TEntity> query = Table;
        if (where != null)
            query = query.Where(where);
        query = includesList.Aggregate(query, (current, include) => current.Include(include));
        return await query.ToListAsync();
    }

    /// <summary>
    /// Query the repository
    /// </summary>
    /// <param name="where"></param>
    /// <param name="includesList"></param>
    /// <returns></returns>
    public async Task<List<TEntity>> Query(
        Expression<Func<TEntity, bool>> where,
        List<Expression<Func<TEntity, object>>> includesList)
    {
        IQueryable<TEntity> query = Table;
        if (where != null)
            query = query.Where(where);

        if (includesList is not null)
            query = includesList.Aggregate(query, (current, include) => current.Include(include));

        return await query.ToListAsync();
    }

    /// <summary>
    /// Add an entity to the repository
    /// </summary>
    /// <param name="entity"></param>
    public async Task Add(TEntity entity)
    {
        await base.AddAsync(entity);
    }

    /// <summary>
    /// Find One entity
    /// </summary>
    /// <param name="where"></param>
    /// <param name="includesList"></param>
    /// <returns></returns>
    public async Task<TEntity> FindOne(Expression<Func<TEntity, bool>> where, List<string> includesList = null)
    {
        IQueryable<TEntity> query = Table.Where(where);
        if (includesList is not null)
            query = includesList.Aggregate(query, (current, include) => current.Include(include));
        return await query.FirstAsync();
    }

    /// <summary>
    /// Find first entity
    /// </summary>
    /// <param name="where"></param>
    /// <param name="includesList"></param>
    /// <returns></returns>
    public async Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> where, List<string> includesList = null)
    {
        IQueryable<TEntity> query = Table;
        if (where is not null)
            query = query.Where(where);
        if (includesList is not null)
            query = includesList.Aggregate(query, (current, include) => current.Include(include));
        return await query.FirstOrDefaultAsync();
    }

    /// <summary>
    /// Get an entity by its ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<TEntity?> GetById(int id)
    {
        return await base.GetByIdAsync(id);
    }

    /// <summary>
    /// Delete an entity
    /// </summary>
    /// <param name="entity"></param>
    public void Delete(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }
}