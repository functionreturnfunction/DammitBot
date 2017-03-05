using FluentMigrator.Builders.Create.Column;
using FluentMigrator.Builders.Create.Table;

namespace DammitBot.Data.Library
{
    public static class FluentMigratorExtensions
    {
        public static string CreateForeignKeyName(string table, string foreignTable, string column)
        {
            return $"FK_{table}_{foreignTable}_{column}";
        }

        public static ICreateTableWithColumnOrSchemaOrDescriptionSyntax WithIdentityColumn(this ICreateTableWithColumnOrSchemaOrDescriptionSyntax that)
        {
            return that.WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable().Unique();
        }

        public static ICreateTableWithColumnOrSchemaOrDescriptionSyntax WithTimestamps(this ICreateTableWithColumnOrSchemaOrDescriptionSyntax that)
        {
            return that
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("UpdatedAt").AsDateTime().Nullable();
        }

        public static ICreateTableColumnOptionOrForeignKeyCascadeOrWithColumnSyntax WithForeignKeyColumn(
            this ICreateTableWithColumnOrSchemaOrDescriptionSyntax that, string columnName, string foreignTable, string foreignId = "Id")
        {
            var builder = that as CreateTableExpressionBuilder;
            return
                that.WithColumn(columnName).AsInt32()
                    .ForeignKey(
                        CreateForeignKeyName(builder.Expression.TableName, foreignTable, columnName),
                        foreignTable, foreignId);
        }
    }
}