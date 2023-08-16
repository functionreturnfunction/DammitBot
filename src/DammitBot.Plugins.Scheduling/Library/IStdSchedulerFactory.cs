using Quartz;
using Quartz.Impl;

namespace DammitBot.Library;

/// <inheritdoc cref="StdSchedulerFactory"/>
public interface IStdSchedulerFactory : ISchedulerFactory
{
    /// <inheritdoc cref="StdSchedulerFactory.Initialize()"/>
    void Initialize();
}