using System;
using System.Threading.Tasks;
using DammitBot.Data.Models;
using DammitBot.Data.Models.Fakers;
using DammitBot.Jobs;
using DammitBot.Library;
using Lamar;
using Moq;
using Quartz;
using Xunit;

namespace DammitBot.Tests.Jobs;

public class ReminderJobTest : InMemoryDatabaseUnitTestBase<ReminderJob>
{
    #region Private Members
    
    private Reminder? _testReminder;
    private Mock<IBot>? _bot;
    
    #endregion
    
    #region Setup/Teardown

    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);

        _bot = serviceRegistry.For<IBot>().Mock();
    }

    protected override void ExtraSetup()
    {
        base.ExtraSetup();
        
        WithUnitOfWork(uow =>
        {
            var user = new UserFaker().Generate();

            user.Id = Convert.ToInt32(uow.Insert(user));

            _testReminder = new Reminder {
                From = user,
                To = user,
                RemindAt = _now.AddHours(-1),
                Text = "blah"
            };

            _testReminder.Id = Convert.ToInt32(uow.Insert(_testReminder));

            uow.Commit();
        });
    }
    
    #endregion

    [Fact]
    public async Task Test_Execute_SendsPendingReminders()
    {
        await _target.Execute(new Mock<IJobExecutionContext>().Object);
        
        _bot!.Verify(x => x.SayToAll(_testReminder!.Text));
    }

    [Fact]
    public async Task Test_Execute_MarksSentRemindersAsReminded()
    {
        await _target.Execute(new Mock<IJobExecutionContext>().Object);

        WithUnitOfWork(uow =>
        {
            var reloaded = uow.Find<Reminder>(_testReminder!.Id)!;
            
            Assert.Equal(_now, reloaded.RemindedAt);
        });
    }

    [Fact]
    public async Task Test_Execute_DoesNotReRemindRemindedReminders()
    {
        WithUnitOfWork(uow => {
            _testReminder!.RemindedAt = _now;
            
            uow.Update(_testReminder);

            uow.Commit();
        });

        await _target.Execute(new Mock<IJobExecutionContext>().Object);
        
        _bot!.Verify(x => x.SayToAll(_testReminder!.Text), Times.Never);
    }
}