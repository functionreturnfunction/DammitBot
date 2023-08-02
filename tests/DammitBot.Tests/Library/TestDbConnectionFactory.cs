using System.Data;

namespace DammitBot.Library;

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