using DammitBot.TestLibrary;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Processors.SQLite;
using Moq;
using StructureMap;
using Xunit;

namespace DammitBot.Utilities
{
    public class MigrationServiceTest : UnitTestBase<MigrationService>
    {
        private Mock<IMigrationRunner> _migrationRunner;

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Inject(out _migrationRunner);
        }

        [Fact]
        public void TestLatestVersionNumberReturnsLatestVersionNumber()
        {
            Assert.Equal(20170305130157688, _target.LatestVersionNumber.Value);
        }

        [Fact]
        public void TestEnsureUpToDateMigratesUpToLatestVersionNumber()
        {
            _target.EnsureUpToDate(new MigrationProcessorOptions<IFlushableLogAnnouncer,SQLiteProcessorFactory>());

            _migrationRunner.Verify(x => x.MigrateUp(_target.LatestVersionNumber.Value));
        }
    }
}
