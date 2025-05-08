using System.Reflection;
using GodotUtilities.SourceGenerators.utils.extensions;

namespace GodotUtilities.SourceGenerators.OnInstantiateExtensions;

internal static class Resources {
    private const string OnInstantiateTemplateName =
        "GodotUtilities.SourceGenerators.OnInstantiateExtensions.OnInstantiateTemplate.sbncs";
    public static readonly string OnInstantiateTemplate =
        Assembly.GetExecutingAssembly().GetEmbeddedResource(OnInstantiateTemplateName);
}