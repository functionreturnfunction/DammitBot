using DammitBot.Abstract;
using DammitBot.Data.Migrations.Library;

// ReSharper disable once CheckNamespace
namespace DammitBot
{
    public class AutoMigrationsPlugin : IPlugin
    {
        protected readonly MigrationRunner _runner;

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
}
