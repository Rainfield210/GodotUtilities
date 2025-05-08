using GodotUtilities.SourceGenerators.utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Scriban;

namespace GodotUtilities.SourceGenerators.SceneWithInstantiateExtensions;

[Generator]
internal class
    SceneWithInstantiateGenerator : SourceGeneratorForDeclaredTypeWithAttribute<SceneWithInstantiateAttribute> {
    private static Template _sceneWithInstantiateTemplate;
    private static Template SceneWithInstantiateTemplate =>
        _sceneWithInstantiateTemplate ??= Template.Parse(Resources.SceneWithInstantiateTemplate);

    protected override (string GeneratedCode, DiagnosticDetail Error) GenerateCode(Compilation compilation,
        SyntaxNode node, INamedTypeSymbol symbol, AttributeData attribute, AnalyzerConfigOptions options) {
        var model = new SceneWithInstantiateDataModel(symbol);
        Log.Debug($"--- MODEL ---\n{model}\n");

        var output = SceneWithInstantiateTemplate.Render(model, member => member.Name);
        Log.Debug($"--- OUTPUT ---\n{output}<END>\n");

        return (output, null);
    }
}
