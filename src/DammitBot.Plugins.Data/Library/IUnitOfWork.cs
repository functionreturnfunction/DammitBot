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

    /// <summary>
    /// Commit any changes made via this instance to data persistence.
    /// </summary>
    void Commit();
    /// <summary>
    /// Get a repository responsible for entities of type <typeparamref name="TEntity"/>.
    /// </summary>
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    /// <summary>
    /// Execute the given <paramref name="sql"/> statement and return the number of changed rows.
    /// </summary>
    int ExecuteNonQuery(string sql);
    /// <summary>
    /// Execute the given <paramref name="sql"/> query and return the value in the first column of the
    /// first row of the results.
    /// </summary>
    object ExecuteScalar(string sql);
    /// <summary>
    /// Execute the given <paramref name="sql"/> query and build an <see cref="IDataReader"/>.
    /// </summary>
    IDataReader ExecuteReader(string sql);

    #endregion
}