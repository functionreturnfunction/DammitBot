using System;
using System.Linq;
using DammitBot.Abstract;
using DammitBot.Configuration;
using DammitBot.Utilities;
using DateTimeProvider;
using Lamar;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DammitBot.IoC;

/// <inheritdoc cref="ContainerConfigurationBase"/>
/// <remarks>
/// This implementation configures types at the top/core level, used by the bot itself and the
/// plugin/configuration architecture.
/// </remarks>
public class DammitBotContainerConfiguration : ContainerConfigurationBase
{
    #region Private Methods

    private static IAssemblyTypeService InitializePluginConfigurations(
        ServiceRegistry e,
        IOptions<BotConfiguration> botConfiguration)
    {
        var assemblyTypeService = new AssemblyTypeService(botConfiguration);
        
        foreach (
            var type in
            assemblyTypeService.GetTypesFromPluginAssemblies()
                .Where(t => !t.IsAbstract &&
                            t.IsSubclassOf(typeof(ContainerConfigurationBase))))
        {
            ((ContainerConfigurationBase)Activator.CreateInstance(type)!).Configure(e);
        }

        return assemblyTypeService;
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
        
        e.AddOptions<BotConfiguration>()
            .BindConfiguration("DammitBot")
            .ValidateDataAnnotations();

        var assemblyService = InitializePluginConfigurations(
            e,
            e.BuildServiceProvider().GetRequiredService<IOptions<BotConfiguration>>());

        e.For<IAssemblyTypeService>()
            .Use(assemblyService).Singleton();

        e.For<IDateTimeProvider>().Use<SystemClockDateTimeProvider>();
    }
    
    #endregion
}