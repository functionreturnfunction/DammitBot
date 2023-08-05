using System.Collections.Generic;
using System.Data;
using DammitBot.Data.Models;
using DammitBot.Library;
using Dapper;
using DateTimeProvider;

namespace DammitBot.Data.Dapper.Repositories;

public class UserRepository : DapperRepositoryBase<User>
{
    public const string BASE_QUERY = "select * from Users as this";

    public UserRepository(
        IDataCommandHelper helper,
        IDbConnection connection,
        IDateTimeProvider dateTimeProvider)
        : base(helper, connection, dateTimeProvider) {}

    protected override string BaseQuery => BASE_QUERY;

    protected override IEnumerable<User> DoQuery(string sql)
    {
        return _connection.Query<User>(sql);
    }

    protected override void FixReferences(User entity) {}
}