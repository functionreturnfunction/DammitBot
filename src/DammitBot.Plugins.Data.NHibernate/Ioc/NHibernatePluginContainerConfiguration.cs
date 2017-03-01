using DammitBot.Abstract;
using DammitBot.Data.Library;
using DammitBot.Data.NHibernate.Library;
using NHibernate;
using StructureMap;

// ReSharper disable once CheckNamespace
namespace DammitBot.Ioc
{
    public class NHibernatePluginContainerConfiguration : PluginContainerConfigurationBase
    {
        public override void Configure(ConfigurationExpression e)
        {
            e.For(typeof(ISessionFactoryBuilder)).Singleton().Use(typeof(SessionFactoryBuilder));
            e.For(typeof(ISessionFactory)).Use(ctx => ctx.GetInstance<ISessionFactoryBuilder>().Build());
            e.For(typeof(ISession)).Use(ctx => ctx.GetInstance<ISessionFactory>().OpenSession());
            e.For<IUnitOfWork>().Use<UnitOfWork>();
            e.For<IDataCommandHelper>().Use<DataCommandHelper>();
        }
    }
}
