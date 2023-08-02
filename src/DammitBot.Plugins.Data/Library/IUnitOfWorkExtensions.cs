using System.Linq;

namespace DammitBot.Library;

public static class IUnitOfWorkExtensions
{
    public static object Insert<TEntity>(this IUnitOfWork that, TEntity entity)
        where TEntity : class
    {
        return that.GetRepository<TEntity>().Insert(entity);
    }

    public static void Update<TEntity>(this IUnitOfWork that, TEntity entity)
        where TEntity : class
    {
        that.GetRepository<TEntity>().Update(entity);
    }

    public static TEntity Find<TEntity>(this IUnitOfWork that, int id)
        where TEntity : class
    {
        return that.GetRepository<TEntity>().Find(id);
    }

    public static IQueryable<TEntity> Query<TEntity>(this IUnitOfWork that)
        where TEntity : class
    {
        return that.GetRepository<TEntity>();
    }
}