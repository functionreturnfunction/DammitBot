using DammitBot.Abstract;
using DammitBot.Data.Library;
using StructureMap;

// ReSharper disable once CheckNamespace
namespace DammitBot.Ioc
{
    public class DataPluginContainerConfiguration : PluginContainerConfigurationBase
    {
        public override void Configure(ConfigurationExpression e)
        {
            e.For(typeof(ISessionFactoryBuilder)).Singleton().Use(typeof(SessionFactoryBuilder));
        }
    }
}
