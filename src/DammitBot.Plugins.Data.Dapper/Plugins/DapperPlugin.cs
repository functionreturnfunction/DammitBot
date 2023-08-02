namespace DammitBot.Plugins;

public class DapperPlugin : IDapperPlugin
{
    public void Initialize()
    {
        SQLitePCL.Batteries_V2.Init();
    }

    public void Cleanup() {}
}