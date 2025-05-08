using Microsoft.CodeAnalysis.Diagnostics;

namespace GodotUtilities.SourceGenerators.utils.extensions;

internal static class GodotExtensions {
    private static string _cachedProjectDir;

    private static string GetGodotProjectDir(AnalyzerConfigOptions options) {
        const string godotProjectDir = "build_property.GodotProjectDir";
        _cachedProjectDir ??= options.TryGetValue(godotProjectDir, out var value) ? value : null;
        return _cachedProjectDir ??
               throw new InvalidOperationException(
                   "Missing Godot project directory; ensure GodotProjectDir is defined.");
    }

    extension(string sysPath) {
        public string ToResourcePath(AnalyzerConfigOptions options) =>
            $"res://{sysPath.Replace(GetGodotProjectDir(options), string.Empty).Replace(Path.DirectorySeparatorChar, '/').Trim('/')}";

        public string ToScenePath() => Path.ChangeExtension(sysPath, "tscn");
    }
}
