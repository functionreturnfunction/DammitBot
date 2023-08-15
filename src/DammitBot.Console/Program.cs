using DammitBot.Abstract;
using DammitBot.IoC;
using DammitBot.Utilities;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DammitBot.Console;

class Program
{
    public static IHostBuilder CreateHostBuilder()
    {
        return Host
            .CreateDefaultBuilder()
            .UseLamar(serviceRegistry =>
            {
                serviceRegistry.For<IMainLoop>().Use<MainLoop>();
                new DammitBotContainerConfiguration().Configure(serviceRegistry);
            });
    }

    static void Main(string[] args)
    {
        using var host = CreateHostBuilder().Build();
        using var loop = host.Services.GetRequiredService<IMainLoop>();
        
        loop.Run();
    }
}