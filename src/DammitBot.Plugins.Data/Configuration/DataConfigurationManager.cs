using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration
{
    public class DataConfigurationManager : ConfigurationManager, IDataConfigurationManager
    {
        public DataConfigurationManager(IConfigurationBuilder builder) : base(builder) {}

        #region Properties

        public string ConnectionString => Configuration["Dapper:connection"];

        #endregion
    }
}
