using System.IO;
using DammitBot.Configuration;
using Microsoft.Data.Sqlite;

namespace DammitBot.Library;

public class SqliteConnectionStringProvider : IConnectionStringProvider
{
    protected readonly IDataConfigurationProvider _config;

    public SqliteConnectionStringProvider(IDataConfigurationProvider config)
    {
        _config = config;
    }

    public string GetMainAppConnectionString()
    {
        return new SqliteConnectionStringBuilder {
            DataSource = _config.ConnectionString == ":memory:" ?
                _config.ConnectionString :
                Path.GetFullPath(_config.ConnectionString)
        }.ToString();
    }
}