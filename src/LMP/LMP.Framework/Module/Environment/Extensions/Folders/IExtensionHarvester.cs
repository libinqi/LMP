using System.Collections.Generic;
using LMP.Module.Environment.Extensions.Models;

namespace LMP.Module.Environment.Extensions.Folders {
    public interface IExtensionHarvester {
        IEnumerable<ExtensionDescriptor> HarvestExtensions(IEnumerable<string> paths, string extensionType, string manifestName, bool manifestIsOptional);
    }
}