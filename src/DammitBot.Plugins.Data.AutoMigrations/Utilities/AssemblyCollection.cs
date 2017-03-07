using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentMigrator.Infrastructure;

namespace DammitBot.Utilities
{
    public class AssemblyCollection : IAssemblyCollection
    {
        #region Properties

        public Assembly[] Assemblies { get; }

        #endregion

        #region Constructors

        public AssemblyCollection(IEnumerable<Assembly> assemblies)
        {
            Assemblies = assemblies.ToArray();
        }

        #endregion

        #region Exposed Methods

        public Type[] GetExportedTypes()
        {
            return Assemblies.SelectMany(a => a.GetExportedTypes()).ToArray();
        }

        public ManifestResourceNameWithAssembly[] GetManifestResourceNames()
        {
            return Assemblies.SelectMany(
                    a => a.GetManifestResourceNames().Select(
                        n => new ManifestResourceNameWithAssembly(n, a)))
                .ToArray();
        }

        #endregion
    }
}