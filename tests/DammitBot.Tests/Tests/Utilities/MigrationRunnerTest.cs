using DammitBot.Data.Migrations.Library;
using DammitBot.Library;
using Xunit;

namespace DammitBot.Tests.Utilities
{
    public class MigrationRunnerTest : InMemoryDatabaseUnitTestBase<MigrationRunner>
    {
        public const int EXPECTED_VERSION = 2;

        protected override void RunMigrations() { }

        [Fact]
        public void TestLatestVersionNumberReturnsLatestVersionNumber()
        {
            Assert.Null(_target.GetLatestVersionNumber());

            for (var i = 1; i < EXPECTED_VERSION; ++i)
            {
                _target.Up(i);

                Assert.Equal(i, _target.GetLatestVersionNumber().Value);
            }
        }

        [Fact]
        public void TestEnsureUpToDateMigratesUpToLatestVersionNumber()
        {
            _target.Up();

            Assert.Equal(EXPECTED_VERSION, _target.GetLatestVersionNumber().Value);
        }
    }
}
