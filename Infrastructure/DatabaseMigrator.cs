using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public class DatabaseMigrator
{
    private readonly IServiceScopeFactory _scopeFactory;

    public DatabaseMigrator(IServiceScopeFactory scopeFactory) {
        _scopeFactory = scopeFactory;
    }

    public async Task Migrate() {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
    }
}
