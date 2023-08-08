using System.Data;
using DammitBot.Abstract;
using DammitBot.Data.Dapper.Repositories;
using DammitBot.Data.Models;
using DammitBot.Data.Repositories;
using DammitBot.Library;
using Lamar;

namespace DammitBot.IoC;

/// <inheritdoc />
/// <remarks>
/// This implementation registers <see cref="Dapper"/>-specific implementations of types from the
/// DammitBot.Data plugin.
/// </remarks>
public class DapperPluginContainerConfiguration : ContainerConfigurationBase
{
    #region Exposed Methods
    
    /// <inheritdoc />
    /// <inheritdoc cref="DapperPluginContainerConfiguration" path="remarks" />
    public override void Configure(ServiceRegistry e)
    {
        e.For<IUnitOfWork>().Use<DapperUnitOfWork>();
        e.For<IDataCommandService>().Use<DapperDataCommandService>();

        e.RegisterRepository<Nick, NickRepository, INickRepository>();
        e.RegisterRepository<Message, MessageRepository, IMessageRepository>();
        e.RegisterRepository<User, UserRepository, IUserRepository>();
        
        // required for UnitOfWork implementation
        e.Injectable<IDbConnection>();
    }
    
    #endregion
}