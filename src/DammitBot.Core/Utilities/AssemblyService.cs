using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DammitBot.Utilities
{
    public class AssemblyService : IAssemblyService
    {
        #region Private Members

        private Assembly? _mainAssembly;
        private IEnumerable<Assembly>? _pluginAssemblies;

        #endregion

        #region Properties

        public Assembly MainAssembly => _mainAssembly ??= typeof(IBot).Assembly;

        #endregion

        #region Private Methods

        private IEnumerable<Assembly> InnerGetPluginAssemblies()
        {
            var dlls = Directory.GetFileSystemEntries(
                MainAssembly.GetDirectory(),
                "DammitBot.Plugins.*.dll");

            foreach (var dll in dlls)
            {
                yield return LoadAssembly(dll);
            }
        }

        private Assembly LoadAssembly(string dll)
        {
            return Assembly.LoadFrom(dll);
        }

        #endregion

        #region Exposed Methods

        public IEnumerable<Assembly> GetAllAssemblies()
        {
            yield return MainAssembly;

            foreach (var assembly in GetPluginAssemblies())
            {
                yield return assembly;
            }
        }

        public IEnumerable<Assembly> GetPluginAssemblies()
        {
            return _pluginAssemblies ??= InnerGetPluginAssemblies().ToList();
        }

        #endregion
    }
}
