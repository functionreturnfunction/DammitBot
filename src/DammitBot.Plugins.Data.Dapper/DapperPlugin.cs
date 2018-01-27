using DammitBot.Abstract;

namespace DammitBot
{
    public class DapperPlugin : IPlugin
    {
        public void Initialize()
        {
            SQLitePCL.Batteries_V2.Init();
        }

        public void Cleanup() {}
    }
}
