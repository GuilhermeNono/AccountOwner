using Contracts;
using Entities;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace AccountOwnerServer.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                .AllowAnyOrigin() // WithOrigins("dominio")
                .AllowAnyMethod() // WithMethods("GET", "POST")
                .AllowAnyHeader() // WithHeaders("accept", "content-header")
            );
        });
    }

    public static void configureIISIntegration(this IServiceCollection services)
    {
        services.Configure<IISOptions>(options =>
        {

        });

    }
    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }

    public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config["mysqlconnection:connectionString"];
        var serverVersion = ServerVersion.AutoDetect(connectionString);
        services.AddDbContext<RepositoryContext>(o =>
        o.UseMySql(connectionString, serverVersion));
    }

    public static void ConfigureRepositoryWrapper(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
    }

}