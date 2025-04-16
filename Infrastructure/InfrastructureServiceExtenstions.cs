using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceExtenstions
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, ConfigurationManager config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection"); 
        services.AddDbContextFactory<AppDbContext>(options => options.UseSqlServer(connectionString));
        services.AddTransient<ICallRecordRepository, CallRecordRepository>();
        services.AddSingleton<DatabaseMigrator>();
        return services;
    }
}