using Abp.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;

namespace LMP.Module.Reflection
{
    /// <summary>
    /// Default implementation of <see cref="IAssemblyFinder"/>.
    /// If gets assemblies from current domain.
    /// </summary>
    internal class LMPAssemblyFinder : IAssemblyFinder
    {
        private const string module_path = "~/LMP.Modules";
        /// <summary>
        /// Gets Singleton instance of <see cref="LMPAssemblyFinder"/>.
        /// </summary>
        public static LMPAssemblyFinder Instance { get { return SingletonInstance; } }
        private static readonly LMPAssemblyFinder SingletonInstance = new LMPAssemblyFinder();

        /// <summary>
        /// Private constructor to disable instancing.
        /// </summary>
        private LMPAssemblyFinder()
        {

        }

        public List<Assembly> GetAllAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().ToList();
        }

        public List<Assembly> GetModuleAssemblies()
        {
            string moduleAssembliesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, module_path);
            return null;
        }
    }
}