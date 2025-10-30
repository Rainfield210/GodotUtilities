using GodotUtilities.SourceGenerators.utils.data_model;
using GodotUtilities.SourceGenerators.utils.extensions;
using Microsoft.CodeAnalysis;

namespace GodotUtilities.SourceGenerators.OnInstantiateExtensions;

internal class OnInstantiateDataModel : MemberDataModel {
    public string MethodName { get; }
    public string MethodArgs { get; }
    public string PassedArgs { get; }
    public string ResourcePath { get; }
    public string ConstructorScope { get; }

    public OnInstantiateDataModel(IMethodSymbol method, string ctor, string godotProjectDir = null)
        : base(method) {
        MethodName = method.Name;
        MethodArgs = string.Join(", ", method.Parameters.Select(x => $"{x.Type} {x.Name}"));
        PassedArgs = string.Join(", ", method.Parameters.Select(x => $"{x.Name}"));
        ResourcePath = ConstructResourcePath();
        ConstructorScope = ctor;
        return;

        string ConstructResourcePath() {
            var classPath = method.ContainingType.ClassPath();
            var resourcePath = GD.GetResourcePath(classPath, godotProjectDir);
            resourcePath = Path.ChangeExtension(resourcePath, "tscn");
            var pathParts = resourcePath.Split('/');
            pathParts[^1] = method.ContainingType.Name + Path.GetExtension(resourcePath);
            return string.Join("/", pathParts);
        }
    }
}
