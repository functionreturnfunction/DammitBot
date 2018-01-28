using System.Collections.Generic;
using System.Data;
using System.Linq;
using DammitBot.Data.Library;
using DammitBot.Data.Models;
using Dapper;

namespace DammitBot.Data.Repositories
{
    public class NickRepository : DapperRepositoryBase<Nick>
    {
        public const string BASE_QUERY = @"
select * from Nicks this
left join Users u
on u.Id = this.UserId";

        protected override string BaseQuery => BASE_QUERY;

        public NickRepository(IDataCommandHelper helper, IDbConnection connection) : base(helper, connection) {}

        protected override IEnumerable<Nick> DoQuery(string sql)
        {
            return _connection.Query<Nick, User, Nick>(sql, (nick, user) => {
                nick.User = user;
                return nick;
            });
        }

        public override object Insert(Nick entity)
        {
            entity.UserId = entity.User == null ? entity.UserId : entity.User.Id;
            return base.Insert(entity);
        }
    }
}
