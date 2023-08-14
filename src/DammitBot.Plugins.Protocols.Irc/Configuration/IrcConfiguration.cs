using System.ComponentModel.DataAnnotations;

namespace DammitBot.Configuration;

/// <summary>
/// Configuration section containing values for connecting to an Irc <see cref="Server"/> and its
/// <see cref="Channels"/>. 
/// </summary>
public class IrcConfiguration
{
    /// <summary>
    /// Hostname of the server to connect to.
    /// </summary>
    [Required]
    public required string Server { get; set; }
    /// <summary>
    /// Nickname to use when connecting.
    /// </summary>
    [Required]
    public required string Nick { get; set; }
    /// <summary>
    /// User name to use when connecting.
    /// </summary>
    [Required]
    public required string User { get; set; }
    /// <summary>
    /// Channels to join after successfully connecting.
    /// </summary>
    [Required]
    public required string[] Channels { get; set; }
}