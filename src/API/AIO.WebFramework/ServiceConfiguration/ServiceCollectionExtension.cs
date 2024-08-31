using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;

namespace AIO.WebFramework.ServiceConfiguration;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddWebFrameworkServices(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true; 
            options.DefaultApiVersion = new ApiVersion(1, 0); 
            options.ReportApiVersions = true;
        });

     
        return services;
    }
}