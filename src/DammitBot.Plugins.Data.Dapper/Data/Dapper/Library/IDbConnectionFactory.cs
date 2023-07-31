using System.Data;

namespace DammitBot.Data.Dapper.Library
{
    public interface IDbConnectionFactory
    {
        IDbConnection Build(string connectionString);
    }
}