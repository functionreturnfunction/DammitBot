using System.Data;
using DammitBot.Abstract;

namespace DammitBot.Plugins.Data.AutoMigrations
{
    public class AutoMigrationsPlugin : IPlugin
    {
        protected readonly IDbConnection _connection;

        public AutoMigrationsPlugin(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Initialize()
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='nicks'";
            if (cmd.ExecuteScalar() != null)
            {
                return;
            }

            cmd.CommandText = "";
        }

        public void Cleanup() {}
    }
}
