using Lamar;
using Moq;

namespace DammitBot.Library;

public static class InstanceExpressionExtensions
{
    public static Mock<T> Mock<T>(this ServiceRegistry.InstanceExpression<T> expr)
        where T : class
    {
        var mock = new Mock<T>();
        expr.Use(mock.Object);
        return mock;
    }

    public static TConcrete Use<TInterface, TConcrete>(
        this ServiceRegistry.InstanceExpression<TInterface> expr,
        TConcrete instance)
        where TInterface : class
        where TConcrete : TInterface
    {
        expr.Use(instance);
        return instance;
    }
}