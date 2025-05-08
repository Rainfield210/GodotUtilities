using GodotUtilities.SourceGenerators.utils.extensions;
using Microsoft.CodeAnalysis;

namespace GodotUtilities.SourceGenerators.utils.data_model;

internal abstract class BaseDataModel {
    public string NSOpen { get; }
    public string NSClose { get; }
    public string NSIndent { get; }
    public string ClassName { get; }

    protected BaseDataModel(ISymbol symbol, INamedTypeSymbol @class) {
        ClassName = @class.ClassDef();
        (NSOpen, NSClose, NSIndent) = symbol.GetNamespaceDeclaration();
    }
}
