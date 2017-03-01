// ReSharper disable once CheckNamespace
namespace DammitBot.Configuration
{
    public class DataConfigurationManager : ConfigurationManager, IDataConfigurationManager
    {
        #region Properties

        public string ConnectionString => System.Configuration.ConfigurationManager.ConnectionStrings["MAIN"].ToString();

        #endregion
    }
}
