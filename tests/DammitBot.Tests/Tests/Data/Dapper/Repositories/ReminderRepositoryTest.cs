using System;
using DammitBot.Data.Dapper.Repositories;
using DammitBot.Data.Models;
using DammitBot.Data.Models.Fakers;
using DammitBot.Library;
using Xunit;

namespace DammitBot.Tests.Data.Dapper.Repositories;

public class ReminderRepositoryTest : DapperRepositoryTestBase<Reminder, ReminderRepository>
{
    protected override Reminder CreateValidEntity()
    {
        return new ReminderFaker().Generate();
    }

    protected override void EnsureReferenceIds(IUnitOfWork uow, Reminder entity)
    {
        entity.FromId = entity.From.Id = Convert.ToInt32(uow.Insert(entity.From));
        entity.ToId = entity.To.Id = Convert.ToInt32(uow.Insert(entity.To));
    }

    protected override void MakeUpdateChange(Reminder entity)
    {
        entity.Text = "totally new text";
    }

    protected override void TestUpdateChange(Reminder entity)
    {
        Assert.Equal("totally new text", entity.Text);
    }
}