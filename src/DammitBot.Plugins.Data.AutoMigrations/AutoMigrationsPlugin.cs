using DammitBot.Abstract;
using DammitBot.Utilities;

namespace DammitBot
{
    public class AutoMigrationsPlugin : IPlugin
    {
        #region Private Members

        private readonly IMigrationService _migrationService;

        #endregion

        #region Constructors

        public AutoMigrationsPlugin(IMigrationService migrationService)
        {
            _migrationService = migrationService;
        }

        #endregion

        #region Exposed Methods

        public void Initialize()
        {
            _migrationService.EnsureUpToDate(null);
        }

        public void Cleanup() {}

        #endregion
    }
}
