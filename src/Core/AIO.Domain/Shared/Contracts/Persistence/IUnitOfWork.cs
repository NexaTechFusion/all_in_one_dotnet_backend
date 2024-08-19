using AIO.Domain.Shared.Contracts.Persistence.Repository;
using AIO.Domain.Shared.Entities;

namespace AIO.Domain.Shared.Contracts.Persistence;

public interface IUnitOfWork
{
    public IRepository<TEntity> GetRepository<TEntity>(bool readOnly = false) where TEntity : class, IEntity;
    Task<int> CommitAsync();
    ValueTask RollBackAsync();
}