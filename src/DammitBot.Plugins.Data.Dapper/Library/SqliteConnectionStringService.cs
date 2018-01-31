using DammitBot.Data.Library;
using Microsoft.Data.Sqlite;

namespace DammitBot.Data.Dapper.Library
{
    public class SqliteConnectionStringService : IConnectionStringService
    {
        public string GetMainAppConnectionString()
        {
            return new SqliteConnectionStringBuilder {
                DataSource = "db/dev.db"
            }.ToString();
        }
    }
}