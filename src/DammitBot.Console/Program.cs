using System;
using DammitBot.Ioc;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DammitBot.Console;

class Program
{
    private static bool LogAndRethrow(ILogger log, Exception e)
    {
        log.LogError("Runtime error occurred.", e);
        return false;
    }

    public static IHostBuilder CreateHostBuilder()
    {
        return Host
            .CreateDefaultBuilder()
            .UseLamar(serviceRegistry =>
                new DammitBotContainerConfiguration().Configure(serviceRegistry));

    }

    static void Main(string[] args)
    {
        using var host = CreateHostBuilder().Build();
        using var bot = host.Services.GetRequiredService<IBot>();
        var log = host.Services.GetRequiredService<ILogger<Program>>();

        try
        {
            bot.Run();
            System.Console.Write("Message for bot: ");

            do
            {
                if (!System.Console.KeyAvailable)
                {
                    continue;
                }
                
                // TODO: cancel console input after x amount of time so we don't block on accidental keypress

                var message = System.Console.ReadLine() ?? string.Empty;
                bot.ReceiveConsoleMessage(message);

                if (bot.Running)
                {
                    System.Console.Write("Message for bot: ");
                }
            } while (bot.Running);
        }
        catch (Exception e) when (LogAndRethrow(log, e))
        {
            throw;
        }
    }
}