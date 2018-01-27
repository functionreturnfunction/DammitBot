using DammitBot.Abstract;
using DammitBot.Data.Library;
using DammitBot.Data.Dapper.Library;
using StructureMap;

// ReSharper disable once CheckNamespace
namespace DammitBot.Ioc
{
    public class DapperPluginContainerConfiguration : PluginContainerConfigurationBase
    {
        public override void Configure(ConfigurationExpression e)
        {
            e.For<IUnitOfWork>().Use<UnitOfWork>();
            e.For<IDataCommandHelper>().Use<DataCommandHelper>();
        }
    }
}
