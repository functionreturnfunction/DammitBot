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
                .WithColumn("Username").AsString(30).NotNullable()
                .WithTimestamps();

            Create.Table("Nicks")
                .WithIdentityColumn()
                .WithColumn("Nickname").AsString(255).NotNullable()
                .WithColumn("UserId").AsInt32().ForeignKey("FK_Nicks_Users_UserId", "Users", "Id").Nullable()
                .WithTimestamps();

            Create.Table("Messages")
                .WithIdentityColumn()
                .WithColumn("Text").AsString(512).NotNullable()
                .WithColumn("FromId").AsInt32().ForeignKey("FK_Messages_Nicks_FromId", "Nicks", "Id").NotNullable()
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