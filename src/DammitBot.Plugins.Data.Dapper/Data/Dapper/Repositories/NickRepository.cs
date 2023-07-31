using System.Collections.Generic;
using System.Data;
using System.Linq;
using DammitBot.Data.Dapper.Library;
using DammitBot.Data.Library;
using DammitBot.Data.Models;
using DateTimeStringParser;
using Dapper;

namespace DammitBot.Data.Dapper.Repositories
{
    public class NickRepository : DapperRepositoryBase<Nick>
    {
        public const string BASE_QUERY = @"
select * from Nicks this
left join Users u
on u.Id = this.UserId";

        protected override string BaseQuery => BASE_QUERY;

        public NickRepository(IDataCommandHelper helper, IDbConnection connection, IDateTimeProvider dateTimeProvider) : base(helper, connection, dateTimeProvider) {}

        protected override IEnumerable<Nick> DoQuery(string sql)
        {
            return _connection.Query<Nick, User, Nick>(sql, (nick, user) => {
                nick.User = user;
                return nick;
            });
        }

        protected override void FixReferences(Nick entity)
        {
            entity.UserId = entity.User == null ? entity.UserId : entity.User.Id;
        }
    }
}
