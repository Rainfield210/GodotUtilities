using Microsoft.CodeAnalysis;

namespace GodotUtilities.SourceGenerators.utils.data_model;

internal abstract class ClassDataModel(INamedTypeSymbol symbol) : BaseDataModel(symbol);
