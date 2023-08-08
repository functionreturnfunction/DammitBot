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
        e.For<IDataCommandService>().Use<DapperDataCommandService>();

        e.RegisterRepository<Nick, NickRepository>();
        e.RegisterRepository<Message, MessageRepository>();
        e.RegisterRepository<User, UserRepository>();
        
        // required for UnitOfWork
        e.Injectable<IDbConnection>();
    }
}