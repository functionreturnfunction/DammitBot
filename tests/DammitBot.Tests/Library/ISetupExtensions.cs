using System.Threading.Tasks;
using Moq;
using Moq.Language.Flow;

namespace DammitBot.Library;

public static class ISetupExtensions
{
    public static Mock<TResult> Mock<T, TResult>(this ISetup<T, TResult> that)
        where T : class
        where TResult : class
    {
        var mock = new Mock<TResult>();
        that.Returns(mock.Object);
        return mock;
    }

    public static Mock<TResult> MockAsync<T, TResult>(this ISetup<T, Task<TResult>> that)
        where T : class
        where TResult : class
    {
        var mock = new Mock<TResult>();
        that.Returns(Task.FromResult(mock.Object));
        return mock;
    }
}