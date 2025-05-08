using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace GodotUtilities.SourceGenerators.OnInstantiateExtensions;

[Generator]
internal class OnInstantiateGenerator : SourceGeneratorForDeclaredMethodWithAttribute<OnInstantiateAttribute> {
    protected override string GenerateCode(Compilation compilation, SyntaxNode node, IMethodSymbol symbol,
                                           AttributeData attribute, AnalyzerConfigOptions options) =>
        Template.Render(new OnInstantiateDataModel(symbol, options), member => member.Name);
}
