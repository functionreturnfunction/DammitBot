using System.Data;
using System.Linq;
using Dommel;

namespace DammitBot.Library;

public class DapperDataCommandHelper : IDataCommandHelper
{
    #region Private Members

    private readonly IDbConnection _connection;

    #endregion

    #region Constructors

    public DapperDataCommandHelper(IDbConnection connection)
    {
        _connection = connection;
    }

    #endregion

    #region Exposed Methods

    public object Insert<TEntity>(TEntity entity)
        where TEntity : class
    {
        return _connection.Insert(entity);
    }

    public void Update<TEntity>(TEntity entity)
        where TEntity : class
    {
        _connection.Update(entity);
    }

    public T Load<T>(int id)
        where T : class
    {
        return _connection.Get<T>(id);
    }

    public IQueryable<T> GetQueryable<T>()
        where T : class
    {
        return _connection.GetAll<T>().ToList().AsQueryable();
    }

    #endregion
}