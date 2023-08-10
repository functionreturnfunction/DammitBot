using DammitBot.Data.Models;
using DammitBot.Library;

namespace DammitBot.Data.Migrations;

/// <summary>
/// Migration to create the <see cref="Reminder"/>s table.
/// </summary>
public class CreateRemindersTable : MigrationBase
{
    #region Constants
    
    private struct StringLengths
    {
        public const int TEXT = 512;
    }
    
    #endregion
    
    #region Properties

    /// <inheritdoc />
    public override int VersionNumber => 2;
    
    #endregion

    /// <inheritdoc />
    /// <remarks>
    /// This implementation will create the <see cref="Reminder"/>s table.
    /// </remarks>
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

    /// <inheritdoc />
    /// <remarks>
    /// This implementation will drop the <see cref="Reminder"/>s table.
    /// </remarks>
    public override void Down(IUnitOfWork uow)
    {
        uow.ExecuteNonQuery("DROP TABLE Reminders;");
    }
}