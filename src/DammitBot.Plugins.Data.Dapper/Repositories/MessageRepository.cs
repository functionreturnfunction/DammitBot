using System.Collections.Generic;
using System.Data;
using System.Linq;
using DammitBot.Data.Library;
using DammitBot.Data.Models;
using Dapper;

namespace DammitBot.Data.Repositories
{
    public class MessageRepository : DapperRepositoryBase<Message>
    {
        public const string BASE_QUERY = @"
select * from Messages this
left join Nicks n
on n.Id = this.FromId
left join Users u
on u.Id = n.UserId";

        protected override string BaseQuery => BASE_QUERY;

        public MessageRepository(IDataCommandHelper helper, IDbConnection connection) : base(helper, connection) {}

        protected override IEnumerable<Message> DoQuery(string sql)
        {
            return _connection.Query<Message, Nick, User, Message>(sql, (msg, nick, user) => {
                msg.From = nick;
                nick.User = user;
                return msg;
            });
        }

        public override object Insert(Message message)
        {
            message.FromId = message.From == null ? message.FromId : message.From.Id;
            return base.Insert(message);
        }
    }
}
