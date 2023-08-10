using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DammitBot.Data.Models;
using DammitBot.Data.Repositories;
using DammitBot.Library;
using Dapper;
using DateTimeProvider;

namespace DammitBot.Data.Dapper.Repositories;

/// <inheritdoc cref="DapperRepositoryBase{TEntity}"/>
public class MessageRepository : DapperRepositoryBase<Message>, IMessageRepository
{
    #region Constants
    
    /// <inheritdoc cref="BaseQuery" />
    public const string BASE_QUERY = @"
select * from Messages this
left join Nicks n
on n.Id = this.FromId
left join Users u
on u.Id = n.UserId";
    
    #endregion
    
    #region Properties

    /// <inheritdoc />
    protected override string BaseQuery => BASE_QUERY;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="MessageRepository"/> class.
    /// </summary>
    public MessageRepository(
        IDataCommandService commandService,
        IDbConnection connection,
        IDateTimeProvider dateTimeProvider)
        : base(commandService, connection, dateTimeProvider) {}
    
    #endregion
    
    #region Private Methods

    private Message MapQueryResult(Message message, Nick nick, User user)
    {
        message.From = nick;
        nick.User = user;
        return message;
    }

    /// <inheritdoc />
    protected override void FixReferences(Message message)
    {
        message.FromId = message.From == null ? message.FromId : message.From.Id;
    }

    /// <inheritdoc />
    protected override IEnumerable<Message> DoQuery(string sql, object? param = null)
    {
        return _connection.Query<Message, Nick, User, Message>(sql, MapQueryResult, param);
    }

    /// <inheritdoc />
    protected override async Task<IEnumerable<Message>> DoQueryAsync(string sql, object? param = null)
    {
        return await _connection.QueryAsync<Message, Nick, User, Message>(sql, MapQueryResult, param);
    }
    
    #endregion
}