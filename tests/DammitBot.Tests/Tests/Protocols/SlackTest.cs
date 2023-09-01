using DammitBot.Configuration;
using DammitBot.IoC;
using DammitBot.Library;
using DammitBot.Protocols;
using Lamar;
using Microsoft.Extensions.Options;
using Xunit;

namespace DammitBot.Tests.Protocols;

public class SlackTest : ProtocolTestBase<Slack, ISlackClient, ISlackClientFactory>
{
    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);
        
        new SlackProtocolContainerConfiguration().Configure(serviceRegistry);
    }

    [Fact]
    public void Test_Cleanup_DisposesOfClient()
    {
        _target.Initialize();

        _target.Cleanup();
        
        _client.Verify(x => x.Dispose());
    }

    [Fact]
    public void Test_SayToAll_SendsMessageToNoChannelInParticular()
    {
        var message = "blah blah blah";
        
        _target.Initialize();
        
        _target.SayToAll(message);

        _client.Verify(x => x.SendMessage(message));
    }

    [Fact]
    public void Test_Configuration_CanBeReadFromSettings()
    {
        var config = _container.GetInstance<IOptions<SlackConfiguration>>().Value;
        
        Assert.Equal("appLevelToken",config.AppLevelToken);
        Assert.Equal("apiToken", config.ApiToken);
    }
}