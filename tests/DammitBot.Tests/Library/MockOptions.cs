using Microsoft.Extensions.Options;
using Moq;

namespace DammitBot.Library;

public class MockOptions<TConfig> : Mock<IOptions<TConfig>>
    where TConfig : class
{
    public MockOptions()
    {
        Setup(x => x.Value).Mock();
    }
}