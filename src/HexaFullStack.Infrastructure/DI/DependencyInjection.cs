using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HexaFullStack.Application.Abstractions.Persistence;
using HexaFullStack.Application.Abstractions.Realtime;
using HexaFullStack.Application.Abstractions.Security;
using HexaFullStack.Infrastructure.Persistence.MySql;
using HexaFullStack.Infrastructure.Persistence.Mongo;
using HexaFullStack.Infrastructure.Realtime;
using HexaFullStack.Infrastructure.Security;

namespace HexaFullStack.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddDbContext<AppDbContext>(o =>
            o.UseMySql(cfg.GetConnectionString("MySql"), ServerVersion.AutoDetect(cfg.GetConnectionString("MySql"))));
        services.AddScoped<IUserRepository, UserRepository>();

        var m = cfg.GetSection("Mongo").Get<MongoSettings>()!;
        services.AddSingleton(m);
        services.AddSingleton<AuditLogRepository>();

        services.AddSingleton(new JwtSettings
        {
            Key = cfg["Jwt:Key"]!,
            Issuer = cfg["Jwt:Issuer"]!,
            Audience = cfg["Jwt:Audience"]!,
            AccessMinutes = int.Parse(cfg["Jwt:AccessMinutes"] ?? "60")
        });
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IJwtTokenService, JwtTokenService>();

        services.AddSignalR();
        services.AddSingleton<INotificationBus, SignalRNotificationBus>();
        return services;
    }
}
