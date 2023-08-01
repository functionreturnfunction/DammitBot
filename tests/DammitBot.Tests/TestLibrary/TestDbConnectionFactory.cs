using System.Data;
using DammitBot.Library;
using Microsoft.Data.Sqlite;

namespace DammitBot.TestLibrary
{
    public class TestDbConnectionFactory : SqliteDbConnectionFactory
    {
        protected IDbConnection _connection;

        public TestDbConnectionFactory(IDbConnection connection)
        {
            _connection = connection;
        }

        public override IDbConnection Build(string connectionString)
        {
            return _connection;
        }
    }
}