using System;
using DateTimeProvider;
using Lamar;
using Microsoft.Extensions.Logging;

namespace DammitBot.Library;

public abstract class UnitTestBase<TTarget> : IDisposable
{
    #region Private Members

    protected readonly IContainer _container;
    protected TTarget _target;
    protected TestDateTimeProvider _dateTimeProvider;
    protected DateTime _now;

    #endregion

    #region Private Methods

    protected virtual IContainer CreateContainer()
    {
        return new Container(serviceRegistry => {
            serviceRegistry.IncludeRegistry(BaseServiceRegistry.Registry);
            ConfigureContainer(serviceRegistry);
        });
    }

    protected virtual void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        _dateTimeProvider = serviceRegistry
            .For<IDateTimeProvider>()
            .Use<IDateTimeProvider, TestDateTimeProvider>(
                new TestDateTimeProvider(_now = DateTime.Now));

        serviceRegistry.For(typeof(ILogger<>)).Use(typeof(MockLogger<>));
    }

    protected virtual void ExtraSetup() {}

    protected virtual TTarget CreateTarget()
    {
        return _container.GetInstance<TTarget>();
    }

    #endregion

    #region Setup/Teardown

    public UnitTestBase()
    {
        _container = CreateContainer();

        ExtraSetup();

        _target = CreateTarget();
    }

    public virtual void Dispose()
    {
        if (_target is IDisposable d)
        {
            d.Dispose();
        }
        
        _container.Dispose();
    }

    #endregion
}
