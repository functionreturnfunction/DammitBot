using System;
using DammitBot.Abstract;
using DammitBot.Data.Library;
using DammitBot.Data.Models;
using DammitBot.Data.Repositories;
using StructureMap;

namespace DammitBot.Ioc
{
    public class ReminderPluginContainerConfiguration : PluginContainerConfigurationBase
    {
        public override void Configure(ConfigurationExpression e)
        {
            e.For<IRepository<Reminder>>().Use<ReminderRepository>();
        }
    }
}