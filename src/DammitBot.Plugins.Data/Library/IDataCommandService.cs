using System.Linq;

namespace DammitBot.Library;

/// <summary>
/// Service which abstracts away any ORM- or RDBMS-specific details involved in performing CRUD operations
/// against persistence.
/// </summary>
public interface IDataCommandService
{
    #region Abstract Methods

    /// <summary>
    /// Insert the provided <paramref name="entity"/> into persistent data storage. 
    /// </summary>
    object Insert<TEntity>(TEntity entity) where TEntity : class;
    /// <summary>
    /// Update any changed properties on the provided <paramref name="entity"/> onto its representation
    /// in persistent data storage. 
    /// </summary>
    void Update<TEntity>(TEntity entity) where TEntity : class;
    /// <summary>
    /// Retrieve the <typeparamref name="TEntity"/> represented by <paramref name="id"/> from persistent
    /// data storage. 
    /// </summary>
    TEntity Load<TEntity>(int id) where TEntity : class;
    /// <summary>
    /// Return a query root object which can perform queries against instances of
    /// <typeparamref name="TEntity"/> from persistent data storage. 
    /// </summary>
    IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class;

    #endregion
}