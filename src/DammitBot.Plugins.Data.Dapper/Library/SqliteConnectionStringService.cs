using System.IO;
using DammitBot.Configuration;
using Microsoft.Data.Sqlite;

namespace DammitBot.Library;

public class SqliteConnectionStringService : IConnectionStringService
{
    protected readonly IDataConfigurationManager _config;

    public SqliteConnectionStringService(IDataConfigurationManager config)
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