using DammitBot.Data.Library;
using FluentMigrator;

namespace DammitBot.Data.Migrations
{
    [Migration(20170228231214389)]
    public class CreateDatabase : Migration
    {
        public struct StringLengths
        {
            public struct Users
            {
                public const int USERNAME = 30;
            }

            public struct Nicks
            {
                public const int NICKNAME = 255;
            }

            public struct Messages
            {
                public const int TEXT = 512, PROTOCOL = 15, CHANNEL = 32;
            }
        }

        public override void Up()
        {
            Create.Table("Users")
                .WithIdentityColumn()
                .WithColumn("Username").AsString(StringLengths.Users.USERNAME).NotNullable().Unique()
                .WithTimestamps();

            Create.Table("Nicks")
                .WithIdentityColumn()
                .WithColumn("Nickname").AsString(StringLengths.Nicks.NICKNAME).NotNullable().Unique()
                .WithForeignKeyColumn("UserId", "Users").Nullable()
                .WithTimestamps();

            Create.Table("Messages")
                .WithIdentityColumn()
                .WithColumn("Text").AsString(StringLengths.Messages.TEXT).NotNullable()
                .WithColumn("Protocol").AsString(StringLengths.Messages.PROTOCOL).Nullable()
                .WithColumn("Channel").AsString(StringLengths.Messages.CHANNEL).Nullable()
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