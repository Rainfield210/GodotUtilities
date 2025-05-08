using GodotUtilities.SourceGenerators.utils.extensions;
using Microsoft.CodeAnalysis;

namespace GodotUtilities.SourceGenerators.utils.data_model;

internal abstract class BaseDataModel(INamedTypeSymbol @class) {
    public string Namespace { get; } = @class.ContainingNamespace.IsGlobalNamespace
        ? null
        : string.Join(".", @class.ContainingNamespace.ConstituentNamespaces);
    public string ClassName { get; } = @class.ClassDef();
}
