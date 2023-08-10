namespace DammitBot.Library;

/// <summary>
/// Repository responsible for managing the persistence and retrieval of <typeparamref name="TEntity"/>
/// instances.
/// </summary>
/// <typeparam name="TEntity">Type of entity the repository deals with.</typeparam>
public interface IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Insert the given instance <paramref name="entity"/> into data persistence.  Return its primary key
    /// value.
    /// </summary>
    object Insert(TEntity entity);
    /// <summary>
    /// Update the given instance <paramref name="entity"/>'s changed properties in persistence.
    /// </summary>
    void Update(TEntity entity);
    /// <summary>
    /// Find the <typeparamref name="TEntity"/> instance with the primary key value <paramref name="id"/>.
    /// Returns null if not found.
    /// </summary>
    TEntity? Find(int id);
}