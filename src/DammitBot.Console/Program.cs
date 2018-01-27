using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using log4net;

namespace DammitBot.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            new Startup().ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var bot = serviceProvider.GetService<IBot>();
            var log = serviceProvider.GetService<ILog>();

            try
            {
                bot.Run();

                do
                {
                    Thread.Sleep(100);
                } while (bot.Running);
            }
            catch (Exception e)
            {
                log.Error("Runtime error encountered.", e);
                throw;
            }
            finally
            {
                bot.Dispose();
            }
        }
    }
}
