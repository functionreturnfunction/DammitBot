using System.Data;
using DammitBot.Abstract;
using DammitBot.Data.Library;
using DammitBot.Data.Dapper.Library;
using StructureMap;
using Microsoft.Data.Sqlite;

// ReSharper disable once CheckNamespace
namespace DammitBot.Ioc
{
    public class DapperPluginContainerConfiguration : PluginContainerConfigurationBase
    {
        public override void Configure(ConfigurationExpression e)
        {
            e.For<IUnitOfWork>().Use<UnitOfWork>();
            e.For<IDataCommandHelper>().Use<DataCommandHelper>();
            e.For<IDbConnection>().Use(_ => new SqliteConnection(new SqliteConnectionStringBuilder {DataSource = "db/dev.db"}.ToString()));
        }
    }
}
