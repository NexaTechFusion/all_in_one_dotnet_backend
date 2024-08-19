using AIO.Domain.Shared.Contracts.Persistence;
using AIO.Infrastructure.Persistence.Repositories.Common;
using AIO.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AIO.Infrastructure.Persistence.ServiceConfiguration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContextPool<ApplicationWriteDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                .EnableSensitiveDataLogging();
        });

        services.AddDbContextPool<ApplicationReadDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging();
        });

        return services;
    }
}