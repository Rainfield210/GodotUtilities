using Microsoft.CodeAnalysis;

namespace GodotUtilities.SourceGenerators.utils.extensions;

public static class SymbolExtensions {
    extension(INamedTypeSymbol symbol) {
        public string ClassDef() => symbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);

        public string ClassPath() => symbol.DeclaringSyntaxReferences.FirstOrDefault()?.SyntaxTree?.FilePath;
    }
}
