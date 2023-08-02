using System.Data;

namespace DammitBot.Library;

public interface IDbConnectionFactory
{
    IDbConnection Build(string connectionString);
}