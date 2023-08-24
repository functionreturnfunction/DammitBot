using System.Data;
using System.Threading.Tasks;
using Dommel;

namespace DammitBot.Library;

/// <inheritdoc />
/// <remarks>
/// This implementation uses an <see cref="IDbConnection"/> and <see cref="Dapper"/> extensions to it in
/// order to perform data commands.
/// </remarks>
public class DapperDataCommandService : IDataCommandService
{
    #region Private Members

    private readonly IDbConnection _connection;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="DapperDataCommandService"/> class.
    /// </summary>
    public DapperDataCommandService(IDbConnection connection)
    {
        _connection = connection;
    }

    #endregion

    #region Exposed Methods

    /// <inheritdoc />
    public object Insert<TEntity>(TEntity entity)
        where TEntity : class
    {
        return _connection.Insert(entity);
    }

    /// <inheritdoc />
    public async Task<object> InsertAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        return await _connection.InsertAsync(entity);
    }

    /// <inheritdoc />
    public void Update<TEntity>(TEntity entity)
        where TEntity : class
    {
        _connection.Update(entity);
    }

    /// <inheritdoc />
    public async Task UpdateAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        await _connection.UpdateAsync(entity);
    }

    /// <inheritdoc />
    public TEntity? Load<TEntity>(int id)
        where TEntity : class
    {
        return _connection.Get<TEntity>(id);
    }

    #endregion
}