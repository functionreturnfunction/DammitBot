﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.Metadata;
using DammitBot.Utilities;

namespace DammitBot.CommandHandlers;

/// <summary>
/// <see cref="ICommandHandler"/> implementation which provides the "help" command, which gives users
/// usage information about the available commands.
/// </summary>
[HandlesCommand(
    "^help( .+)?$",
    "Get usage information for the available bot commands.")]
public class HelpCommandHandler : CommandHandlerBase
{
    #region Private Members
    
    private readonly IAssemblyService _assemblyService;
    private readonly IBotConfigurationSection _config;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for he <see cref="HelpCommandHandler"/> class.
    /// </summary>
    public HelpCommandHandler(
        IBot bot,
        IConfigurationProvider configurationProvider,
        IAssemblyService assemblyService)
        : base(bot)
    {
        _config = configurationProvider.BotConfig;
        _assemblyService = assemblyService;
    }
    
    #endregion
    
    #region Private Methods

    private IEnumerable<HandlesCommandAttribute> GetCommandHandlerAttributes()
    {
        var assemblies =  _assemblyService
            .GetAllAssemblies();
        var types = assemblies.GetTypes();
        return types
            .Where(
                t =>
                    !t.IsAbstract && typeof(CommandHandlerBase).IsAssignableFrom(t) &&
                    t.HasAttribute<HandlesCommandAttribute>())
            .Select(t => t.GetCustomAttribute<HandlesCommandAttribute>()!);
    }

    private string StripBeginningAndEndMarkers(string regex)
    {
        return new Regex("([^\\\\])?(?:\\^|\\$)").Replace(regex, "$1");
    }

    private string BuildHelpMessage()
    {
        var sb = new StringBuilder("This bot responds to the following commands:" + Environment.NewLine);

        foreach (var attribute in GetCommandHandlerAttributes())
        {
            sb.AppendLine(
                $"\t {_config.GoesBy} {StripBeginningAndEndMarkers(attribute.Regex.ToString())} " + 
                $"- {attribute.Description}");
        }

        return sb.ToString();
    }
    
    #endregion

    #region Exposed Methods

    /// <summary>
    /// Respond with usage information about the various available commands.
    /// </summary>
    public override void Handle(CommandEventArgs e)
    {
        Bot.ReplyToMessage(e, BuildHelpMessage());
    }

    #endregion
}