using AIO.Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIO.Infrastructure.Persistence;

public class ApplicationWriteDbContext : ApplicationDbContext
{
    public ApplicationWriteDbContext(DbContextOptions options)
        : base(options)
    {
        SavingChanges += OnSavingChanges;
    }

    private void OnSavingChanges(object sender, SavingChangesEventArgs e)
    {
        ConfigureEntityDates();
    }


    private void ConfigureEntityDates()
    {
        IEnumerable<ITimeModification> updatedEntities = ChangeTracker.Entries().Where(x =>
                x.Entity is ITimeModification && x.State == EntityState.Modified)
            .Select(x => x.Entity as ITimeModification);

        IEnumerable<ITimeModification> addedEntities = ChangeTracker.Entries().Where(x =>
            x.Entity is ITimeModification && x.State == EntityState.Added).Select(x => x.Entity as ITimeModification);

        foreach (ITimeModification entity in updatedEntities)
            if (entity != null)
                entity.ModifiedDate = DateTime.Now;

        foreach (ITimeModification entity in addedEntities)
            if (entity != null)
            {
                entity.CreatedTime = DateTime.Now;
                entity.ModifiedDate = DateTime.Now;
            }
    }
}