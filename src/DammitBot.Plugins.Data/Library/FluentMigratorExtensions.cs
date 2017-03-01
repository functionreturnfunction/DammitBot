using FluentMigrator.Builders.Create.Table;

namespace DammitBot.Data.Library
{
    public static class FluentMigratorExtensions
    {
        public static ICreateTableWithColumnOrSchemaOrDescriptionSyntax WithIdentityColumn(this ICreateTableWithColumnOrSchemaOrDescriptionSyntax that)
        {
            return that.WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable();
        }
    }
}