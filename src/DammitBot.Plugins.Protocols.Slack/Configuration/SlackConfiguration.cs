using System.ComponentModel.DataAnnotations;

namespace DammitBot.Configuration;

/// <summary>
/// Configuration section containing values for connecting to Slack.
/// </summary>
public class SlackConfiguration
{
    /// <summary>
    /// App-Level Tokens start with "xapp-", and can be found in the api.slack.com page for your app under
    /// the "Basic Information" tab, down past "App Credentials".
    /// </summary>
    [Required]
    public string AppLevelToken { get; init; }
    /// <summary>
    /// Api Tokens also seem to be called "Bot Tokens", and can be found in the api.slack.com page for
    /// your app under the "Install App" tab, called "Bot User OAuth Token".
    /// </summary>
    [Required]
    public string ApiToken { get; init; }
}