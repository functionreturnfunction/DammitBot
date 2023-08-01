namespace DammitBot.Configuration
{
    public interface IIrcConfigurationManager : IConfigurationManager
    {
        #region Abstract Properties

        IIrcConfigurationSection IrcConfigurationSection { get; }

        #endregion
    }
}