using DammitBot.Abstract;
using DammitBot.Data.Models;
using DammitBot.Data.Dapper.Repositories;
using DammitBot.Data.Repositories;
using DammitBot.Library;
using Lamar;

namespace DammitBot.IoC;

/// <inheritdoc />
/// <remarks>
/// This implementation registers <see cref="Dapper"/>-specific implementations of types from the
/// DammitBot.Plugins.Reminders plugin.
/// </remarks>
public class RemindersDapperPluginContainerConfiguration : ContainerConfigurationBase
{
    #region Exposed Methods
    
    /// <inheritdoc />
    /// <inheritdoc cref="RemindersDapperPluginContainerConfiguration" path="remarks" />
    public override void Configure(ServiceRegistry e)
    {
        e.RegisterRepository<Reminder, ReminderRepository, IReminderRepository>();
    }
    
    #endregion
}