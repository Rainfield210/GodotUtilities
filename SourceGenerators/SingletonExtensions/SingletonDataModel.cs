using GodotUtilities.SourceGenerators.utils.data_model;
using Microsoft.CodeAnalysis;

namespace GodotUtilities.SourceGenerators.SingletonExtensions;

internal class SingletonDataModel(INamedTypeSymbol symbol) : ClassDataModel(symbol);
