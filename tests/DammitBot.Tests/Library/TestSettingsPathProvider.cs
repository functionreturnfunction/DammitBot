using System.IO;
using DammitBot.Configuration;

namespace DammitBot.Library;

public class TestSettingsPathProvider : ISettingsPathProvider
{
    public string SettingsPath =>
        Path.GetFullPath(Path.Combine("..", "..", "..", "appsettings.json"));
}