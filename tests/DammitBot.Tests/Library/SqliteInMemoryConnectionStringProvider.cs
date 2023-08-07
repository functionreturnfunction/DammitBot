using Microsoft.Data.Sqlite;

namespace DammitBot.Library;

public class SqliteInMemoryConnectionStringProvider : IConnectionStringProvider
{
    public string GetMainAppConnectionString()
    {
        return new SqliteConnectionStringBuilder {
            DataSource = ":memory:",
        }.ToString();
    }
}