using System.Collections.Generic;
using System.Data;
using System.Linq;
using DammitBot.Data.Models;
using DammitBot.Data.Repositories;
using DammitBot.Library;
using Dapper;
using DateTimeProvider;

namespace DammitBot.Data.Dapper.Repositories;

public class UserRepository : DapperRepositoryBase<User>, IUserRepository
{
    public const string BASE_QUERY = "select * from Users as this";

    public UserRepository(
        IDataCommandService commandService,
        IDbConnection connection,
        IDateTimeProvider dateTimeProvider)
        : base(commandService, connection, dateTimeProvider) {}

    protected override string BaseQuery => BASE_QUERY;

    protected override IEnumerable<User> DoQuery(string sql, object? param = null)
    {
        return _connection.Query<User>(sql, param);
    }

    protected override void FixReferences(User entity) {}
    
    public User? FindByUsername(string username)
    {
        return DoQuery(BaseQuery + " where this.Username = @username", new { username })
            .SingleOrDefault();
    }
}