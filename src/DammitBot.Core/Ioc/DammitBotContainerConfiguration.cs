using System;
using System.Linq;
using DammitBot.Abstract;
using DammitBot.Utilities;
using DateTimeStringParser;
using Lamar;
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

        e.For<IConfigurationBuilder>().Use<ConfigurationBuilder>();

        e.For<IDateTimeProvider>().Use<SystemClockDateTimeProvider>();
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
            ((ContainerConfigurationBase)Activator.CreateInstance(type)!).Configure(e);
        }

        return assemblyService;
    }

    #endregion
}