using DammitBot.Abstract;
using DammitBot.Data.Library;
using DammitBot.Data.Models;
using DammitBot.Data.Dapper.Repositories;
using StructureMap;

namespace DammitBot.Ioc
{
    public class RemindersDapperPluginContainerConfiguration : PluginContainerConfigurationBase
    {
        public override void Configure(ConfigurationExpression e)
        {
            e.For<IRepository<Reminder>>().Use<ReminderRepository>();
        }
    }
}