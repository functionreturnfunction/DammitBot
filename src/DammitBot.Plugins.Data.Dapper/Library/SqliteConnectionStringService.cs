using System.IO;
using DammitBot.Configuration;
using DammitBot.Data.Library;
using Microsoft.Data.Sqlite;

namespace DammitBot.Data.Dapper.Library
{
    public class SqliteConnectionStringService : IConnectionStringService
    {
        protected IDataConfigurationManager _config;

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
}