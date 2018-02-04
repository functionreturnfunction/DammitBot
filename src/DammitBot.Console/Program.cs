using System;
using System.Threading;
using DammitBot.Ioc;
using log4net;

namespace DammitBot.Console
{
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
                    Thread.Sleep(100);
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
}
