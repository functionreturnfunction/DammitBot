using DammitBot.Abstract;
using DammitBot.Data.Models;
using DammitBot.Data.Dapper.Repositories;
using DammitBot.Library;
using Lamar;

namespace DammitBot.Ioc;

public class RemindersDapperPluginContainerConfiguration : ContainerConfigurationBase
{
    public override void Configure(ServiceRegistry e)
    {
        e.For<IRepository<Reminder>>().Use<ReminderRepository>();
    }
}