using System.Collections.Generic;
using System.Reflection;

namespace DammitBot.Utilities;

public interface IAssemblyService
{
    #region Abstract Methods

    IEnumerable<Assembly> GetAllAssemblies();
    IEnumerable<Assembly> GetPluginAssemblies();

    #endregion
}