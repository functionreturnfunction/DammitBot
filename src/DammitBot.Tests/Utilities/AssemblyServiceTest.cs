using System.Linq;
using DammitBot.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DammitBot.Utilities
{
    [TestClass]
    public class AssemblyServiceTest : UnitTestBase<AssemblyService>
    {
        [TestMethod]
        public void TestAllAssembliesReturnsCoreDllAndPluginDlls()
        {
            var expected = new[] {
                "DammitBot.Core",
                "DammitBot.Plugins.Commands",
                "DammitBot.Plugins.Commands.Die",
                "DammitBot.Plugins.Commands.Help",
                "DammitBot.Plugins.Data",
                "DammitBot.Plugins.MessageLogging",
                "DammitBot.Plugins.Scheduling",
                "DammitBot.Plugins.Scheduling.TeamCity"
            };

            var results = _target.GetAllAssemblies();

            foreach (var assembly in results)
            {
                MyAssert.Contains(expected, assembly.GetName().Name);
            }

            Assert.AreEqual(expected.Length, results.Count(), "Did not find the correct number of assemblies.");
        }
    }
}
