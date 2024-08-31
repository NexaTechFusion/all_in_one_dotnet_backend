using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace AIO.WebFramework.Configurations.Swagger;

public static class SwaggerConfigurationExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        //More info : https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters
        //Add services and configuration to use swagger
        services.AddSwaggerGen(options =>
        {
            //show controller XML comments like summary
            string xmlDocPath = Path.Combine(AppContext.BaseDirectory, "Project.xml");
            options.IncludeXmlComments(xmlDocPath, true);

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "API V1"
            });

            options.MapType<DateTime>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date"
            });
            #region Filters

            // Use enum members instead of int
            options.SchemaFilter<EnumSchemaFilter>();

            #region Versioning

            // Remove version parameter from all Operations
            options.OperationFilter<RemoveVersionParameters>();

            //set version "api/v{version}/[controller]" from current swagger doc verion
            options.DocumentFilter<SetVersionInPaths>();

            //Seperate and categorize end-points by doc version
            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
            
                IEnumerable<ApiVersion> versions = methodInfo.DeclaringType!
                    .GetCustomAttributes<ApiVersionAttribute>(true)
                    .SelectMany(attr => attr.Versions);
            
                return versions.Any(v => $"v{v.ToString()}" == docName);
            });

            #endregion
            
            #endregion
        });
    }

    public static void UseSwaggerAndUi(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        //More info : https://github.com/domaindrivendev/Swashbuckle.AspNetCore

        //Swagger middleware for generate "Open API Documentation" in swagger.json
        app.UseSwagger();

        //Swagger middleware for generate UI from swagger.json
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("v1/swagger.json", "API Docs");

            #region Customizing

            //// Display
            options.DisplayRequestDuration();
            options.DocExpansion(DocExpansion.None);
            options.EnableFilter();
            options.ShowExtensions();

            //// Network
            options.EnableValidator();

            #endregion
        });

        //ReDoc UI middleware. ReDoc UI is an alternative to swagger-ui
        app.UseReDoc(options =>
        {
            options.SpecUrl("/swagger/v1/swagger.json");

            #region Customizing

            //By default, the ReDoc UI will be exposed at "/api-docs"
            options.RoutePrefix = "api-docs";
            options.DocumentTitle = "API Docs";

            options.EnableUntrustedSpec();
            options.ScrollYOffset(10);
            options.HideHostname();
            options.ExpandResponses("200,201");
            options.RequiredPropsFirst();
            options.NoAutoAuth();
            options.PathInMiddlePanel();
            options.SortPropsAlphabetically();

            #endregion
        });
    }
}