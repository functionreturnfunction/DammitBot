using System.Data;
using Microsoft.Data.Sqlite;

namespace DammitBot.Data.Dapper.Library
{
    public class SqliteDbConnectionFactory : IDbConnectionFactory
    {
        public virtual IDbConnection Build(string connectionString)
        {
            return new SqliteConnection(connectionString);
        }
    }
}