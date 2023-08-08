using System.Data;
using DammitBot.Abstract;
using DammitBot.Data.Dapper.Repositories;
using DammitBot.Data.Models;
using DammitBot.Data.Repositories;
using DammitBot.Library;
using Lamar;

namespace DammitBot.Ioc;

public class DapperPluginContainerConfiguration : ContainerConfigurationBase
{
    public override void Configure(ServiceRegistry e)
    {
        e.For<IUnitOfWork>().Use<DapperUnitOfWork>();
        e.For<IDataCommandService>().Use<DapperDataCommandService>();

        e.RegisterRepository<Nick, NickRepository, INickRepository>();
        e.RegisterRepository<Message, MessageRepository, IMessageRepository>();
        e.RegisterRepository<User, UserRepository, IUserRepository>();
        
        // required for UnitOfWork
        e.Injectable<IDbConnection>();
    }
}