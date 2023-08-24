using System.Threading.Tasks;

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
    /// <inheritdoc cref="Insert{TEntity}"/>
    /// <remarks>Asynchronously.</remarks>
    Task<object> InsertAsync<TEntity>(TEntity entity) where TEntity : class;
    /// <summary>
    /// Update any changed properties on the provided <paramref name="entity"/> onto its representation
    /// in persistent data storage. 
    /// </summary>
    void Update<TEntity>(TEntity entity) where TEntity : class;
    /// <inheritdoc cref="Update{TEntity}"/>
    /// <remarks>Asynchronously.</remarks>
    Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class;
    /// <summary>
    /// Retrieve the <typeparamref name="TEntity"/> represented by <paramref name="id"/> from persistent
    /// data storage. Return null if entity was not found.
    /// </summary>
    TEntity? Load<TEntity>(int id) where TEntity : class;

    #endregion
}