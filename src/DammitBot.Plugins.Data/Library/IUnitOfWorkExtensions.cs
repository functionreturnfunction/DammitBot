namespace DammitBot.Library;

/// <summary>
/// Extensions to the <see cref="IUnitOfWork"/> interface.
/// </summary>
public static class IUnitOfWorkExtensions
{
    /// <inheritdoc cref="IRepository{TEntity}.Insert"/>
    public static object Insert<TEntity>(this IUnitOfWork that, TEntity entity)
        where TEntity : class
    {
        return that.GetEntityRepository<TEntity>().Insert(entity);
    }

    /// <inheritdoc cref="IRepository{TEntity}.Update"/>
    public static void Update<TEntity>(this IUnitOfWork that, TEntity entity)
        where TEntity : class
    {
        that.GetEntityRepository<TEntity>().Update(entity);
    }

    /// <inheritdoc cref="IRepository{TEntity}.Find"/>
    public static TEntity Find<TEntity>(this IUnitOfWork that, int id)
        where TEntity : class
    {
        return that.GetEntityRepository<TEntity>().Find(id);
    }
}