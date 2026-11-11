using ClaimFlow.Application.Interfaces;
using ClaimFlow.Infrastructure.Persistence;
using ClaimFlow.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClaimFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ClaimFlow")
            ?? throw new InvalidOperationException(
                "Connection string 'ClaimFlow' is required. Set ConnectionStrings__ClaimFlow.");

        services.AddDbContext<ClaimFlowDbContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36))));
        
        services.AddScoped<IPolicyRepository, PolicyRepository>();
        
        return services;

    }
}
