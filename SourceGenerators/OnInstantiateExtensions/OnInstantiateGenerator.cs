using GodotUtilities.SourceGenerators.utils.extensions;
using GodotUtilities.SourceGenerators.utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Scriban;

namespace GodotUtilities.SourceGenerators.OnInstantiateExtensions;

[Generator]
internal class
    OnInstantiateGenerator : SourceGeneratorForDeclaredMethodWithAttribute<OnInstantiateAttribute> {
    private static Template _onInstantiateTemplate;
    private static Template OnInstantiateTemplate =>
        _onInstantiateTemplate ??= Template.Parse(Resources.OnInstantiateTemplate);

    protected override (string GeneratedCode, DiagnosticDetail Error) GenerateCode(Compilation compilation,
        SyntaxNode node, IMethodSymbol symbol, AttributeData attribute, AnalyzerConfigOptions options) {
        var model = new OnInstantiateDataModel(symbol, ReconstructAttribute().ConstructorScope,
            options.TryGetGodotProjectDir());
        Log.Debug($"--- MODEL ---\n{model}\n");

        var output = OnInstantiateTemplate.Render(model, member => member.Name);
        Log.Debug($"--- OUTPUT ---\n{output}<END>\n");

        return (output, null);

        OnInstantiateAttribute ReconstructAttribute()
            => new((string)attribute.ConstructorArguments[0].Value);
    }
}
