using DammitBot.Abstract;
using DammitBot.IoC;
using DammitBot.Utilities;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host
    .CreateDefaultBuilder()
    .UseLamar(serviceRegistry =>
    {
        serviceRegistry.For<IMainLoop>().Use<MainLoop>();
        new DammitBotContainerConfiguration().Configure(serviceRegistry);
    })
    .Build();
using var loop = host.Services.GetRequiredService<IMainLoop>();
        
loop.Run();
