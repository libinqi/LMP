using LMP.Module.Environment.Extensions.Models;
using System.Collections.Generic;
using System.Linq;

namespace LMP.Module.Environment.Extensions
{
    public interface IExtensionManager {
        IEnumerable<ExtensionDescriptor> AvailableExtensions();

        ExtensionDescriptor GetExtension(string id);
    }

    public static class ExtensionManagerExtensions {
    }
}
