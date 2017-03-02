using System.Security.Cryptography.X509Certificates;
using DammitBot.Configuration;

namespace DammitBot.Protocols.Irc.Configuration
{
    public interface IIrcConfigurationManager : IConfigurationManager
    {
        IrcConfigurationSection IrcConfigurationSection { get; }
    }
}