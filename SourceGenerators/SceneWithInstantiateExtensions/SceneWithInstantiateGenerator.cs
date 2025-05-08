using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace GodotUtilities.SourceGenerators.SceneWithInstantiateExtensions;

[Generator]
internal class
    SceneWithInstantiateGenerator : SourceGeneratorForDeclaredTypeWithAttribute<SceneWithInstantiateAttribute> {
    protected override string GenerateCode(Compilation compilation, SyntaxNode node, INamedTypeSymbol symbol,
                                           AttributeData attribute, AnalyzerConfigOptions options) =>
        Template.Render(new SceneWithInstantiateDataModel(symbol, options), member => member.Name);
}
