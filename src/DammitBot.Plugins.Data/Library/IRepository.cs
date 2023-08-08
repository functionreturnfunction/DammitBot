
namespace DammitBot.Library;

/// <summary>
/// Main interface for database access.  Shoud be used in conjunction with an IUnitOfWork.
/// </summary>
/// <typeparam name="TEntity">Type of entity the repository deals with.</typeparam>
public interface IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Insert the given instance <paramref name="entity"/> into data persistence.  Return its primary
    /// key value.
    /// </summary>
    object Insert(TEntity entity);
    /// <summary>
    /// Update the given instance <paramref name="entity"/>'s changed properties in persistence.
    /// </summary>
    void Update(TEntity entity);
    /// <summary>
    /// Find the <typeparamref name="TEntity"/> instance with the primary key value <paramref name="id"/>.
    /// </summary>
    TEntity Find(int id);
}