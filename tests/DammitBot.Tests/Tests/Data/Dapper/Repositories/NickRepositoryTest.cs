using DammitBot.Data.Dapper.Repositories;
using DammitBot.Data.Models;
using DammitBot.Data.Models.Fakers;
using DammitBot.Library;
using Xunit;

namespace DammitBot.Tests.Data.Dapper.Repositories;

public class UserRepositoryTest : DapperRepositoryTestBase<User, UserRepository>
{
    protected override User CreateValidEntity()
    {
        return new UserFaker().Generate();
    }

    protected override void EnsureReferenceIds(IUnitOfWork uow, User entity) {}

    protected override void MakeUpdateChange(User entity)
    {
        entity.Username = "totally new username";
    }

    protected override void TestUpdateChange(User entity)
    {
        Assert.Equal("totally new username", entity.Username);
    }
}

public class NickRepositoryTest : DapperRepositoryTestBase<Nick, NickRepository>
{
    protected override Nick CreateValidEntity()
    {
        return new NickFaker().Generate();
    }

    protected override void EnsureReferenceIds(IUnitOfWork uow, Nick entity) {}

    protected override void MakeUpdateChange(Nick entity)
    {
        entity.Nickname = "totally new nickname";
    }

    protected override void TestUpdateChange(Nick entity)
    {
        Assert.Equal("totally new nickname", entity.Nickname);
    }
}