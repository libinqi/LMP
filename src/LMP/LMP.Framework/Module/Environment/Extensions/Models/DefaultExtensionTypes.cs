using System;

namespace LMP.Module.Environment.Extensions.Models {
    public static class DefaultExtensionTypes {
        public const string Module = "Module";

        public static bool IsModule(string extensionType) {
            return string.Equals(extensionType, Module, StringComparison.OrdinalIgnoreCase);
        }
    }
}