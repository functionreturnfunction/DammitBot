using DammitBot.Abstract;
using DammitBot.Library;
using Lamar;

namespace DammitBot.Ioc;

public class DataPluginContainerConfiguration : ContainerConfigurationBase
{
    public override void Configure(ServiceRegistry e)
    {
        e.For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>();
        e.For(typeof(IRepository<>)).Use(typeof(Repository<>));
    }
}