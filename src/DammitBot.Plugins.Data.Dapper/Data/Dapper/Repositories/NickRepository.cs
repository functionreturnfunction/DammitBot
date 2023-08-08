using System.Collections.Generic;
using System.Data;
using System.Linq;
using DammitBot.Data.Models;
using DammitBot.Data.Repositories;
using DammitBot.Library;
using Dapper;
using DateTimeProvider;

namespace DammitBot.Data.Dapper.Repositories;

public class NickRepository : DapperRepositoryBase<Nick>, INickRepository
{
    public const string BASE_QUERY = @"
select * from Nicks this
left join Users u
on u.Id = this.UserId";

    protected override string BaseQuery => BASE_QUERY;

    public NickRepository(
        IDataCommandService commandService,
        IDbConnection connection,
        IDateTimeProvider dateTimeProvider)
        : base(commandService, connection, dateTimeProvider) {}

    protected override IEnumerable<Nick> DoQuery(string sql, object? param = null)
    {
        return _connection.Query<Nick, User, Nick>(sql, (nick, user) => {
            nick.User = user;
            return nick;
        }, param);
    }

    protected override void FixReferences(Nick entity)
    {
        entity.UserId = entity.User == null ? entity.UserId : entity.User.Id;
    }

    public Nick? FindByNickname(string nickname)
    {
        return DoQuery(BaseQuery + " where this.Nickname = @nickname", new { nickname })
            .SingleOrDefault();
    }
}