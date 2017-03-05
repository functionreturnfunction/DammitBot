using DammitBot.Data.Library;
using FluentMigrator;

namespace DammitBot.Data.Migrations
{
    [Migration(20170228231214389)]
    public class CreateDatabase : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithIdentityColumn()
                .WithColumn("Username").AsString(30).NotNullable().Unique()
                .WithTimestamps();

            Create.Table("Nicks")
                .WithIdentityColumn()
                .WithColumn("Nickname").AsString(255).NotNullable().Unique()
                .WithForeignKeyColumn("UserId", "Users").Nullable()
                .WithTimestamps();

            Create.Table("Messages")
                .WithIdentityColumn()
                .WithColumn("Text").AsString(512).NotNullable()
                .WithColumn("Protocol").AsString(15).Nullable()
                .WithColumn("Channel").AsString(32).Nullable()
                .WithForeignKeyColumn("FromId", "Nicks").NotNullable()
                .WithTimestamps();
        }

        public override void Down()
        {
            Delete.Table("Messages");
            Delete.Table("Nicks");
            Delete.Table("Users");
        }
    }
}