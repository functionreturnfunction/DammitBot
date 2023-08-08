using System;
using System.Text;
using DammitBot.Data.Models;
using DammitBot.Library;

namespace DammitBot.Data.Migrations
{
    public class Initial : MigrationBase
    {
        public override int VersionNumber => 1;

        public override void Up(IUnitOfWork uow)
        {
            uow.ExecuteNonQuery(@"
CREATE TABLE
IF NOT EXISTS Users (
  Id integer PRIMARY KEY,
  Username text NOT NULL,
  CreatedAt text NOT NULL,
  UpdatedAt text
);

CREATE TABLE
IF NOT EXISTS Nicks (
  Id integer PRIMARY KEY,
  Nickname text NOT NULL,
  UserId int,
  CreatedAt text NOT NULL,
  UpdatedAt text,
  FOREIGN KEY (UserId) REFERENCES Users (Id)
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

        public override void Seed(IUnitOfWork uow)
        {
            var jason = new User {
                Username = "jason"
            };
            jason.Id = Convert.ToInt32(uow.Insert<User>(jason));
            uow.Insert<Nick>(new Nick {
                Nickname = "gentooflux",
                User = jason
            });
            uow.Insert<Nick>(new Nick {
                Nickname = "gentooflux1",
                User = jason
            });
        }

        public override void Down(IUnitOfWork uow)
        {
            uow.ExecuteNonQuery(@"
DROP TABLE Messages;
DROP TABLE Nicks;
DROP TABLE Users;");
        }
    }
}