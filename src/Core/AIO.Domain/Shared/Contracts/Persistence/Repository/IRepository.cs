using AIO.Domain.Shared.Entities;
using System.Linq.Expressions;

namespace AIO.Domain.Shared.Contracts.Persistence.Repository;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Query a list of items
    /// </summary>
    /// <param name="where"></param>
    /// <returns></returns>
    Task<List<TEntity>> Query(Expression<Func<TEntity, bool>>? where);

    /// <summary>
    /// Query a list of items
    /// </summary>
    /// <param name="where"></param>
    /// <param name="includesList"></param>
    /// <returns></returns>
    Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> where, List<string> includesList);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="where"></param>
    /// <param name="includesList"></param>
    /// <returns></returns>
    Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> where,
        List<Expression<Func<TEntity, object>>> includesList);

    /// <summary>
    /// Add a new item
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task Add(TEntity entity);

    /// <summary>
    /// Find an item 
    /// </summary>
    /// <param name="where"></param>
    /// <param name="includesList"></param>
    /// <returns></returns>
    Task<TEntity> FindOne(Expression<Func<TEntity, bool>> where, List<string> includesList = null);

    /// <summary>
    /// Find first item
    /// </summary>
    /// <param name="where"></param>
    /// <param name="includesList"></param>
    /// <returns></returns>
    Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> where, List<string> includesList = null);

    /// <summary>
    /// Get an item
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity?> GetById(int id);

    /// <summary>
    /// Delete an item
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    void Delete(TEntity entity);
    
    /// <summary>
    /// Count items
    /// </summary>
    /// <param name="where"></param>
    /// <param name="includesList"></param>
    /// <returns></returns>
    Task<int> Count(Expression<Func<TEntity, bool>> where, List<string> includesList = null);
    
    /// <summary>
    /// Multiple Add
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task MultiAdd(TEntity[] entities);
}