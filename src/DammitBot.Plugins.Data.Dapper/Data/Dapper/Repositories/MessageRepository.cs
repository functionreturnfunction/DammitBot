using System.Collections.Generic;
using System.Data;
using DammitBot.Data.Models;
using DammitBot.Data.Repositories;
using DammitBot.Library;
using Dapper;
using DateTimeProvider;

namespace DammitBot.Data.Dapper.Repositories;

public class MessageRepository : DapperRepositoryBase<Message>, IMessageRepository
{
    public const string BASE_QUERY = @"
select * from Messages this
left join Nicks n
on n.Id = this.FromId
left join Users u
on u.Id = n.UserId";

    protected override string BaseQuery => BASE_QUERY;

    public MessageRepository(
        IDataCommandService commandService,
        IDbConnection connection,
        IDateTimeProvider dateTimeProvider)
        : base(commandService, connection, dateTimeProvider) {}

    protected override void FixReferences(Message message)
    {
        message.FromId = message.From == null ? message.FromId : message.From.Id;
    }

    protected override IEnumerable<Message> DoQuery(string sql, object? param = null)
    {
        return _connection.Query<Message, Nick, User, Message>(sql, (msg, nick, user) => {
            msg.From = nick;
            nick.User = user;
            return msg;
        }, param);
    }
}