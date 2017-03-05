using DammitBot.Data.Library;
using FluentMigrator;

namespace DammitBot.Plugins.Reminders.Migrations
{
    [Migration(20170305130157688)]
    public class CreateRemindersTable : Migration
    {
        public override void Up()
        {
            Create.Table("Reminders")
                .WithIdentityColumn()
                .WithColumn("Text").AsString(512).NotNullable()
                .WithForeignKeyColumn("FromId", "Users").NotNullable()
                .WithForeignKeyColumn("ToId", "Users").NotNullable()
                .WithColumn("RemindAt").AsDateTime().NotNullable()
                .WithColumn("RemindedAt").AsDateTime().Nullable()
                .WithTimestamps();
        }

        public override void Down()
        {
            Delete.Table("Reminders");
        }
    }
}