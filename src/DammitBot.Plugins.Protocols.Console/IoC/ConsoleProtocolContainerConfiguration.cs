using DammitBot.Abstract;
using DammitBot.Utilities;
using Lamar;

namespace DammitBot.IoC;

public class ConsoleProtocolContainerConfiguration : ContainerConfigurationBase
{
    public override void Configure(ServiceRegistry e)
    {
        e.For<IConsoleMainLoop>().Use<ConsoleMainLoop>().Singleton();
        e.For<IMainLoop>().Use(ctx => ctx.GetInstance<IConsoleMainLoop>());
    }
}