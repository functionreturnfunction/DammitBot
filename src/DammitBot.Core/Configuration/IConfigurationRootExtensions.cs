using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration;

public static class IConfigurationRootExtensions
{
    public static IConfigurationSection EnsureConfigSection(this IConfigurationRoot root, string section)
    {
        return root.GetSection(section) ??
               throw new ConfigurationErrorsException(
                   $"Required configuration section '{section}' is missing");
    }
}