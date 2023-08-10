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

/// <inheritdoc cref="DapperRepositoryBase{TEntity}"/>
public class UserRepository : DapperRepositoryBase<User>, IUserRepository
{
    #region Constants
    
    /// <inheritdoc cref="BaseQuery" />
    public const string BASE_QUERY = "select * from Users as this";
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="UserRepository"/> class.
    /// </summary>
    public UserRepository(
        IDataCommandService commandService,
        IDbConnection connection,
        IDateTimeProvider dateTimeProvider)
        : base(commandService, connection, dateTimeProvider) {}
    
    #endregion

    #region Properties
    
    /// <inheritdoc />
    protected override string BaseQuery => BASE_QUERY;

    #endregion
    
    #region Private Methods
    
    /// <inheritdoc />
    protected override IEnumerable<User> DoQuery(string sql, object? param = null)
    {
        return _connection.Query<User>(sql, param);
    }
    
    /// <inheritdoc />
    protected override async Task<IEnumerable<User>> DoQueryAsync(string sql, object? param = null)
    {
        return await _connection.QueryAsync<User>(sql, param);
    }

    /// <inheritdoc />
    protected override void FixReferences(User entity) {}

    #endregion

    #region Exposed Methods
    
    /// <inheritdoc />
    public User? FindByUsername(string username)
    {
        return DoQuery(BaseQuery + " where this.Username = @username", new { username })
            .SingleOrDefault();
    }
    
    #endregion
}