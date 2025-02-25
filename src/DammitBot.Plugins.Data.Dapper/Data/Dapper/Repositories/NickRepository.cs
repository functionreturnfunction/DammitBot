using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DammitBot.Data.Models;
using DammitBot.Data.Repositories;
using DammitBot.Library;
using Dapper;
using DateTimeProvider;

namespace DammitBot.Data.Dapper.Repositories;

/// <inheritdoc cref="DapperRepositoryBase{Nick}"/>
public class NickRepository : DapperRepositoryBase<Nick>, INickRepository
{
    #region Constants
    
    /// <inheritdoc cref="BaseQuery" />
    public const string BASE_QUERY = @"
select * from Nicks this
left join Users u
on u.Id = this.UserId";
    
    #endregion
    
    #region Properties

    /// <inheritdoc />
    protected override string BaseQuery => BASE_QUERY;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="NickRepository"/> class.
    /// </summary>
    public NickRepository(
        IDataCommandService commandService,
        IDbConnection connection,
        IDateTimeProvider dateTimeProvider)
        : base(commandService, connection, dateTimeProvider) {}
    
    #endregion
    
    #region Private Methods

    private Nick MapQueryResult(Nick nick, User user)
    {
        nick.User = user;
        return nick;
    }

    /// <inheritdoc />
    protected override IEnumerable<Nick> DoQuery(string sql, object? param = null)
    {
        return _connection.Query<Nick, User, Nick>(sql, MapQueryResult, param);
    }

    /// <inheritdoc />
    protected override async Task<IEnumerable<Nick>> DoQueryAsync(string sql, object? param = null)
    {
        return  await _connection.QueryAsync<Nick, User, Nick>(sql, MapQueryResult, param);
    }

    /// <inheritdoc />
    protected override void FixReferences(Nick entity)
    {
        entity.UserId = entity.User == null ? entity.UserId : entity.User.Id;
    }
    
    #endregion
    
    #region Exposed Methods

    /// <inheritdoc />
    public Nick? FindByNicknameAndProtocol(string nickname, string protocol)
    {
        return DoQuery(
                BaseQuery + " where this.Nickname = @nickname and this.Protocol = @protocol",
                new { nickname, protocol })
            .SingleOrDefault();
    }
    
    #endregion
}