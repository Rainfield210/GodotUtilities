using GodotUtilities.SourceGenerators.utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Scriban;

namespace GodotUtilities.SourceGenerators.SingletonExtensions;

[Generator]
internal class SingletonSourceGenerator : SourceGeneratorForDeclaredTypeWithAttribute<SingletonAttribute> {
    private static Template _singletonTemplate;
    private static Template SingletonTemplate =>
        _singletonTemplate ??= Template.Parse(Resources.SingletonTemplate);

    protected override (string GeneratedCode, DiagnosticDetail Error) GenerateCode(Compilation compilation,
        SyntaxNode node, INamedTypeSymbol symbol, AttributeData attribute, AnalyzerConfigOptions options) {
        var model = new SingletonDataModel(symbol);
        Log.Debug($"--- MODEL ---\n{model}\n");

        var output = SingletonTemplate.Render(model, member => member.Name);
        Log.Debug($"--- OUTPUT ---\n{output}<END>\n");

        return (output, null);
    }
}
