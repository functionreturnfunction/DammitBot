using Lamar;
using Lamar.IoC.Instances;

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
    public static ConstructorInstance<TRepository, IRepository<TEntity>>
        RegisterRepository<TEntity, TRepository>(this ServiceRegistry e)
        where TRepository : class, IRepository<TEntity>
        where TEntity : class
    {
        return e.For<IRepository<TEntity>>().Use<TRepository>();
    }
}