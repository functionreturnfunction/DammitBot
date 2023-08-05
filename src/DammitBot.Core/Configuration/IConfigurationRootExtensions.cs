using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace DammitBot.Configuration;

/// <summary>
/// Extensions to the <see cref="IConfigurationRoot"/> interface.
/// </summary>
public static class IConfigurationRootExtensions
{
    /// <summary>
    /// Get the <see cref="IConfigurationSection"/> <paramref name="section"/> from the
    /// <see cref="IConfigurationRoot"/>.
    /// </summary>
    /// <exception cref="ConfigurationErrorsException">
    /// Thrown when the specified <paramref name="section"/> is not found.
    /// </exception>
    public static IConfigurationSection EnsureConfigSection(this IConfigurationRoot root, string section)
    {
        return root.GetSection(section) ??
               throw new ConfigurationErrorsException(
                   $"Required configuration section '{section}' is missing");
    }
}