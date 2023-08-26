using System.ComponentModel.DataAnnotations;

namespace DammitBot.Configuration;

/// <summary>
/// Configuration details pertaining to the bot itself.
/// </summary>
public class BotConfiguration
{
    /// <summary>
    /// Name of the bot; it can assume it is being spoken to when found at the beginning of messages.
    /// This value will be used in a <see cref="System.Text.RegularExpressions.Regex"/>, so the value can
    /// contain valid regex syntax.
    /// </summary>
    [Required]
    public required string? GoesBy { get; set; }
    /// <summary>
    /// Optional array of strings which will be used as globs to ignore plugin assemblies.  Be careful
    /// with this value, as it's very possible to end up ignoring plugin assemblies which are required by
    /// other plugin assemblies which haven't been ignored.
    /// </summary>
    public string[]? IgnoreAssemblies { get; set; }
}