using System;
using DammitBot.Data.Dapper.Repositories;
using DammitBot.Data.Models;
using DammitBot.Data.Models.Fakers;
using DammitBot.Library;
using Xunit;

namespace DammitBot.Tests.Data.Dapper.Repositories;

public class MessageRepositoryTest : DapperRepositoryTestBase<Message, MessageRepository>
{
    protected override Message CreateValidEntity()
    {
        return new MessageFaker().Generate();
    }

    protected override void EnsureReferenceIds(IUnitOfWork uow, Message entity)
    {
        entity.FromId = entity.From.Id = Convert.ToInt32(uow.Insert(entity.From));
    }

    protected override void MakeUpdateChange(Message entity)
    {
        entity.Channel = "this is a totally new channel";
    }

    protected override void TestUpdateChange(Message entity)
    {
        Assert.Equal("this is a totally new channel", entity.Channel);
    }
}