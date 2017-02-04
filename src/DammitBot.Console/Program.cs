using System;
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

            try
            {
                bot.Run();
            }
            catch (Exception e)
            {
                container.GetInstance<ILog>().Error("Runtime error encountered.", e);
            }
            finally
            {
                bot.Dispose();
                container.Dispose();
            }
        }
    }
}
