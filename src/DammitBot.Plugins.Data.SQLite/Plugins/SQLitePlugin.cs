namespace DammitBot.Plugins;

public class SQLitePlugin : ISQLitePlugin
{
    public void Initialize()
    {
        SQLitePCL.Batteries_V2.Init();
    }

    public void Cleanup() {}
}