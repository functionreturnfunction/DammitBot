using DammitBot.Abstract;
using DammitBot.Library;
using StructureMap;

namespace DammitBot.Ioc;

public class DataPluginContainerConfiguration : ContainerConfigurationBase
{
    public override void Configure(ConfigurationExpression e)
    {
        e.For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>();
        e.For(typeof(IRepository<>)).Use(typeof(Repository<>));
    }
}