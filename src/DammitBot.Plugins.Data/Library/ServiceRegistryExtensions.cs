using Lamar;

namespace DammitBot.Library;

/// <summary>
/// Extensions for the <see cref="ServiceRegistryExtensions"/> class.
/// </summary>
public static class ServiceRegistryExtensions
{
    /// <summary>
    /// Register <typeparamref name="TRepository"/> as the repository for <typeparamref name="TEntity"/>
    /// instances. 
    /// </summary>
    public static void RegisterRepository<TEntity, TRepository, TRepositoryInterface>(
        this ServiceRegistry e)
        where TRepositoryInterface : class, IRepository<TEntity>
        where TRepository : class, TRepositoryInterface
        where TEntity : class
    {
        e.For<IRepository<TEntity>>().Use<TRepository>();
        e.For<TRepositoryInterface>().Use<TRepository>();
    }
}