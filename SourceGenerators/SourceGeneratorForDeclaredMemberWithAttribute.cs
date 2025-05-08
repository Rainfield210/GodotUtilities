using System.Collections.Immutable;
using System.Reflection;
using System.Text.RegularExpressions;
using GodotUtilities.SourceGenerators.utils.extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Scriban;
using GeneratorContext = Microsoft.CodeAnalysis.IncrementalGeneratorInitializationContext;

namespace GodotUtilities.SourceGenerators;

public abstract class
    SourceGeneratorForDeclaredMemberWithAttribute<TAttribute, TDeclarationSyntax> : IIncrementalGenerator
    where TAttribute : Attribute where TDeclarationSyntax : MemberDeclarationSyntax {
    private const string Ext = ".generated.cs";
    private const string TemplateExt = ".sbncs";
    private static readonly string AttributeType = typeof(TAttribute).Name;
    private static readonly string
        AttributeName = Regex.Replace(AttributeType, "Attribute$", "", RegexOptions.Compiled);
    internal static readonly Template Template = Template.Parse(Assembly.GetExecutingAssembly()
                                                                        .GetEmbeddedResource(
                                                                            $"{typeof(TAttribute).Namespace}.{AttributeName}Template{TemplateExt}"));

    public void Initialize(GeneratorContext context) {
        var syntaxProvider = context.SyntaxProvider.CreateSyntaxProvider(IsSyntaxTarget, GetSyntaxTarget);
        var compilationProvider = context.CompilationProvider.Combine(syntaxProvider.Collect())
                                         .Combine(context.AnalyzerConfigOptionsProvider);
        context.RegisterImplementationSourceOutput(compilationProvider,
                                                   (sourceProductionContext, provider) =>
                                                       OnExecute(sourceProductionContext, provider.Left.Left,
                                                                 provider.Left.Right, provider.Right));
        return;

        static bool IsSyntaxTarget(SyntaxNode node, CancellationToken _) =>
            node is TDeclarationSyntax { AttributeLists.Count: not 0 } type && type.AttributeLists
                .SelectMany(attributeList => attributeList.Attributes)
                .Any(attribute => attribute.Name.ToString() == AttributeName);

        static TDeclarationSyntax GetSyntaxTarget(GeneratorSyntaxContext context, CancellationToken _) =>
            (TDeclarationSyntax)context.Node;

        void OnExecute(SourceProductionContext sourceProductionContext, Compilation compilation,
                       ImmutableArray<TDeclarationSyntax> nodes, AnalyzerConfigOptionsProvider options) {
            foreach (var node in nodes.Distinct()) {
                if (sourceProductionContext.CancellationToken.IsCancellationRequested) {
                    return;
                }
                var model = compilation.GetSemanticModel(node.SyntaxTree);
                var symbol = model.GetDeclaredSymbol(Node(node));
                var attribute = symbol.GetAttributes().SingleOrDefault(x => x.AttributeClass.Name == AttributeType);
                if (attribute is null) {
                    continue;
                }
                var hintName =
                    $"{symbol.ContainingNamespace?.ToDisplayString() ?? "Global"}.{symbol.ContainingType?.Name ?? symbol.Name}_{AttributeName}{Ext}";
                sourceProductionContext.AddSource(
                    hintName, GenerateCode(compilation, node, symbol, attribute, options.GlobalOptions));
            }
        }
    }

    protected abstract string GenerateCode(Compilation compilation, SyntaxNode node, ISymbol symbol,
                                           AttributeData attribute, AnalyzerConfigOptions options);

    protected virtual SyntaxNode Node(TDeclarationSyntax node) => node;
}
