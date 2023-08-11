using System.ComponentModel.DataAnnotations;

namespace DammitBot.Configuration;

/// <summary>
/// Configuration details specific to dealing with persistence, specifically a
/// <see cref="ConnectionString"/>.
/// </summary>
public class DataConfiguration
{
    /// <summary>
    /// Connection string value configured for use within the application.
    /// </summary>
    [Required]
    public string? ConnectionString { get; set; }
}