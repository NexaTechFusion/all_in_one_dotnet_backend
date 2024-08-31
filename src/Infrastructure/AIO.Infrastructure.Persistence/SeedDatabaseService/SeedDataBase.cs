using AIO.Domain.Shared.Contracts.Persistence;
using AIO.Infrastructure.Persistence.Seeds;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AIO.Infrastructure.Persistence.SeedDatabaseService;

public interface ISeedDataBase
{
    Task Seed();
}

public class SeedDataBase(IUnitOfWork unitOfWork,
    ILogger<SeedDataBase> logger) : ISeedDataBase
{
    public async Task Seed()
    {
        var productSeed = new ProductSeed(unitOfWork);
        await productSeed.Run();
    }
}