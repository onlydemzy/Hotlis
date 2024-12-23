
using Hotlis.Application.Common.Interfaces;
using Hotlis.Infrastructure.Interceptors;
using Hotlis.Infrastructure.Persistence;
using Hotlis.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Hotlis.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        
        AddPersistence(services, configuration);
        AddInterceptors(services);
        
        return services;
    }

    private static IServiceCollection AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        //var connectionString = configuration.GetConnectionString("devcon");
        string connectionString="Data Source=(local); Database=HotlisDb; User Id=sa; Password=NETwizzer123#; TrustServerCertificate=true"; 
        services.AddDbContext<AppDbContext>(options=>
            options.UseSqlServer(connectionString)
        );
        services.AddDbContextFactory<AppDbContext>(options=>options.UseSqlServer(connectionString),ServiceLifetime.Scoped);
        
        return services;
    }
    private static IServiceCollection AddInterceptors(IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
        services.AddScoped<IUserContextService, UserContextService>();
        services.AddScoped<AuditLogInterceptor>();
        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<IDbContextFactory<AppDbContext>, DbContextFactory<AppDbContext>>();
        
        return services;
    }

    public static async Task InitializeDatabaseAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await AppDbContextSeed.SeedDataSync(context);

    }
    
}