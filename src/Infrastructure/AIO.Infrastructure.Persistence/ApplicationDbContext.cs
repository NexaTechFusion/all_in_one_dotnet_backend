using AIO.Domain.Product.Entities;
using AIO.Domain.Shared.Entities;
using AIO.SharedKernel.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AIO.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        Assembly entitiesAssembly = typeof(IEntity).Assembly;
        modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.AddRestrictDeleteBehaviorConvention();
        modelBuilder.AddPluralizingTableNameConvention();
    }
}