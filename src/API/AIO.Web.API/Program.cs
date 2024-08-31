using AIO.Application.Shared.ServiceConfiguration;
using AIO.Infrastructure.Persistence;
using AIO.Infrastructure.Persistence.SeedDatabaseService;
using AIO.Infrastructure.Persistence.ServiceConfiguration;
using AIO.WebFramework.Configurations.Swagger;
using AIO.WebFramework.ServiceConfiguration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddApplicationServices()
    .AddPersistenceServices(configuration)
    .AddWebFrameworkServices();


WebApplication app = builder.Build();
await using AsyncServiceScope scope = app.Services.CreateAsyncScope();


#region Seeding and creating database

try
{
    ApplicationDbContext context = scope.ServiceProvider.GetService<ApplicationWriteDbContext>() ??
                                   throw new Exception("Database Context Not Found");
    await context.Database.MigrateAsync();

    var seedService = scope.ServiceProvider.GetRequiredService<ISeedDataBase>();
    await seedService.Seed();
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogCritical(exception, exception.Message);
}

#endregion

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerAndUi();
}

app.MapControllers();
app.Run();