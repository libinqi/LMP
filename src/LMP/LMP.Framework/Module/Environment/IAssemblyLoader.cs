﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LMP.Module.Environment {
    public interface IAssemblyLoader {
        Assembly Load(string assemblyName);
    }

    public class DefaultAssemblyLoader : IAssemblyLoader {
        private readonly IAssemblyNameResolver _assemblyNameResolver;
        private readonly ConcurrentDictionary<string, Assembly> _loadedAssemblies = new ConcurrentDictionary<string, Assembly>(StringComparer.OrdinalIgnoreCase);

        public DefaultAssemblyLoader(IAssemblyNameResolver assemblyNameResolver) {
            _assemblyNameResolver = assemblyNameResolver;
        }

        public Assembly Load(string assemblyName) {
            try {
                return _loadedAssemblies.GetOrAdd(this.ExtractAssemblyShortName(assemblyName), shortName => LoadWorker(shortName, assemblyName));
            }
            catch (Exception e) {
                //Logger.Error(e, "Error loading assembly '{0}'", assemblyName);
                return null;
            }
        }

        private Assembly LoadWorker(string shortName, string fullName) {
            Assembly result;

            // Try loading with full name first (if there is a full name)
            if (fullName != shortName) {
                result = TryAssemblyLoad(fullName);
                if (result != null)
                    return result;
            }

            // Try loading with short name
            result = TryAssemblyLoad(shortName);
            if (result != null)
                return result;

            // Try resolving the short name to a full name
            var resolvedName = _assemblyNameResolver.Resolve(shortName);
            if (resolvedName != null) {
                return Assembly.Load(resolvedName);
            }

            // Try again so that we get the exception this time
            return Assembly.Load(fullName);
        }

        private static Assembly TryAssemblyLoad(string name) {
            try {
                return Assembly.Load(name);
            }
            catch {
                return null;
            }
        }
    }

    public static class AssemblyLoaderExtensions {
        public static string ExtractAssemblyShortName(this IAssemblyLoader assemblyLoader, string fullName) {
            return ExtractAssemblyShortName(fullName);
        }

        public static string ExtractAssemblyShortName(string fullName) {
            int index = fullName.IndexOf(',');
            if (index < 0)
                return fullName;
            return fullName.Substring(0, index);
        }
    }
}