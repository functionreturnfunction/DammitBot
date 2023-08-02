using System;
using DammitBot.Configuration;
using DateTimeStringParser;
using log4net;
using Microsoft.Extensions.Configuration;
using Moq;
using StructureMap;

namespace DammitBot.Library;

public abstract class UnitTestBase<TTarget> : IDisposable
{
    #region Private Members

    protected readonly IContainer _container;
    protected readonly Mock<ILog> _log;
    protected TTarget _target;
    protected readonly TestDateTimeProvider _dateTimeProvider;
    protected readonly DateTime _now;

    #endregion

    #region Private Methods

    protected virtual IContainer CreateContainer()
    {
        return new Container();
    }

    protected virtual void ConfigureContainer()
    {
        _container.Configure(e => {
            e.Scan(a => {
                a.AssembliesFromApplicationBaseDirectory();
                a.WithDefaultConventions();
            });
            e.For<IConfigurationBuilder>().Use<ConfigurationBuilder>();
            e.For<ISettingsPathHelper>().Use<TestSettingsPathHelper>();
        });
    }

    protected virtual void ExtraSetup() {}

    protected virtual void Inject<TMock>(
        out Mock<TMock> obj,
        MockBehavior behavior = MockBehavior.Default)
        where TMock : class
    {
        Inject((obj = new Mock<TMock>(behavior)).Object);
    }

    protected virtual void Inject<TMock>()
        where TMock : class
    {
        Mock<TMock> throwaway;
        Inject(out throwaway);
    }

    protected virtual void Inject<TObj>(TObj obj)
        where TObj : class
    {
        _container.Inject(obj);
    }

    protected virtual TTarget ConstructTarget()
    {
        return _container.GetInstance<TTarget>();
    }

    #endregion

    #region Setup/Teardown

    public UnitTestBase()
    {
        _container = CreateContainer();
        Inject(out _log);
        _dateTimeProvider = new TestDateTimeProvider(_now = DateTime.Now);
        Inject<IDateTimeProvider>(_dateTimeProvider);

        ConfigureContainer();
        ExtraSetup();

        _target = ConstructTarget();
    }

    public virtual void Dispose() {}

    #endregion
}