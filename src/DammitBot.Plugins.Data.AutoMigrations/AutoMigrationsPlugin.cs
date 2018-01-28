using System;
using System.Data;
using System.IO;
using System.Linq;
using DammitBot.Abstract;
using DammitBot.Data.Models;
using Dommel;

namespace DammitBot.Plugins.Data.AutoMigrations
{
    public class AutoMigrationsPlugin : IPlugin
    {
        protected readonly IDbConnection _connection;

        public AutoMigrationsPlugin(IDbConnection connection)
        {
            _connection = connection;
        }

        private void WithOpenConnection(Action<IDbConnection> fn)
        {
            try
            {
                _connection.Open();
                fn(_connection);
                _connection.Close();
            }
            catch (Exception e)
            {
                throw new Exception($"Connection with connection string '{_connection.ConnectionString}' threw exception; Current directory is '{Directory.GetCurrentDirectory()}'", e);
            }
        }

        public void Initialize()
        {
            WithOpenConnection(conn => {
                var cmd = conn.CreateCommand();

                cmd.CommandText = @"
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
";
                cmd.ExecuteNonQuery();

                if (!conn.GetAll<User>().Any(u => u.Username == "jason"))
                {
                    var jason = new User {Username = "jason", CreatedAt = DateTime.Now};
                    jason.Id = Convert.ToInt32(conn.Insert(jason));
                    conn.Insert(new Nick {Nickname = "gentooflux", UserId = jason.Id, CreatedAt = DateTime.Now});
                    conn.Insert(new Nick {Nickname = "gentooflux1", UserId = jason.Id, CreatedAt = DateTime.Now});
                }
            });
        }

        public void Cleanup() {}
    }
}
