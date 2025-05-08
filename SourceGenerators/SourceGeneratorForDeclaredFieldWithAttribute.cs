using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace GodotUtilities.SourceGenerators;

public abstract class
    SourceGeneratorForDeclaredFieldWithAttribute<TAttribute> : SourceGeneratorForDeclaredMemberWithAttribute<TAttribute,
    FieldDeclarationSyntax> where TAttribute : Attribute {
    protected abstract string GenerateCode(Compilation compilation, SyntaxNode node, IFieldSymbol symbol,
                                           AttributeData attribute, AnalyzerConfigOptions options);

    protected sealed override string GenerateCode(Compilation compilation, SyntaxNode node, ISymbol symbol,
                                                  AttributeData attribute, AnalyzerConfigOptions options) =>
        GenerateCode(compilation, node, (IFieldSymbol)symbol, attribute, options);

    protected override SyntaxNode Node(FieldDeclarationSyntax node) => node.Declaration.Variables.Single();
}
