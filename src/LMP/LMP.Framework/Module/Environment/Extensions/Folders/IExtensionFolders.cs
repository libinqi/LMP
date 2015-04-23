using System.Collections.Generic;
using LMP.Module.Environment.Extensions.Models;

namespace LMP.Module.Environment.Extensions.Folders {
    public interface IExtensionFolders {
        IEnumerable<ExtensionDescriptor> AvailableExtensions();
    }
}