using System.Reflection;
using GodotUtilities.SourceGenerators.utils.extensions;

namespace GodotUtilities.SourceGenerators.SceneWithInstantiateExtensions;

internal static class Resources {
    private const string SceneWithInstantiateTemplateName =
        "GodotUtilities.SourceGenerators.SceneWithInstantiateExtensions.SceneWithInstantiateTemplate.sbncs";
    public static readonly string SceneWithInstantiateTemplate =
        Assembly.GetExecutingAssembly().GetEmbeddedResource(SceneWithInstantiateTemplateName);
}
