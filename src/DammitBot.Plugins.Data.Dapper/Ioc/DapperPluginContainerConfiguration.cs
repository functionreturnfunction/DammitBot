using System.Data;
using DammitBot.Abstract;
using DammitBot.Data.Library;
using DammitBot.Data.Models;
using DammitBot.Data.Dapper.Library;
using DammitBot.Data.Dapper.Repositories;
using StructureMap;
using Microsoft.Data.Sqlite;

// ReSharper disable once CheckNamespace
namespace DammitBot.Ioc
{
    public class DapperPluginContainerConfiguration : ContainerConfigurationBase
    {
        public override void Configure(ConfigurationExpression e)
        {
            e.For<IUnitOfWork>().Use<UnitOfWork>();
            e.For<IDataCommandHelper>().Use<DataCommandHelper>();
            e.For<IDbConnectionFactory>().Use<SqliteDbConnectionFactory>();
            e.For<IConnectionStringService>().Use<SqliteConnectionStringService>();

            e.For<IRepository<Nick>>().Use<NickRepository>();
            e.For<IRepository<Message>>().Use<MessageRepository>();
            e.For<IRepository<User>>().Use<UserRepository>();
        }
    }
}
