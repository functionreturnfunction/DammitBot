using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration;

/// <summary>
/// Extensions to the <see cref="IConfigurationSection"/> interface.
/// </summary>
public static class IConfigurationSectionExtensions
{
    /// <summary>
    /// Get the config value with the specified <paramref name="key"/> from the
    /// <see cref="IConfigurationSection"/>.
    /// </summary>
    /// <exception cref="ConfigurationErrorsException">
    /// Thrown when the specified <paramref name="key"/> is not found.
    /// </exception>
    public static string EnsureConfigValue(this IConfigurationSection config, string key)
    {
        return config[key] ??
               throw new ConfigurationErrorsException(
                   $"Required configuration key '{key}' is missing");
    }
}