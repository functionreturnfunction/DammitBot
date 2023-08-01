using System.Data;
using Microsoft.Data.Sqlite;

namespace DammitBot.Library
{
    public class SqliteDbConnectionFactory : IDbConnectionFactory
    {
        public virtual IDbConnection Build(string connectionString)
        {
            return new SqliteConnection(connectionString);
        }
    }
}