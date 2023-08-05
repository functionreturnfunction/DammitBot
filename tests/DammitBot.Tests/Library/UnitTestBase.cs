using System;
using DammitBot.Configuration;
using DateTimeProvider;
using Lamar;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace DammitBot.Library;

public abstract class UnitTestBase<TTarget> : IDisposable
{
    #region Private Members

    protected readonly IContainer _container;
    protected Mock<ILogger<TTarget>> _log;
    protected TTarget _target;
    protected TestDateTimeProvider _dateTimeProvider;
    protected DateTime _now;

    #endregion

    #region Private Methods

    protected virtual IContainer CreateContainer()
    {
        return new Container(ConfigureContainer);
    }

    protected virtual void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        serviceRegistry.Scan(a => {
            a.AssembliesFromApplicationBaseDirectory();
            a.WithDefaultConventions();
        });
        
        serviceRegistry.For<IConfigurationBuilder>().Use<ConfigurationBuilder>();
        serviceRegistry.For<ISettingsPathHelper>().Use<TestSettingsPathHelper>();

        _log = serviceRegistry.For<ILogger<TTarget>>().Mock();
        _dateTimeProvider = serviceRegistry
            .For<IDateTimeProvider>()
            .Use<IDateTimeProvider, TestDateTimeProvider>(
                new TestDateTimeProvider(_now = DateTime.Now));
    }

    protected virtual void ExtraSetup() {}

    protected virtual TTarget ConstructTarget()
    {
        return _container.GetInstance<TTarget>();
    }

    #endregion

    #region Setup/Teardown

    public UnitTestBase()
    {
        _container = CreateContainer();

        ExtraSetup();

        _target = ConstructTarget();
    }

    public virtual void Dispose() {}

    #endregion
}