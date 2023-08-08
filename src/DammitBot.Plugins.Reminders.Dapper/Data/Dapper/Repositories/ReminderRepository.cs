using System;
using System.Collections.Generic;
using System.Data;
using DammitBot.Data.Models;
using DammitBot.Data.Repositories;
using DammitBot.Library;
using Dapper;
using DateTimeProvider;

namespace DammitBot.Data.Dapper.Repositories;

public class ReminderRepository : DapperRepositoryBase<Reminder>, IReminderRepository
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

    public ReminderRepository(
        IDataCommandService commandService,
        IDbConnection connection,
        IDateTimeProvider dateTimeProvider)
        : base(commandService, connection, dateTimeProvider)
    {
    }

    protected override string BaseQuery => BASE_QUERY;

    protected override IEnumerable<Reminder> DoQuery(string sql, object? param = null)
    {
        return _connection.Query<Reminder, User, User, Reminder>(
            sql,
            (rmnd, from, to) =>
            {
                rmnd.From = from;
                rmnd.To = to;
                return rmnd;
            }, param);
    }

    protected override void FixReferences(Reminder entity)
    {
        entity.FromId = entity.From == null ? entity.FromId : entity.From.Id;
        entity.ToId = entity.To == null ? entity.ToId : entity.To.Id;
    }

    public IEnumerable<Reminder> GetPending(DateTime since)
    {
        return DoQuery(
            BaseQuery + " where this.RemindAt IS NOT NULL and this.RemindAt <= @since",
            new { since });
    }
}