using LMP.Caching;
using System;

namespace LMP.Module.Environment.Extensions
{
    public interface IExtensionMonitoringCoordinator {
        void MonitorExtensions(Action<IVolatileToken> monitor);
    }
}