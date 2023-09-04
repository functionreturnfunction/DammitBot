using DammitBot.Configuration;
using Lamar;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DammitBot.Library;

/// <summary>
/// Scanning all the assemblies for types to register takes time, and only really needs to happen once
/// for all the tests which inherit from this class.  Thus we build this static ServiceRegistry once
/// in a singleton and then use it repeatedly when creating containers in the instance constructor of
/// UnitTestBase, which gets called by all its inheritors.
/// </summary>
internal static class BaseServiceRegistry
{
    private static ServiceRegistry _registry;
    public static ServiceRegistry Registry => _registry ??= CreateBaseRegistry(); 

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

        return serviceRegistry;
    }
}