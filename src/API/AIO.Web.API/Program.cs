using AIO.Application.Shared.ServiceConfiguration;
using AIO.Infrastructure.Persistence.ServiceConfiguration;
using AIO.WebFramework.Configurations.Swagger;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(configuration);
    
WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerAndUI();
}

app.MapControllers();
app.Run();