using AIO.Application.Shared.Validations;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace AIO.Application.Shared.ServiceConfiguration;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
            options.Namespace = "AIO.Application.Mediator";
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidateCommandBehavior<,>));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}