using GodotUtilities.SourceGenerators.utils.data_model;
using GodotUtilities.SourceGenerators.utils.extensions;
using Microsoft.CodeAnalysis;

namespace GodotUtilities.SourceGenerators.SceneWithInstantiateExtensions;

internal class SceneWithInstantiateDataModel : ClassDataModel {
    public string ResourcePath { get; }

    public SceneWithInstantiateDataModel(INamedTypeSymbol symbol) : base(symbol) {
        ResourcePath = ConstructResourcePath();
        return;

        string ConstructResourcePath() {
            var classPath = symbol.ClassPath();
            var resourcePath = GD.GetResourcePath(classPath);
            resourcePath = Path.ChangeExtension(resourcePath, "tscn");
            var snakeCaseClassName = ClassName.ToSnakeCase();
            var pathParts = resourcePath.Split('/');
            pathParts[^1] = snakeCaseClassName + Path.GetExtension(resourcePath);
            return string.Join("/", pathParts);
        }
    }
}
