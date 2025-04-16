using Infrastructure;

namespace CDR_API;

public class MigrateDatabaseOnStartup : IStartupFilter
{
    private readonly DatabaseMigrator _migrator;

    public MigrateDatabaseOnStartup(DatabaseMigrator migrator) {
        _migrator = migrator;
    }

    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) {
        _migrator.Migrate().Wait();
        return next;
    }
}
