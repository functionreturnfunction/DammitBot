using System.IO;
using DammitBot.Configuration;

namespace DammitBot.TestLibrary
{
    public class TestSettingsPathHelper : ISettingsPathHelper
    {
        public string SettingsPath => Path.GetFullPath(Path.Combine("..", "..", "..", "appsettings.json"));
    }
}