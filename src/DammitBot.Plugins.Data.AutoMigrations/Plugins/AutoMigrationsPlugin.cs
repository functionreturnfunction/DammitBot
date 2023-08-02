using DammitBot.Data.Migrations.Library;

namespace DammitBot.Plugins;

public class AutoMigrationsPlugin : IAutoMigrationsPlugin
{
    protected readonly MigrationRunner _runner;

    public bool Priority => true;

    public AutoMigrationsPlugin(MigrationRunner runner)
    {
        _runner = runner;
    }

    public void Initialize()
    {
        _runner.Up();
    }

    public void Cleanup() {}
}