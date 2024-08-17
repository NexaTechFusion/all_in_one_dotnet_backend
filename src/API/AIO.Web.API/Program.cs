using AIO.WebFramework.Configurations.Swagger;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerAndUI();
}

app.MapControllers();
app.Run();