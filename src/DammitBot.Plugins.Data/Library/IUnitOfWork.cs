using System;
using System.Data;

namespace DammitBot.Library;

/// <summary>
/// Class which handles database session and transaction details.  If you're working with a repository
/// you need one of these, and if you don't Commit() and Dispose() it your changes will not be saved.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    #region Abstract Methods

    void Commit();
    IRepository<T> GetRepository<T>() where T : class;
    int ExecuteNonQuery(string sql);
    object ExecuteScalar(string sql);
    IDataReader ExecuteReader(string sql);

    #endregion
}