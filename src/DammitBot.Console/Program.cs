using System;
using DammitBot.Ioc;
using log4net;

namespace DammitBot.Console;

class Program
{
    private static bool LogAndRethrow(ILog log, Exception e)
    {
        log.Error("Runtime error occurred.", e);
        return false;
    }

    static void Main(string[] args)
    {
        var container = DammitBotContainerConfiguration.GetContainer();
        var bot = container.GetInstance<IBot>();
        var log = container.GetInstance<ILog>();

        try
        {
            bot.Run();

            do
            {
                System.Console.Write("Message for bot: ");
                var message = System.Console.ReadLine() ?? string.Empty;
                bot.ReceiveConsoleMessage(message);
            } while (bot.Running);
        }
        catch (Exception e) when (LogAndRethrow(log, e))
        {
            throw;
        }
        finally
        {
            bot.Dispose();
            container.Dispose();
        }
    }
}