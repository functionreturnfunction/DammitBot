using System.Collections.Generic;
using System.Data;
using DammitBot.Data.Dapper.Library;
using DammitBot.Library;
using DammitBot.Models;
using DateTimeStringParser;
using Dapper;

namespace DammitBot.Data.Dapper.Repositories
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

        public MessageRepository(IDataCommandHelper helper, IDbConnection connection, IDateTimeProvider dateTimeProvider) : base(helper, connection, dateTimeProvider) {}

        protected override void FixReferences(Message message)
        {
            message.FromId = message.From == null ? message.FromId : message.From.Id;
        }

        protected override IEnumerable<Message> DoQuery(string sql)
        {
            return _connection.Query<Message, Nick, User, Message>(sql, (msg, nick, user) => {
                msg.From = nick;
                nick.User = user;
                return msg;
            });
        }
    }
}
