using DammitBot.Abstract;
using DammitBot.Library;
using DammitBot.Wrappers;
using Lamar;

namespace DammitBot.Ioc;

public class IrcProtocolContainerConfiguration : ContainerConfigurationBase
{
    public override void Configure(ServiceRegistry e)
    {
        e.For<IIrcClient>().Use<IrcClientWrapper>();
    }
}