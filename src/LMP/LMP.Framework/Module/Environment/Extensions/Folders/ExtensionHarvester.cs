using Abp.Localization;
using LMP.Caching;
using LMP.FileSystems.WebSite;
using LMP.Module.Environment.Extensions.Models;
using LMP.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LMP.Module.Environment.Extensions.Folders {
    public class ExtensionHarvester : IExtensionHarvester {
        private const string NameSection = "name";
        private const string PathSection = "path";
        private const string DescriptionSection = "description";
        private const string VersionSection = "version";
        private const string AuthorSection = "author";
        private const string CategorySection = "category";
        private const string SessionStateSection = "sessionstate";

        private readonly ICacheManager _cacheManager;
        private readonly IWebSiteFolder _webSiteFolder;
        private readonly ICriticalErrorProvider _criticalErrorProvider;

        public ExtensionHarvester(ICacheManager cacheManager, IWebSiteFolder webSiteFolder, ICriticalErrorProvider criticalErrorProvider) {
            _cacheManager = cacheManager;
            _webSiteFolder = webSiteFolder;
            _criticalErrorProvider = criticalErrorProvider;
        }

        public bool DisableMonitoring { get; set; }

        public IEnumerable<ExtensionDescriptor> HarvestExtensions(IEnumerable<string> paths, string extensionType, string manifestName, bool manifestIsOptional) {
            return paths
                .SelectMany(path => HarvestExtensions(path, extensionType, manifestName, manifestIsOptional))
                .ToList();
        }

        private IEnumerable<ExtensionDescriptor> HarvestExtensions(string path, string extensionType, string manifestName, bool manifestIsOptional) {
            string key = string.Format("{0}-{1}-{2}", path, manifestName, extensionType);

            return _cacheManager.Get(key, ctx => {
                if (!DisableMonitoring) {
                    //Logger.Debug("Monitoring virtual path \"{0}\"", path);
                    ctx.Monitor(_webSiteFolder.WhenPathChanges(path));
                }

                return AvailableExtensionsInFolder(path, extensionType, manifestName, manifestIsOptional).ToReadOnlyCollection();
            });
        }

        private List<ExtensionDescriptor> AvailableExtensionsInFolder(string path, string extensionType, string manifestName, bool manifestIsOptional) {
            //Logger.Information("Start looking for extensions in '{0}'...", path);
            var subfolderPaths = _webSiteFolder.ListDirectories(path);
            var localList = new List<ExtensionDescriptor>();
            foreach (var subfolderPath in subfolderPaths) {
                var extensionId = Path.GetFileName(subfolderPath.TrimEnd('/', '\\'));
                var manifestPath = Path.Combine(subfolderPath, manifestName);
                try {
                    var descriptor = GetExtensionDescriptor(path, extensionId, extensionType, manifestPath, manifestIsOptional);

                    if (descriptor == null)
                        continue;

                    if (descriptor.Path != null && !descriptor.Path.IsValidUrlSegment()) {
                        //Logger.Error("The module '{0}' could not be loaded because it has an invalid Path ({1}). It was ignored. The Path if specified must be a valid URL segment. The best bet is to stick with letters and numbers with no spaces.",
                        //             extensionId,
                        //             descriptor.Path);
                        _criticalErrorProvider.RegisterErrorMessage(new LocalizedString(extensionId, string.Format("The extension '{0}' could not be loaded because it has an invalid Path ({1}). It was ignored. The Path if specified must be a valid URL segment. The best bet is to stick with letters and numbers with no spaces.",
                                     extensionId,
                                     descriptor.Path),null));
                        continue;
                    }

                    if (descriptor.Path == null) {
                        descriptor.Path = descriptor.Name.IsValidUrlSegment()
                                              ? descriptor.Name
                                              : descriptor.Id;
                    }

                    localList.Add(descriptor);
                }
                catch (Exception ex) {
                    // Ignore invalid module manifests
                    //Logger.Error(ex, "The module '{0}' could not be loaded. It was ignored.", extensionId);
                    _criticalErrorProvider.RegisterErrorMessage(new LocalizedString(extensionId, string.Format("The extension '{0}' manifest could not be loaded. It was ignored.", extensionId),null));
                }
            }
            //Logger.Information("Done looking for extensions in '{0}': {1}", path, string.Join(", ", localList.Select(d => d.Id)));
            return localList;
        }

        public static ExtensionDescriptor GetDescriptorForExtension(string locationPath, string extensionId, string extensionType, string manifestText) {
            Dictionary<string, string> manifest = ParseManifest(manifestText);
            var extensionDescriptor = new ExtensionDescriptor {
                Location = locationPath,
                Id = extensionId,
                ExtensionType = extensionType,
                Name = GetValue(manifest, NameSection) ?? extensionId,
                Path = GetValue(manifest, PathSection),
                Description = GetValue(manifest, DescriptionSection),
                Version = GetValue(manifest, VersionSection),
                Author = GetValue(manifest, AuthorSection),
                SessionState = GetValue(manifest, SessionStateSection)
            };

            return extensionDescriptor;
        }

        private ExtensionDescriptor GetExtensionDescriptor(string locationPath, string extensionId, string extensionType, string manifestPath, bool manifestIsOptional) {
            return _cacheManager.Get(manifestPath, context => {
                if (!DisableMonitoring) {
                    //Logger.Debug("Monitoring virtual path \"{0}\"", manifestPath);
                    context.Monitor(_webSiteFolder.WhenPathChanges(manifestPath));
                }

                var manifestText = _webSiteFolder.ReadFile(manifestPath);
                if (manifestText == null) {
                    if (manifestIsOptional) {
                        manifestText = string.Format("Id: {0}", extensionId);
                    }
                    else {
                        return null;
                    }
                }

                return GetDescriptorForExtension(locationPath, extensionId, extensionType, manifestText);
            });
        }

        private static Dictionary<string, string> ParseManifest(string manifestText) {
            var manifest = new Dictionary<string, string>();

            using (StringReader reader = new StringReader(manifestText)) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    string[] field = line.Split(new[] { ":" }, 2, StringSplitOptions.None);
                    int fieldLength = field.Length;
                    if (fieldLength != 2)
                        continue;
                    for (int i = 0; i < fieldLength; i++) {
                        field[i] = field[i].Trim();
                    }
                    switch (field[0].ToLowerInvariant()) {
                        case NameSection:
                            manifest.Add(NameSection, field[1]);
                            break;
                        case PathSection:
                            manifest.Add(PathSection, field[1]);
                            break;
                        case DescriptionSection:
                            manifest.Add(DescriptionSection, field[1]);
                            break;
                        case VersionSection:
                            manifest.Add(VersionSection, field[1]);
                            break;
                        case AuthorSection:
                            manifest.Add(AuthorSection, field[1]);
                            break;                     
                        case CategorySection:
                            manifest.Add(CategorySection, field[1]);
                            break;
                        case SessionStateSection:
                            manifest.Add(SessionStateSection, field[1]);
                            break;
                    }
                }
            }

            return manifest;
        }

        //private static IEnumerable<FeatureDescriptor> GetFeaturesForExtension(IDictionary<string, string> manifest, ExtensionDescriptor extensionDescriptor) {
        //    var featureDescriptors = new List<FeatureDescriptor>();

        //    // Default feature
        //    FeatureDescriptor defaultFeature = new FeatureDescriptor {
        //        Id = extensionDescriptor.Id,
        //        Name = GetValue(manifest, FeatureNameSection) ?? extensionDescriptor.Name,
        //        Priority = GetValue(manifest, PrioritySection) != null ? int.Parse(GetValue(manifest, PrioritySection)) : 0,
        //        Description = GetValue(manifest, FeatureDescriptionSection) ?? GetValue(manifest, DescriptionSection) ?? string.Empty,
        //        Dependencies = ParseFeatureDependenciesEntry(GetValue(manifest, DependenciesSection)),
        //        Extension = extensionDescriptor,
        //        Category = GetValue(manifest, CategorySection)
        //    };

        //    featureDescriptors.Add(defaultFeature);

        //    // Remaining features
        //    string featuresText = GetValue(manifest, FeaturesSection);
        //    if (featuresText != null) {
        //        FeatureDescriptor featureDescriptor = null;
        //        using (StringReader reader = new StringReader(featuresText)) {
        //            string line;
        //            while ((line = reader.ReadLine()) != null) {
        //                if (IsFeatureDeclaration(line)) {
        //                    if (featureDescriptor != null) {
        //                        if (!featureDescriptor.Equals(defaultFeature)) {
        //                            featureDescriptors.Add(featureDescriptor);
        //                        }

        //                        featureDescriptor = null;
        //                    }

        //                    string[] featureDeclaration = line.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
        //                    string featureDescriptorId = featureDeclaration[0].Trim();
        //                    if (String.Equals(featureDescriptorId, extensionDescriptor.Id, StringComparison.OrdinalIgnoreCase)) {
        //                        featureDescriptor = defaultFeature;
        //                        featureDescriptor.Name = extensionDescriptor.Name;
        //                    }
        //                    else {
        //                        featureDescriptor = new FeatureDescriptor {
        //                            Id = featureDescriptorId,
        //                            Extension = extensionDescriptor
        //                        };
        //                    }
        //                }
        //                else if (IsFeatureFieldDeclaration(line)) {
        //                    if (featureDescriptor != null) {
        //                        string[] featureField = line.Split(new[] { ":" }, 2, StringSplitOptions.None);
        //                        int featureFieldLength = featureField.Length;
        //                        if (featureFieldLength != 2)
        //                            continue;
        //                        for (int i = 0; i < featureFieldLength; i++) {
        //                            featureField[i] = featureField[i].Trim();
        //                        }

        //                        switch (featureField[0].ToLowerInvariant()) {
        //                            case NameSection:
        //                                featureDescriptor.Name = featureField[1];
        //                                break;
        //                            case DescriptionSection:
        //                                featureDescriptor.Description = featureField[1];
        //                                break;
        //                            case CategorySection:
        //                                featureDescriptor.Category = featureField[1];
        //                                break;
        //                            case PrioritySection:
        //                                featureDescriptor.Priority = int.Parse(featureField[1]);
        //                                break;
        //                            case DependenciesSection:
        //                                featureDescriptor.Dependencies = ParseFeatureDependenciesEntry(featureField[1]);
        //                                break;
        //                        }
        //                    }
        //                    else {
        //                        string message = string.Format("The line {0} in manifest for extension {1} was ignored", line, extensionDescriptor.Id);
        //                        throw new ArgumentException(message);
        //                    }
        //                }
        //                else {
        //                    string message = string.Format("The line {0} in manifest for extension {1} was ignored", line, extensionDescriptor.Id);
        //                    throw new ArgumentException(message);
        //                }
        //            }

        //            if (featureDescriptor != null && !featureDescriptor.Equals(defaultFeature))
        //                featureDescriptors.Add(featureDescriptor);
        //        }
        //    }

        //    return featureDescriptors;
        //}

        private static bool IsFeatureFieldDeclaration(string line) {
            if (line.StartsWith("\t\t") ||
                line.StartsWith("\t    ") ||
                line.StartsWith("    ") ||
                line.StartsWith("    \t"))
                return true;

            return false;
        }

        private static bool IsFeatureDeclaration(string line) {
            int lineLength = line.Length;
            if (line.StartsWith("\t") && lineLength >= 2) {
                return !Char.IsWhiteSpace(line[1]);
            }
            if (line.StartsWith("    ") && lineLength >= 5)
                return !Char.IsWhiteSpace(line[4]);

            return false;
        }

        private static IEnumerable<string> ParseFeatureDependenciesEntry(string dependenciesEntry) {
            if (string.IsNullOrEmpty(dependenciesEntry))
                return Enumerable.Empty<string>();

            var dependencies = new List<string>();
            foreach (var s in dependenciesEntry.Split(',')) {
                dependencies.Add(s.Trim());
            }
            return dependencies;
        }

        private static string GetValue(IDictionary<string, string> fields, string key) {
            string value;
            return fields.TryGetValue(key, out value) ? value : null;
        }
    }
}