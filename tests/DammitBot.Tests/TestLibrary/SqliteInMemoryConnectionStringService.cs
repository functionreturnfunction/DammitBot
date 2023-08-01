using DammitBot.Library;
using Microsoft.Data.Sqlite;

namespace DammitBot.TestLibrary
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