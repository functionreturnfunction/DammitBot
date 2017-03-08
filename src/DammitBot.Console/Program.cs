using System;
using System.Threading;
using DammitBot.Ioc;
using log4net;

namespace DammitBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = DependencyRegistrar.GetContainer();
            var bot = container.GetInstance<IBot>();
            var log = container.GetInstance<ILog>();

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
                container.Dispose();
            }
        }
    }
}
