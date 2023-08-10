using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DammitBot.Data.Models;
using DammitBot.Data.Repositories;
using DammitBot.Library;
using Dapper;
using DateTimeProvider;

namespace DammitBot.Data.Dapper.Repositories;

/// <inheritdoc cref="DapperRepositoryBase{TEntity}" />
public class ReminderRepository : DapperRepositoryBase<Reminder>, IReminderRepository
{
    #region Constants
    
    /// <inheritdoc cref="BaseQuery" />
    public const string BASE_QUERY = @"
select * from Reminders this
left join Users f
on f.Id = this.FromId
left join Users t
on t.Id = this.ToId";
    
    #endregion
    
    #region Properties

    /// <inheritdoc />
    protected override string BaseQuery => BASE_QUERY;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor fot the <see cref="ReminderRepository"/> class.
    /// </summary>
    public ReminderRepository(
        IDataCommandService commandService,
        IDbConnection connection,
        IDateTimeProvider dateTimeProvider)
        : base(commandService, connection, dateTimeProvider) { }
    
    #endregion

    #region Private Methods

    private Reminder MapQueryResult(Reminder reminder, User from, User to)
    {
        reminder.From = from;
        reminder.To = to;
        return reminder;
    }
    
    /// <inheritdoc />
    protected override IEnumerable<Reminder> DoQuery(string sql, object? param = null)
    {
        return _connection.Query<Reminder, User, User, Reminder>(sql, MapQueryResult, param);
    }
    
    /// <inheritdoc />
    protected override async Task<IEnumerable<Reminder>> DoQueryAsync(string sql, object? param = null)
    {
        return await _connection.QueryAsync<Reminder, User, User, Reminder>(sql, MapQueryResult, param);
    }

    /// <inheritdoc />
    protected override void FixReferences(Reminder entity)
    {
        entity.FromId = entity.From == null ? entity.FromId : entity.From.Id;
        entity.ToId = entity.To == null ? entity.ToId : entity.To.Id;
    }
    
    #endregion
    
    #region Public Methods

    /// <inheritdoc />
    public async Task<IEnumerable<Reminder>> GetPendingAsync(DateTime since)
    {
        return await DoQueryAsync(
            BaseQuery +
            " where this.RemindedAt IS NULL and this.RemindAt IS NOT NULL and this.RemindAt <= @since",
            new { since });
    }
    
    #endregion
}