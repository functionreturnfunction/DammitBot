using System.Data;

namespace DammitBot.Library;

public static class IDBConnectionExtensions
{
    public static IDbCommand GetCommand(this IDbConnection connection, string sql)
    {
        var cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        return cmd;
    }

    public static int ExecuteNonQuery(this IDbConnection connection, string sql)
    {
        return connection.GetCommand(sql).ExecuteNonQuery();
    }

    public static object? ExecuteScalar(this IDbConnection connection, string sql)
    {
        return connection.GetCommand(sql).ExecuteScalar();
    }
}