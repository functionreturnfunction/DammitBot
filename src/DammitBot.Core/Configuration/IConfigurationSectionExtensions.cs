using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration;

public static class IConfigurationSectionExtensions
{
    public static string EnsureConfigValue(this IConfigurationSection config, string key)
    {
        return config[key] ??
               throw new ConfigurationErrorsException(
                   $"Required configuration key '{key}' is missing");
    }
}