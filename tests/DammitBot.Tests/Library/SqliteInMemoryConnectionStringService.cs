using Microsoft.Data.Sqlite;

namespace DammitBot.Library
{
    public class SqliteInMemoryConnectionStringService : IConnectionStringService
    {
        public string GetMainAppConnectionString()
        {
            return new SqliteConnectionStringBuilder {
                DataSource = ":memory:",
            }.ToString();
        }
    }
}