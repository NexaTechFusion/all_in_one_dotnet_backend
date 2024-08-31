using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AIO.WebFramework.Configurations.Swagger;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (!context.Type.IsEnum) return;
        IEnumerable<OpenApiString> enumValues = Enum.GetNames(context.Type).Select(name => new OpenApiString(name));
        schema.Enum = enumValues.Cast<IOpenApiAny>().ToList();
    }
}