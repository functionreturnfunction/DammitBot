using System;
using System.Linq;
using DammitBot.Abstract;
using DammitBot.Configuration;
using DammitBot.Utilities;
using DateTimeProvider;
using Lamar;
using Microsoft.Extensions.DependencyInjection;

namespace DammitBot.IoC;

/// <inheritdoc cref="ContainerConfigurationBase"/>
/// <remarks>
/// This implementation configures types at the top/core level, used by the bot itself and the
/// plugin/configuration architecture.
/// </remarks>
public class DammitBotContainerConfiguration : ContainerConfigurationBase
{
    #region Private Methods

    private static IAssemblyTypeService InitializePluginConfigurations(ServiceRegistry e)
    {
        var assemblyService = new AssemblyTypeService();

        foreach (
            var type in
            assemblyService.GetTypesFromPluginAssemblies()
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

        e.For<IBot>().Use<Bot>().Singleton();

        var assemblyService = InitializePluginConfigurations(e);

        e.For<IAssemblyTypeService>()
            .Use(assemblyService).Singleton();

        e.For<IDateTimeProvider>().Use<SystemClockDateTimeProvider>();

        e.AddOptions<BotConfiguration>()
            .BindConfiguration("DammitBot")
            .ValidateDataAnnotations();
    }
    
    #endregion
}