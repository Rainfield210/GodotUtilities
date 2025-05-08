using System.Reflection;
using GodotUtilities.SourceGenerators.utils.extensions;

namespace GodotUtilities.SourceGenerators.SingletonExtensions;

internal static class Resources {
    private const string SingletonTemplateName =
        "GodotUtilities.SourceGenerators.SingletonExtensions.SingletonTemplate.sbncs";
    public static readonly string SingletonTemplate =
        Assembly.GetExecutingAssembly().GetEmbeddedResource(SingletonTemplateName);
}
