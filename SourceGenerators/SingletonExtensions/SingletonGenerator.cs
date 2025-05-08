using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace GodotUtilities.SourceGenerators.SingletonExtensions;

[Generator]
internal class SingletonSourceGenerator : SourceGeneratorForDeclaredTypeWithAttribute<SingletonAttribute> {
    protected override string GenerateCode(Compilation compilation, SyntaxNode node, INamedTypeSymbol symbol,
                                           AttributeData attribute, AnalyzerConfigOptions options) =>
        Template.Render(new SingletonDataModel(symbol), member => member.Name);
}
