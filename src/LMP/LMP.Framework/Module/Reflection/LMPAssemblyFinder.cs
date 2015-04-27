using Abp.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using LMP.FileSystems.AppData;
using LMP.FileSystems.VirtualPath;
using System.Web.Compilation;
using System.Web;

namespace LMP.Module.Reflection
{
    /// <summary>
    /// Default implementation of <see cref="IAssemblyFinder"/>.
    /// If gets assemblies from current domain.
    /// </summary>
    public class LMPAssemblyFinder : IAssemblyFinder
    {
        private const string module_path = "Dependencies";
        private readonly IVirtualPathProvider _virtualPathProvider;
        private readonly IAppDataFolderRoot _appDataFolderRoot;
        /// <summary>
        /// Private constructor to disable instancing.
        /// </summary>
        public LMPAssemblyFinder(IAppDataFolderRoot appDataFolderRoot, IVirtualPathProvider virtualPathProvider)
        {
            _appDataFolderRoot = appDataFolderRoot;
            _virtualPathProvider = virtualPathProvider;
        }

        public List<Assembly> GetAllAssemblies()
        {
            var allReferencedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            string moduleAssembliesDirectory = Path.Combine(_appDataFolderRoot.RootPath, module_path);
            var moduleStrArray = _virtualPathProvider.ListFiles(moduleAssembliesDirectory).Where(s => StringComparer.OrdinalIgnoreCase.Equals(Path.GetExtension(s), ".dll")).ToList();

            foreach (var item in moduleStrArray)
            {
               var assemblie = Assembly.LoadFile(_virtualPathProvider.MapPath(item));
                if (assemblie != null && !allReferencedAssemblies.Any(a => a.FullName == assemblie.FullName))
                {
                    allReferencedAssemblies.Add(assemblie);
                }
            }
            return allReferencedAssemblies;
        }      
    }
}