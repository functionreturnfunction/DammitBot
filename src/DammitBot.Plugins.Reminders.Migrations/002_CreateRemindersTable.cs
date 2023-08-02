using DammitBot.Data.Migrations.Library;
using DammitBot.Library;

namespace DammitBot.Migrations;

public class CreateRemindersTable : MigrationBase
{
    public struct StringLengths
    {
        public const int TEXT = 512;
    }

    public override int Id => 2;

    public override void Up(IUnitOfWork uow)
    {
        uow.ExecuteNonQuery(@"
CREATE TABLE
IF NOT EXISTS Reminders (
    Id integer PRIMARY KEY,
    Text text NOT NULL,
    FromId int NOT NULL,
    ToId int NOT NULL,
    RemindAt text NOT NULL,
    RemindedAt text,
    CreatedAt text NOT NULL,
    UpdatedAt text,
    FOREIGN KEY (FromId) REFERENCES Users (Id),
    FOREIGN KEY (ToId) REFERENCES Users (Id)
);");
    }

    public override void Down(IUnitOfWork uow)
    {
        uow.ExecuteNonQuery("DROP TABLE Reminders;");
    }
}