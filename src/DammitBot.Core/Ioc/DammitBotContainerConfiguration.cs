using System;
using System.Reflection;
using System.Linq;
using DammitBot.Abstract;
using DammitBot.Configuration;
using DammitBot.Utilities;
using DammitBot.Utilities.AssemblyEnumerableExtensions;
using DammitBot.Wrappers;
using log4net;
using log4net.Repository;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using StructureMap;

namespace DammitBot.Ioc
{
    public class DammitBotContainerConfiguration : ContainerConfigurationBase
    {
        #region Private Methods

        public override void Configure(ConfigurationExpression e)
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

            e.For<ILog>()
                .AlwaysUnique()
                .Use(
                     ctx =>
                     ctx.ParentType == null
                     ? LogManager.GetLogger(
                         ctx.GetInstance<ILoggerRepository>().Name,
                         "DammitBot Global")
                     : LogManager.GetLogger(ctx.ParentType));
        }

        private static IAssemblyService InitializePluginConfigurations(ConfigurationExpression e)
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
}
