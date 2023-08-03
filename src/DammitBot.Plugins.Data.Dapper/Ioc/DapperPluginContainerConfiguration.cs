using System.Data;
using DammitBot.Abstract;
using DammitBot.Data.Dapper.Repositories;
using DammitBot.Data.Models;
using DammitBot.Library;
using Lamar;

namespace DammitBot.Ioc;

public class DapperPluginContainerConfiguration : ContainerConfigurationBase
{
    public override void Configure(ServiceRegistry e)
    {
        e.For<IUnitOfWork>().Use<DapperUnitOfWork>();
        e.For<IDataCommandHelper>().Use<DapperDataCommandHelper>();
        e.For<IDbConnectionFactory>().Use<SqliteDbConnectionFactory>();
        e.For<IConnectionStringService>().Use<SqliteConnectionStringService>();

        e.For<IRepository<Nick>>().Use<NickRepository>();
        e.For<IRepository<Message>>().Use<MessageRepository>();
        e.For<IRepository<User>>().Use<UserRepository>();
        
        // required for UnitOfWork
        e.Injectable<IDbConnection>();
    }
}