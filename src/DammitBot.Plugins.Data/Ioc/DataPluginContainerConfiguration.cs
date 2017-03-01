using DammitBot.Abstract;
using DammitBot.Data.Library;
using LeadPipe.Net.Data;
using LeadPipe.Net.Data.NHibernate;
using LeadPipe.Net.Domain;
using StructureMap;

// ReSharper disable once CheckNamespace
namespace DammitBot.Ioc
{
    public class DataPluginContainerConfiguration : PluginContainerConfigurationBase
    {
        public override void Configure(ConfigurationExpression e)
        {
            e.For(typeof(ISessionFactoryBuilder)).Singleton().Use(typeof(SessionFactoryBuilder));
            e.For(typeof(IDataSessionProvider<>)).Use(typeof(DataSessionProvider));
            e.For(typeof(IActiveDataSessionManager<>)).Use(typeof(ActiveDataSessionManager));
            e.For<IDataCommandProvider>().Use<DataCommandProvider>();
            e.For(typeof(IObjectFinder<>)).Use(typeof(ObjectFinder<>));
            e.For<IUnitOfWorkFactory>().Singleton().Use<UnitOfWorkFactory>();
            e.For(typeof(IRepository<>)).Use(typeof(Repository<>));
        }
    }
}
