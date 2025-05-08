using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace GodotUtilities.SourceGenerators;

public abstract class
    SourceGeneratorForDeclaredMethodWithAttribute<TAttribute> : SourceGeneratorForDeclaredMemberWithAttribute<TAttribute
    , MethodDeclarationSyntax> where TAttribute : Attribute {
    protected abstract string GenerateCode(Compilation compilation, SyntaxNode node, IMethodSymbol symbol,
                                           AttributeData attribute, AnalyzerConfigOptions options);

    protected sealed override string GenerateCode(Compilation compilation, SyntaxNode node, ISymbol symbol,
                                                  AttributeData attribute, AnalyzerConfigOptions options) =>
        GenerateCode(compilation, node, (IMethodSymbol)symbol, attribute, options);
}
