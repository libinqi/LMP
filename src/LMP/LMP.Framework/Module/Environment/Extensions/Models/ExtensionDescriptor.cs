using System.Collections.Generic;

namespace LMP.Module.Environment.Extensions.Models {
    public class ExtensionDescriptor {
        /// <summary>
        /// Virtual path base,"~/Modules"
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Folder name under virtual path base
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The extension type.
        /// </summary>
        public string ExtensionType { get; set; }
        
        // extension metadata
        public string Name { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }
        public string SessionState { get; set; }
    }
}