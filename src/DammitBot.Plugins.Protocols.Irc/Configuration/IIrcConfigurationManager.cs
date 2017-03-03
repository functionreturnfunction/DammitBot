// ReSharper disable once CheckNamespace
namespace DammitBot.Configuration
{
    public interface IIrcConfigurationManager : IConfigurationManager
    {
        #region Abstract Properties

        IrcConfigurationSection IrcConfigurationSection { get; }

        #endregion
    }
}