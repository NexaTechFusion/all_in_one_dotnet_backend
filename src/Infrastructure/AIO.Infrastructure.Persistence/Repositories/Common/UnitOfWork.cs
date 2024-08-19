using AIO.Domain.Shared.Contracts.Persistence;
using AIO.Infrastructure.Persistence;
using AIO.Domain.Shared.Contracts.Persistence.Repository;
using AIO.Domain.Shared.Entities;

namespace AIO.Infrastructure.Persistence.Repositories.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationReadDbContext _readDbContext;
    private readonly Dictionary<Type, object> _readRepositories;

    private readonly ApplicationWriteDbContext _writeDbContext;
    private readonly Dictionary<Type, object> _writeRepositories;

    public UnitOfWork(ApplicationWriteDbContext writeDbContext, ApplicationReadDbContext readDbContext)
    {
        _writeDbContext = writeDbContext;
        _readDbContext = readDbContext;
        _readRepositories = new Dictionary<Type, object>();
        _writeRepositories = new Dictionary<Type, object>();
    }
    
    public IRepository<TEntity> GetRepository<TEntity>(bool readOnly = false) where TEntity : class, IEntity
    {
        Dictionary<Type, object> repositories = readOnly ? _readRepositories : _writeRepositories;
        if (repositories.ContainsKey(typeof(TEntity)))
            return repositories[typeof(TEntity)] as Repository<TEntity>;

        var repository = new Repository<TEntity>(readOnly ? _readDbContext : _writeDbContext, readOnly);
        repositories.Add(typeof(TEntity), repository);
        return repository;
    }

    public Task<int> CommitAsync()
    {
        return _writeDbContext.SaveChangesAsync();
    }

    public ValueTask RollBackAsync()
    {
        return _writeDbContext.DisposeAsync();
    }
}