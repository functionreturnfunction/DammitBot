using DammitBot.Data.Migrations.Library;
using DammitBot.Library;
using Xunit;

namespace DammitBot.Tests.Utilities;

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

            var actual = _target.GetLatestVersionNumber();

            Assert.NotNull(actual);
            Assert.Equal(i, actual!.Value);
        }
    }

    [Fact]
    public void TestEnsureUpToDateMigratesUpToLatestVersionNumber()
    {
        _target.Up();

        var actual = _target.GetLatestVersionNumber();

        Assert.NotNull(actual);
        Assert.Equal(EXPECTED_VERSION, actual!.Value);
    }
}