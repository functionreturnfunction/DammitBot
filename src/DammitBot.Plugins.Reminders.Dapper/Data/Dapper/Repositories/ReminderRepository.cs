using System.Collections.Generic;
using System.Data;
using DammitBot.Data.Models;
using DammitBot.Library;
using DateTimeStringParser;
using Dapper;

namespace DammitBot.Data.Dapper.Repositories
{
    public class ReminderRepository : DapperRepositoryBase<Reminder>
    {
//                 public const string BASE_QUERY = @"
// select * from Messages this
// left join Nicks n
// on n.Id = this.FromId
// left join Users u
// on u.Id = n.UserId";

        public const string BASE_QUERY = @"
select * from Reminders this
left join Users f
on f.Id = this.FromId
left join Users t
on t.Id = this.ToId";

        public ReminderRepository(IDataCommandHelper helper, IDbConnection connection, IDateTimeProvider dateTimeProvider) : base(helper, connection, dateTimeProvider) {}

        protected override string BaseQuery => BASE_QUERY;

        protected override IEnumerable<Reminder> DoQuery(string sql)
        {
            // return _connection.Query<Message, Nick, User, Message>(sql, (msg, nick, user) => {
            //     msg.From = nick;
            //     nick.User = user;
            //     return msg;
            // });
            return _connection.Query<Reminder, User, User, Reminder>(sql, (rmnd, from, to) => {
                rmnd.From = from;
                rmnd.To = to;
                return rmnd;
            });
        }

        protected override void FixReferences(Reminder entity)
        {
            entity.FromId = entity.From == null ? entity.FromId : entity.From.Id;
            entity.ToId = entity.To == null ? entity.ToId : entity.To.Id;
        }
    }
}