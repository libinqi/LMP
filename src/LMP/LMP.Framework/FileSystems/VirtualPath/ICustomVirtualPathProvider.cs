using System.Web.Hosting;

namespace LMP.FileSystems.VirtualPath
{
    public interface ICustomVirtualPathProvider {
        VirtualPathProvider Instance { get; }
    }
}