using System;
using System.Linq;
using DammitBot.Abstract;
using DammitBot.Configuration;
using DammitBot.Utilities;
using DammitBot.Utilities.AssemblyEnumerableExtensions;
using DammitBot.Wrappers;
using log4net;
using log4net.Config;
using StructureMap;

namespace DammitBot.Ioc
{
    public class DependencyRegistrar
    {
        private readonly Container _container;

        #region Constructors

        static DependencyRegistrar()
        {
            XmlConfigurator.Configure();
        }

        public DependencyRegistrar(Container container)
        {
            _container = container;
        }

        #endregion

        #region Private Methods

        public void ConfigureContainer(ConfigurationExpression e)
        {
            e.Scan(s => {
                s.AssembliesFromApplicationBaseDirectory();
                s.WithDefaultConventions();
            });

            e.For<ILog>()
                .AlwaysUnique()
                .Use(
                    ctx =>
                        ctx.ParentType == null
                            ? LogManager.GetLogger("DammitBot Global")
                            : LogManager.GetLogger(ctx.ParentType));
            e.For<IBot>()
                .Use<Bot>().Singleton();

            var assemblyService = InitializePluginConfigurations(e);

            e.For<IAssemblyService>()
                .Use<AssemblyService>().Singleton();
        }

        private static IAssemblyService InitializePluginConfigurations(ConfigurationExpression e)
        {
            var assemblyService = new AssemblyService();

            foreach (
                var type in
                assemblyService.GetPluginAssemblies()
                    .GetTypes()
                    .Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(PluginContainerConfigurationBase))))
            {
                ((PluginContainerConfigurationBase)Activator.CreateInstance(type)).Configure(e);
            }

            return assemblyService;
        }

        #endregion

        #region Exposed Methods

        public static IInstantiationService GetContainer()
        {
            var container = new Container();
            var registrar = new DependencyRegistrar(container);

            container.Configure(e => registrar.ConfigureContainer(e));

            return new InstantiationService(container);
        }

        #endregion
    }
}