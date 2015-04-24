using Abp.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using LMP.FileSystems.AppData;
using LMP.FileSystems.VirtualPath;

namespace LMP.Module.Reflection
{
    /// <summary>
    /// Default implementation of <see cref="IAssemblyFinder"/>.
    /// If gets assemblies from current domain.
    /// </summary>
    internal class LMPAssemblyFinder : IAssemblyFinder
    {
        private const string module_path = "Dependencies";
        private readonly IVirtualPathProvider _virtualPathProvider;
        private readonly IAppDataFolderRoot _appDataFolderRoot;
        List<Assembly> moduleAssemblies;

        /// <summary>
        /// Private constructor to disable instancing.
        /// </summary>
        private LMPAssemblyFinder(IAppDataFolderRoot appDataFolderRoot, IVirtualPathProvider virtualPathProvider)
        {
            _appDataFolderRoot = appDataFolderRoot;
            _virtualPathProvider = virtualPathProvider;
            moduleAssemblies = new List<Assembly>();
        }

        public List<Assembly> GetAllAssemblies()
        {
            moduleAssemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies().ToList());
            GetModuleAssemblies();
            return moduleAssemblies;
        }

        public void GetModuleAssemblies()
        {
            string moduleAssembliesDirectory = Path.Combine(_appDataFolderRoot.RootPath, module_path);
            var moduleStrArray = _virtualPathProvider.ListFiles(module_path).Where(s => StringComparer.OrdinalIgnoreCase.Equals(Path.GetExtension(s), ".dll")).ToList();
            Assembly assemblie = null;

            foreach (var item in moduleStrArray)
            {
                assemblie = Assembly.LoadFile(item);
                if (assemblie != null)
                {
                    moduleAssemblies.Add(Assembly.LoadFile(item));
                }
            }
        }
    }
}