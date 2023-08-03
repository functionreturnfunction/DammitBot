using System;
using System.Reflection;
using System.Linq;
using DammitBot.Abstract;
using DammitBot.Utilities;
using DammitBot.Wrappers;
using Lamar;
using log4net;
using log4net.Repository;
using log4net.Config;
using log4net.Core;
using Microsoft.Extensions.Configuration;

namespace DammitBot.Ioc;

public class DammitBotContainerConfiguration : ContainerConfigurationBase
{
    #region Private Methods

    public override void Configure(ServiceRegistry e)
    {
        e.Scan(s => {
            s.AssembliesFromApplicationBaseDirectory();
            s.WithDefaultConventions();
        });

        e.For<IBot>()
            .Use<Bot>().Singleton();

        var assemblyService = InitializePluginConfigurations(e);

        e.For<IAssemblyService>()
            .Use(assemblyService).Singleton();

        e.For<ILoggerRepository>()
            .Use(ctx => LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy)))
            .Singleton();

        e.For<IConfigurationBuilder>().Use<ConfigurationBuilder>();

        e.For<ILog>().Use(ctx => LogManager.GetLogger("DammitBot Global"));
    }

    private static IAssemblyService InitializePluginConfigurations(ServiceRegistry e)
    {
        var assemblyService = new AssemblyService();

        foreach (
            var type in
            assemblyService.GetPluginAssemblies()
                .GetTypes()
                .Where(t => !t.IsAbstract &&
                            t.IsSubclassOf(typeof(ContainerConfigurationBase))))
        {
            ((ContainerConfigurationBase)Activator.CreateInstance(type)).Configure(e);
        }

        return assemblyService;
    }

    #endregion

    #region Exposed Methods

    public static IInstantiationService GetContainer()
    {
        var registrar = new DammitBotContainerConfiguration();
        var container = new Container(e => registrar.Configure(e));
        XmlConfigurator.Configure(container.GetInstance<ILoggerRepository>());

        return new InstantiationService(container);
    }

    #endregion
}