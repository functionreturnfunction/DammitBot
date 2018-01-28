using System;
using System.Data;
using System.IO;
using System.Linq;
using DammitBot.Abstract;
using DammitBot.Data.Models;
using DammitBot.Data.Migrations.Library;
using Dommel;

namespace DammitBot
{
    public class AutoMigrationsPlugin : IPlugin
    {
        protected readonly MigrationRunner _runner;

        public AutoMigrationsPlugin(MigrationRunner runner)
        {
            _runner = runner;
        }

        public void Initialize()
        {
            _runner.Up();
        }

        public void Cleanup() {}
    }
}
