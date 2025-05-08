using GodotUtilities.SourceGenerators.utils.data_model;
using GodotUtilities.SourceGenerators.utils.extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace GodotUtilities.SourceGenerators.SceneWithInstantiateExtensions;

internal class SceneWithInstantiateDataModel(INamedTypeSymbol symbol, AnalyzerConfigOptions options)
    : ClassDataModel(symbol) {
    public string ResourcePath { get; } = symbol.ClassPath().ToResourcePath(options).ToScenePath();
}
