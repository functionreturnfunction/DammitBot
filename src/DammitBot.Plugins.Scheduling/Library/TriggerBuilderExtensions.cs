using System;
using System.Diagnostics.CodeAnalysis;
using DammitBot.Wrappers;
using Quartz;

namespace DammitBot.Library;

/// <summary>
/// Extensions for the <see cref="TriggerBuilder"/> class.
/// </summary>
[ExcludeFromCodeCoverage]
public static class TriggerBuilderExtensions
{
    /// <inheritdoc cref="SimpleScheduleTriggerBuilderExtensions.WithSimpleSchedule(TriggerBuilder,Action{SimpleScheduleBuilder})"/>
    public static TriggerBuilder WithSimpleSchedule(
        this TriggerBuilder triggerBuilder,
        Action<ISimpleScheduleBuilder> fn)
    {
        return triggerBuilder.WithSimpleSchedule(builder =>
            fn(new SimpleScheduleBuilderWrapper(builder)));
    }
}