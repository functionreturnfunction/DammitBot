using DammitBot.Data.Dapper.Repositories;
using DammitBot.Data.Models;
using DammitBot.Data.Models.Fakers;
using DammitBot.Library;
using Xunit;

namespace DammitBot.Tests.Data.Dapper.Repositories;

public class NickRepositoryTest : DapperRepositoryTestBase<Nick, NickRepository>
{
    #region Private Methods
    
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
    
    #endregion
    
    #region FindByNicknameAndProtocol(nickname, protocol) tests

    [Fact]
    public void Test_FindByNicknameAndProtocol_FindsNickByNicknameAndProtocol()
    {
        var nick = InsertValidEntity();
        
        Assert.Equivalent(
            nick,
            _target.FindByNicknameAndProtocol(nick.Nickname, nick.Protocol));
    }

    [Fact]
    public void Test_FindByNicknameAndProtocol_ReturnsNull_WhenNicknameAndProtocolNotFound()
    {
        var nick = InsertValidEntity();

        Assert.Null(_target.FindByNicknameAndProtocol(
            "not a real nickname",
            "not a real protocol"));

        Assert.Null(_target.FindByNicknameAndProtocol(
            nick.Nickname,
            "not a real protocol"));

        Assert.Null(_target.FindByNicknameAndProtocol(
            "not a real nickname",
            nick.Protocol));
    }
    
    #endregion
}