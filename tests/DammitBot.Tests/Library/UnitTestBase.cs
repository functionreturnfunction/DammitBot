using System;
using DammitBot.Configuration;
using DateTimeProvider;
using Lamar;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DammitBot.Library;

public abstract class UnitTestBase<TTarget> : IDisposable
{
    #region Private Members

    // Scanning all the assemblies for types to register takes time, and only really needs to happen once
    // for all the tests which inherit from this class.  Thus we build this static ServiceRegistry once
    // from the static constructor and then use it repeatedly to build the container for each individual
    // test in the instance constructor.
    private static ServiceRegistry? _baseRegistry;
    protected readonly IContainer _container;
    protected TTarget _target;
    protected TestDateTimeProvider _dateTimeProvider;
    protected DateTime _now;

    #endregion

    #region Private Methods

    protected virtual IContainer CreateContainer()
    {
        return new Container(serviceRegistry => {
            serviceRegistry.IncludeRegistry(_baseRegistry);
            ConfigureContainer(serviceRegistry);
        });
    }

    protected virtual void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        _dateTimeProvider = serviceRegistry
            .For<IDateTimeProvider>()
            .Use<IDateTimeProvider, TestDateTimeProvider>(
                new TestDateTimeProvider(_now = DateTime.Now));
    }

    protected virtual void ExtraSetup() {}

    protected virtual TTarget CreateTarget()
    {
        return _container.GetInstance<TTarget>();
    }

    #endregion

    #region Setup/Teardown

    static UnitTestBase()
    {
        _baseRegistry ??= CreateBaseRegistry();
    }

    public UnitTestBase()
    {
        _container = CreateContainer();

        ExtraSetup();

        _target = CreateTarget();
    }

    private static ServiceRegistry CreateBaseRegistry()
    {
        var serviceRegistry = new ServiceRegistry();
        
        serviceRegistry.Scan(a => {
            a.AssembliesFromApplicationBaseDirectory();
            a.WithDefaultConventions();
        });

        serviceRegistry
            .For<IConfiguration>()
            .Use(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());

        serviceRegistry.AddOptions<BotConfiguration>()
            .BindConfiguration("DammitBot")
            .ValidateDataAnnotations();

        serviceRegistry.AddOptions<DataConfiguration>()
            .BindConfiguration("Data")
            .ValidateDataAnnotations();

        serviceRegistry.For<ILogger<TTarget>>().Mock();
        
        return serviceRegistry;
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