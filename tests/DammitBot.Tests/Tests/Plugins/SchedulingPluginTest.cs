using System;
using System.Threading;
using DammitBot.IoC;
using DammitBot.Library;
using DammitBot.Metadata;
using DammitBot.Plugins;
using Lamar;
using Moq;
using Moq.Sequences;
using Quartz;
using Quartz.Spi;
using Xunit;

namespace DammitBot.Tests.Plugins;

public class SchedulingPluginTest : UnitTestBase<SchedulingPlugin>
{
    #region Private Members
    
    private Mock<IStdSchedulerFactory>? _schedulerFactory;
    private Mock<IScheduler>? _scheduler;
    private Mock<IJobFactory>? _jobFactory;
    private Mock<IJobService>? _jobService;
    
    #endregion
    
    #region Setup/Teardown
    
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
    
    #endregion
    
    #region Initialize() tests

    [Fact]
    public void Test_Initialize_CreatesAndStartsTheScheduler()
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
        _schedulerFactory.VerifyNoOtherCalls();
        _scheduler.VerifyAll();
        _scheduler.VerifyNoOtherCalls();
        _jobService.VerifyAll();
        _jobService.VerifyNoOtherCalls();
    }

    [Fact]
    public void Test_Initialize_SchedulesJobs()
    {
        using var sequence = Sequence.Create();

        _jobService!.Setup(x => x.GetAllJobs()).InSequence().Returns(new[] {
            typeof(TestHourlyJob),
            typeof(TestDailyJob)
        });
        var hourlyJob = _jobService.Setup(x => x.Build(
            typeof(TestHourlyJob),
            nameof(TestHourlyJob),
            "TestHourlyGroup"))
            .InSequence()
            .Mock();
        var hourlyTrigger = _jobService.Setup(x => x.BuildTrigger(
                typeof(TestHourlyJob),
                "TestHourlyTrigger",
                "TestHourlyGroup"))
            .InSequence()
            .Mock();
        _scheduler!.Setup(x => x.ScheduleJob(
            hourlyJob.Object,
            hourlyTrigger.Object,
            default))
            .InSequence();
        var dailyJob = _jobService.Setup(x => x.Build(
                typeof(TestDailyJob),
                nameof(TestDailyJob),
                "TestDailyGroup"))
            .InSequence()
            .Mock();
        var dailyTrigger = _jobService.Setup(x => x.BuildTrigger(
                typeof(TestDailyJob),
                "TestDailyTrigger",
                "TestDailyGroup"))
            .InSequence()
            .Mock();
        _scheduler!.Setup(x => x.ScheduleJob(
            dailyJob.Object,
            dailyTrigger.Object,
            default))
            .InSequence();
        
        _target.Initialize();
        
        _jobService.VerifyAll();
        _scheduler.VerifyAll();
    }
    
    #endregion
    
    #region Nested Classes
    
    [Daily]
    public class TestDailyJob {}
    
    public class TestHourlyJob {}
    
    #endregion
}