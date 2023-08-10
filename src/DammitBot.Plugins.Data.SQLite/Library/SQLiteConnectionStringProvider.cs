using DammitBot.Configuration;
using Microsoft.Data.Sqlite;

namespace DammitBot.Library;

/// <inheritdoc />
/// <remarks>
/// This implementation provides connection strings for connecting to SQLite databases.
/// </remarks>
public class SQLiteConnectionStringProvider : IConnectionStringProvider
{
    #region Private Members
    
    private readonly IDataConfigurationProvider _config;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="SQLiteConnectionStringProvider"/> class.
    /// </summary>
    public SQLiteConnectionStringProvider(IDataConfigurationProvider config)
    {
        _config = config;
    }
    
    #endregion

    #region Exposed Methods
    
    /// <inheritdoc />
    public string GetMainAppConnectionString()
    {
        return new SqliteConnectionStringBuilder {
            DataSource = _config.ConnectionString == ":memory:" ?
                _config.ConnectionString :
                Path.GetFullPath(_config.ConnectionString)
        }.ToString();
    }
    
    #endregion
}