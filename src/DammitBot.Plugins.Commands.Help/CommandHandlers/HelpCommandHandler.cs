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
using Microsoft.Extensions.Options;

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
    
    private readonly IAssemblyTypeService _assemblyTypeService;
    private readonly BotConfiguration _config;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for he <see cref="HelpCommandHandler"/> class.
    /// </summary>
    public HelpCommandHandler(
        IBot bot,
        IOptions<BotConfiguration> botConfig,
        IAssemblyTypeService assemblyTypeService)
        : base(bot)
    {
        _config = botConfig.Value;
        _assemblyTypeService = assemblyTypeService;
    }
    
    #endregion
    
    #region Private Methods

    private IEnumerable<HandlesCommandAttribute> GetCommandHandlerAttributes(
        CommandEventArgs commandEventArgs)
    {
        var types =  _assemblyTypeService
            .GetTypesFromAllAssemblies();
        var attributes = types
            .Where(
                t =>
                    !t.IsAbstract && typeof(CommandHandlerBase).IsAssignableFrom(t) &&
                    t.HasAttribute<HandlesCommandAttribute>())
            .OrderBy(x => x.Name)
            .Select(t => t.GetCustomAttribute<HandlesCommandAttribute>()!);

        return commandEventArgs.UserIsAdmin
            ? attributes
            : attributes.Where(a => !a.AdminOnly);
    }

    private string StripBeginningAndEndMarkers(string regex)
    {
        return new Regex("([^\\\\])?(?:\\^|\\$)").Replace(regex, "$1");
    }

    private string BuildHelpMessage(CommandEventArgs commandEventArgs)
    {
        var sb = new StringBuilder("This bot responds to the following commands:" + Environment.NewLine);

        foreach (var attribute in GetCommandHandlerAttributes(commandEventArgs))
        {
            sb.AppendLine(
                $"\t {_config.GoesBy} {StripBeginningAndEndMarkers(attribute.Regex.ToString())} " + 
                $"- {attribute.Description}{(attribute.AdminOnly ? " (admin only)" : null)}");
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
        Bot.ReplyToMessage(e, BuildHelpMessage(e));
    }

    #endregion
}