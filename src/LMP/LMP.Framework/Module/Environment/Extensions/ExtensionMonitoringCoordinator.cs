using LMP.Caching;
using LMP.FileSystems.VirtualPath;
using LMP.Module.Environment.Extensions.Loaders;
using LMP.Module.Environment.Extensions.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LMP.Module.Environment.Extensions
{
    public class ExtensionMonitoringCoordinator : IExtensionMonitoringCoordinator {
        private readonly IVirtualPathMonitor _virtualPathMonitor;
        private readonly IAsyncTokenProvider _asyncTokenProvider;
        private readonly IExtensionManager _extensionManager;
        private readonly IEnumerable<IExtensionLoader> _loaders;
        private readonly IExtensionLoader _loader;

        public ExtensionMonitoringCoordinator(
            IVirtualPathMonitor virtualPathMonitor,
            IAsyncTokenProvider asyncTokenProvider,
            IExtensionManager extensionManager,
            IExtensionLoader loader) {

            _virtualPathMonitor = virtualPathMonitor;
            _asyncTokenProvider = asyncTokenProvider;
            _extensionManager = extensionManager;
            _loader = loader;
            _loaders = new List<IExtensionLoader>() { _loader };
        }

        public bool Disabled { get; set; }

        public void MonitorExtensions(Action<IVolatileToken> monitor) {
            // We may be disabled by custom host configuration for performance reasons
            if (Disabled)
                return;

            //PERF: Monitor extensions asynchronously.
            monitor(_asyncTokenProvider.GetToken(MonitorExtensionsWork));
        }

        public void MonitorExtensionsWork(Action<IVolatileToken> monitor) {
            //Logger.Information("Start monitoring extension files...");
            //// Monitor add/remove of any module/theme
            //Logger.Debug("Monitoring virtual path \"{0}\"", "~/Modules");
            monitor(_virtualPathMonitor.WhenPathChanges("~/LMP.Modules"));
            //Logger.Debug("Monitoring virtual path \"{0}\"", "~/Themes");
            //monitor(_virtualPathMonitor.WhenPathChanges("~/Themes"));

            // Give loaders a chance to monitor any additional changes
            var extensions = _extensionManager.AvailableExtensions().Where(d => DefaultExtensionTypes.IsModule(d.ExtensionType)).ToList();
            foreach (var extension in extensions) {
                foreach (var loader in _loaders) {
                    loader.Monitor(extension, monitor);
                }
            }
            //Logger.Information("Done monitoring extension files...");
        }
    }
}