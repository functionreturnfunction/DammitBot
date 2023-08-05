using System;
using System.Linq;
using DammitBot.Abstract;
using DammitBot.Utilities;
using DateTimeProvider;
using Lamar;
using Microsoft.Extensions.Configuration;

namespace DammitBot.Ioc;

/// <inheritdoc cref="ContainerConfigurationBase"/>
/// <remarks>
/// This implementation configures types at the top/core level, used by the bot itself and the
/// plugin/configuration architecture.
/// </remarks>
public class DammitBotContainerConfiguration : ContainerConfigurationBase
{
    #region Private Methods

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
    
    #region Exposed Methods
    
    /// <inheritdoc cref="ContainerConfigurationBase.Configure"/>
    /// <inheritdoc cref="DammitBotContainerConfiguration" path="Remarks"/>
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
    
    #endregion
}