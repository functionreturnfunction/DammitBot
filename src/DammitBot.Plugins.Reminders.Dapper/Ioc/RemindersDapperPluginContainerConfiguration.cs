using DammitBot.Abstract;
using DammitBot.Data.Models;
using DammitBot.Data.Dapper.Repositories;
using DammitBot.Library;
using StructureMap;

namespace DammitBot.Ioc
{
    public class RemindersDapperPluginContainerConfiguration : ContainerConfigurationBase
    {
        public override void Configure(ConfigurationExpression e)
        {
            e.For<IRepository<Reminder>>().Use<ReminderRepository>();
        }
    }
}