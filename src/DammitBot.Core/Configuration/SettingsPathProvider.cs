using System.IO;

namespace DammitBot.Configuration;

/// <inheritdoc cref="ISettingsPathProvider" />
public class SettingsPathProvider : ISettingsPathProvider
{
    #region Constants
    
    /// <summary>
    /// Name of the settings file.
    /// </summary>
    public const string SETTINGS_FILE_NAME = "appsettings.json";
    
    #endregion
    
    #region Properties
    
    /// <inheritdoc cref="ISettingsPathProvider.SettingsPath" />
    public string SettingsPath => Path.GetFullPath(Path.Combine(SETTINGS_FILE_NAME));
    
    #endregion
}