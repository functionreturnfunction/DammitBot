using System.Data;
using System.Linq;
using DammitBot.Data.Library;
using DammitBot.Data.Models;
using Dapper;

namespace DammitBot.Data.Repositories
{
    public class NickRepository : RepositoryBase<Nick>
    {
        public const string BASE_QUERY = @"
select * from Nicks n
left join Users u
on u.Id = n.UserId";

        private readonly IDbConnection _connection;

        public NickRepository(IDataCommandHelper helper, IDbConnection connection) : base(helper)
        {
            _connection = connection;
        }

        public override Nick Find(int id)
        {
            var sql = BASE_QUERY + $" where n.Id = {id}";

            return _connection.Query<Nick, User, Nick>(sql, (nick, user) => { nick.User = user; return nick; }).SingleOrDefault();
        }

        protected override IQueryable<Nick> GetQueryable()
        {
            var sql = BASE_QUERY + " order by n.Id";

            return _connection.Query<Nick, User, Nick>(sql, (nick, user) => { nick.User = user; return nick; }).AsQueryable();
        }
    }
}
