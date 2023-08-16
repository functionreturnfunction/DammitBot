using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using DammitBot.Library;
using Quartz;
using Quartz.Impl;

namespace DammitBot.Wrappers;

/// <inheritdoc/>
[ExcludeFromCodeCoverage]
public class StdSchedulerFactoryWrapper : IStdSchedulerFactory
{
    private readonly StdSchedulerFactory _innerFactory;

    /// <summary>
    /// Constructor for the <see cref="StdSchedulerFactoryWrapper"/> class.
    /// </summary>
    public StdSchedulerFactoryWrapper()
    {
        _innerFactory = new StdSchedulerFactory();
    }
    
    /// <inheritdoc />
    public async Task<IReadOnlyList<IScheduler>> GetAllSchedulers(
        CancellationToken cancellationToken = default)
    {
        return await _innerFactory.GetAllSchedulers(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IScheduler> GetScheduler(CancellationToken cancellationToken = default)
    {
        return await _innerFactory.GetScheduler(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IScheduler?> GetScheduler(
        string schedName,
        CancellationToken cancellationToken = default)
    {
        return await _innerFactory.GetScheduler(schedName, cancellationToken);
    }

    /// <inheritdoc />
    public void Initialize()
    {
        _innerFactory.Initialize();
    }
}