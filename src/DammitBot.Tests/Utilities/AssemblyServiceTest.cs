﻿using System.Linq;
using DammitBot.TestLibrary;
using Xunit;

namespace DammitBot.Utilities
{

    public class AssemblyServiceTest : UnitTestBase<AssemblyService>
    {
        [Fact]
        public void TestAllAssembliesReturnsCoreDllAndPluginDlls()
        {
            var expected = new[] {
                "DammitBot.Core",
                "DammitBot.Plugins.Commands",
                "DammitBot.Plugins.Commands.Die",
                "DammitBot.Plugins.Commands.Help",
                "DammitBot.Plugins.Data",
                "DammitBot.Plugins.Data.AutoMigrations",
                "DammitBot.Plugins.Data.Migrations",
                "DammitBot.Plugins.Data.NHibernate",
                "DammitBot.Plugins.MessageLogging",
                "DammitBot.Plugins.Protocols.Irc",
                "DammitBot.Plugins.Reminders",
                "DammitBot.Plugins.Reminders.Migrations",
                "DammitBot.Plugins.Scheduling",
                "DammitBot.Plugins.TeamCity"
            };

            var results = _target.GetAllAssemblies();

            foreach (var assembly in results)
            {
                Assert.Contains(assembly.GetName().Name, expected);
            }

            Assert.Equal(expected.Length, results.Count());
        }
    }
}
