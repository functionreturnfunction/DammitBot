using System.Data;
using System.Linq;
using DammitBot.Data.Library;
using DammitBot.Data.Models;
using Dapper;

namespace DammitBot.Data.Repositories
{
    public class MessageRepository : RepositoryBase<Message>
    {
        public const string BASE_QUERY = @"
select * from Messages m
left join Nicks n
on n.Id = m.FromId
left join Users u
on u.Id = n.UserId";

        private readonly IDbConnection _connection;

        public MessageRepository(IDataCommandHelper helper, IDbConnection connection) : base(helper)
        {
            _connection = connection;
        }

        public override object Insert(Message message)
        {
            message.FromId = message.From == null ? message.FromId : message.From.Id;
            return base.Insert(message);
        }

        public override Message Find(int id)
        {
            var sql = BASE_QUERY + $" where m.Id = {id}";

            return _connection.Query<Message, Nick, User, Message>(sql, (msg, nick, user) => {
                msg.From = nick;
                nick.User = user;
                return msg;
            }).SingleOrDefault();
        }
    }
}
