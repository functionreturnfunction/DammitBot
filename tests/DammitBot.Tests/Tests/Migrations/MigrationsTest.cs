using System.Linq;
using DammitBot.Library;
using Dapper;
using Xunit;

namespace DammitBot.Tests.Migrations;

public class MigrationsTest : InMemoryDatabaseUnitTestBase<MigrationRunner>
{
    [Fact]
    public void Test_RunningAllMigrationsDown_LeavesOnlyTheVersionsTable()
    {
        var beforeCount = _connection.QuerySingle<int>(@"
            SELECT count(name)
              FROM sqlite_schema
             WHERE type = 'table'
               AND name NOT LIKE 'sqlite_%'");
        
        Assert.True(beforeCount > 1);
        
        _target.Down();

        var tableNames = _connection.Query<string>(@"
            SELECT name
              FROM sqlite_schema
             WHERE type = 'table'
               AND name NOT LIKE 'sqlite_%'")
            .ToList();

        Assert.Single(tableNames);
        Assert.Equal(MigrationRunner.VERSION_INFO_TABLE, tableNames.Single());
    }
}