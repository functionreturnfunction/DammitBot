using System;
using System.Threading;
using DammitBot.IoC;
using DammitBot.Library;
using DammitBot.Plugins;
using Lamar;
using Moq;
using Moq.Sequences;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Xunit;

namespace DammitBot.Tests.Plugins;

public class SchedulingPluginTest : UnitTestBase<SchedulingPlugin>
{
    private Mock<IStdSchedulerFactory>? _schedulerFactory;
    private Mock<IScheduler>? _scheduler;
    private Mock<IJobFactory>? _jobFactory;
    private Mock<IJobService>? _jobService;
    
    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);
        
        new SchedulingPluginContainerConfiguration().Configure(serviceRegistry);

        _schedulerFactory = serviceRegistry.For<IStdSchedulerFactory>().Mock();
        _scheduler = _schedulerFactory
            .Setup(x => x.GetScheduler(default))
            .MockAsync();
        _jobFactory = serviceRegistry.For<IJobFactory>().Mock();
        _jobService = serviceRegistry.For<IJobService>().Mock();
    }

    [Fact]
    public void Test_InitializesThingsWithJobsAndWhatever()
    {
        using var sequence = Sequence.Create();

        _schedulerFactory!.Setup(x => x.Initialize()).InSequence();
        _scheduler!
            .SetupSet(x => x.JobFactory = _jobFactory!.Object).InSequence();
        _scheduler!.Setup(x => x.Start(default)).InSequence();
        _jobService!
            .Setup(x => x.GetAllJobs()).InSequence().Returns(Type.EmptyTypes);
        
        _target.Initialize();
        
        _schedulerFactory.VerifyAll();
        _scheduler.VerifyAll();
    }
}