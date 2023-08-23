using System;
using System.Threading.Tasks;
using DammitBot.IoC;
using DammitBot.Library;
using DammitBot.Metadata;
using DammitBot.Plugins;
using DammitBot.Utilities;
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
    private Mock<IAssemblyTypeService>? _assemblyTypeService;
    
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
        _assemblyTypeService = serviceRegistry.For<IAssemblyTypeService>().Mock();
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
        _assemblyTypeService!
            .Setup(x => x.GetTypesFromAllAssemblies())
            .InSequence()
            .Returns(Type.EmptyTypes);
        
        _target.Initialize();
        
        _schedulerFactory.VerifyAll();
        _schedulerFactory.VerifyNoOtherCalls();
        _scheduler.VerifyAll();
        _scheduler.VerifyNoOtherCalls();
        _assemblyTypeService.VerifyAll();
        _assemblyTypeService.VerifyNoOtherCalls();
    }

    [Fact]
    public void Test_Initialize_SchedulesJobs()
    {
        using var sequence = Sequence.Create();

        _assemblyTypeService!
            .Setup(x => x.GetTypesFromAllAssemblies())
            .InSequence()
            .Returns(new[] {
                typeof(TestHourlyJob),
                typeof(TestDailyJob)
            });

        _scheduler!.Setup(x => x.ScheduleJob(
                It.Is<IJobDetail>(j => j.JobType == typeof(TestHourlyJob)),
                It.Is<ITrigger>(t => t.Key.Name == "TestHourlyTrigger"),
                default))
            .InSequence();
        _scheduler!.Setup(x => x.ScheduleJob(
                It.Is<IJobDetail>(j => j.JobType == typeof(TestDailyJob)),
                It.Is<ITrigger>(t => t.Key.Name == "TestDailyTrigger"),
                default))
            .InSequence();
        
        _target.Initialize();
        
        _scheduler.VerifyAll();
    }
    
    #endregion
    
    #region Nested Classes
    
    [Daily]
    public class TestDailyJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
    
    [Hourly]
    public class TestHourlyJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
    
    #endregion
}