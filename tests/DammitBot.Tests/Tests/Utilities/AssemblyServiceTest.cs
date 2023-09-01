using System.Linq;
using DammitBot.Library;
using DammitBot.Utilities;
using Xunit;

namespace DammitBot.Tests.Utilities;

public class AssemblyServiceTest : UnitTestBase<AssemblyTypeService>
{
    [Fact]
    public void Test_AllAssemblies_ReturnsCoreDllAndPluginDlls()
    {
        var expected = new[] {
            "DammitBot.Core",
            "DammitBot.Plugins.Commands",
            "DammitBot.Plugins.Commands.Die",
            "DammitBot.Plugins.Commands.Help",
            "DammitBot.Plugins.Data",
            "DammitBot.Plugins.Data.AutoMigrations",
            "DammitBot.Plugins.Data.Dapper",
            "DammitBot.Plugins.Data.Migrations",
            "DammitBot.Plugins.Data.SQLite",
            "DammitBot.Plugins.MessageLogging",
            "DammitBot.Plugins.Protocols.Console",
            "DammitBot.Plugins.Protocols.Irc",
            "DammitBot.Plugins.Protocols.Irc.IrcDotNet",
            "DammitBot.Plugins.Reminders",
            "DammitBot.Plugins.Reminders.Dapper",
            "DammitBot.Plugins.Reminders.Migrations",
            "DammitBot.Plugins.Scheduling",
            "DammitBot.Plugins.Protocols.Slack",
            "DammitBot.Plugins.Protocols.Slack.SlackNet",
        };

        var types = _target.GetTypesFromAllAssemblies();
        var assemblies = types.Select(x => x.Assembly).Distinct();

        foreach (var assembly in assemblies)
        {
            Assert.Contains(assembly.GetName().Name, expected);
        }

        Assert.Equal(expected.Length, assemblies.Count());
    }
}