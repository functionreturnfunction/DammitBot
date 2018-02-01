using System.IO;

namespace DammitBot.Configuration
{
    public class SettingsPathHelper : ISettingsPathHelper
    {
        public string SettingsPath => Path.GetFullPath(Path.Combine("appsettings.json"));
    }
}