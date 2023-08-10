using System.Data;
using Microsoft.Data.Sqlite;

namespace DammitBot.Library;

/// <inheritdoc />
/// <remarks>
/// This implementation builds instances of <see cref="SqliteConnection"/> for connecting to SQLite
/// databases.
/// </remarks>
public class SQLiteDbConnectionFactory : IDbConnectionFactory
{
    #region Exposed Methods
    
    /// <inheritdoc />
    /// <inheritdoc cref="SQLiteDbConnectionFactory" path="remarks" />
    public virtual IDbConnection Build(string connectionString)
    {
        return new SqliteConnection(connectionString);
    }
    
    #endregion
}