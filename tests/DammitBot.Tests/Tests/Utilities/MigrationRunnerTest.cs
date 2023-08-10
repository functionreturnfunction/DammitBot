using DammitBot.Library;
using Xunit;

namespace DammitBot.Tests.Utilities;

public class MigrationRunnerTest : InMemoryDatabaseUnitTestBase<MigrationRunner>
{
    public const int EXPECTED_VERSION = 2;

    protected override void RunMigrations() { }

    [Fact]
    public void Test_LatestVersionNumber_ReturnsLatestVersionNumber()
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
    public void Test_EnsureUpToDate_MigratesUpToLatestVersionNumber()
    {
        _target.Up();

        var actual = _target.GetLatestVersionNumber();

        Assert.NotNull(actual);
        Assert.Equal(EXPECTED_VERSION, actual!.Value);
    }
}