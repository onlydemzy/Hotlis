using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Hotlis.Application;
public static class AppDependency
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //services.AddMediatR(c=>c.RegisterServicesFromAssemblyContaining<NewUserCommand>());
        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddMediatR(c=>c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        //services.AddMappings();
        return services;
    }

    /*private static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config=TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddScoped<IMapper,Mapper>();
        return services;
    }*/

}
