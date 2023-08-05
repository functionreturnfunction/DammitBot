namespace DammitBot.Configuration;

/// <summary>
/// Configuration details pertaining to the bot itself.
/// </summary>
public interface IBotConfigurationSection
{
    /// <summary>
    /// Name of the bot; it can assume it is being spoken to when found at the beginning of messages.
    /// This value will be used in a <see cref="System.Text.RegularExpressions.Regex"/>, so the value can
    /// contain valid regex syntax.
    /// </summary>
    string GoesBy { get; }
}