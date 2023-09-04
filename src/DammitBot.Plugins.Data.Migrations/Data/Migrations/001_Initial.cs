using System;
using System.Text;
using DammitBot.Data.Models;
using DammitBot.Library;

namespace DammitBot.Data.Migrations
{
    /// <summary>
    /// Initial migration to be run by the system.
    /// </summary>
    public class Initial : MigrationBase
    {
        #region Properties
        
        /// <inheritdoc />
        public override int VersionNumber => 1;
        
        #endregion
        
        #region Exposed Methods

        /// <inheritdoc />
        /// <remarks>
        /// This implementation will create the <see cref="User"/>s, <see cref="Nick"/>s, and
        /// <see cref="Message"/>s tables.
        /// </remarks>
        public override void Up(IUnitOfWork uow)
        {
            uow.ExecuteNonQuery(@"
CREATE TABLE
IF NOT EXISTS Users (
  Id integer PRIMARY KEY,
  Username text NOT NULL,
  CreatedAt text NOT NULL,
  IsAdmin bit NOT NULL DEFAULT 0,
  UpdatedAt text,
  UNIQUE (Username)
);

CREATE TABLE
IF NOT EXISTS Nicks (
  Id integer PRIMARY KEY,
  Protocol text NOT NULL,
  Nickname text NOT NULL,
  UserId int,
  CreatedAt text NOT NULL,
  UpdatedAt text,
  FOREIGN KEY (UserId) REFERENCES Users (Id),
  UNIQUE (Protocol, Nickname)
);

CREATE TABLE
IF NOT EXISTS Messages (
  Id integer PRIMARY KEY,
  Text text NOT NULL,
  Protocol text NOT NULL,
  Channel text NOT NULL,
  FromId int NOT NULL,
  CreatedAt text NOT NULL,
  UpdatedAt text,
  FOREIGN KEY (FromId) REFERENCES Nicks (Id)
);
");
        }

        /// <inheritdoc />
        /// <remarks>
        /// This implementation inserts an initial <see cref="User"/> and <see cref="Nick"/>.
        /// </remarks>
        public override void Seed(IUnitOfWork uow)
        {
            var admin = new User {
                Username = "admin",
                IsAdmin = true
            };
            admin.Id = Convert.ToInt32(uow.Insert<User>(admin));
            uow.Insert<Nick>(new Nick {
                Protocol = "Console",
                Nickname = "Console User",
                User = admin
            });
        }

        /// <inheritdoc />
        /// <remarks>
        /// This implementation will drop the <see cref="User"/>s, <see cref="Nick"/>s, and
        /// <see cref="Message"/>s tables.
        /// </remarks>
        public override void Down(IUnitOfWork uow)
        {
            uow.ExecuteNonQuery(@"
DROP TABLE Messages;
DROP TABLE Nicks;
DROP TABLE Users;");
        }
        
        #endregion
    }
}