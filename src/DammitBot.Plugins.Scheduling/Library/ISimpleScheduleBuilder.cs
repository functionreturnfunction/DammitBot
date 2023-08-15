using Quartz;

namespace DammitBot.Library;

/// <inheritdoc cref="SimpleScheduleBuilder"/>
public interface ISimpleScheduleBuilder
{
    /// <inheritdoc cref="SimpleScheduleBuilder.WithIntervalInHours" />
    ISimpleScheduleBuilder WithIntervalInHours(int interval);
    /// <inheritdoc cref="SimpleScheduleBuilder.WithIntervalInSeconds" />
    ISimpleScheduleBuilder WithIntervalInSeconds(int interval);
    /// <inheritdoc cref="SimpleScheduleBuilder.WithIntervalInMinutes" />
    ISimpleScheduleBuilder WithIntervalInMinutes(int interval);
    /// <inheritdoc cref="SimpleScheduleBuilder.RepeatForever" />
    ISimpleScheduleBuilder RepeatForever();
}