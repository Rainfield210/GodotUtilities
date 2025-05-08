using GodotUtilities.SourceGenerators.utils.data_model;
using GodotUtilities.SourceGenerators.utils.extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace GodotUtilities.SourceGenerators.OnInstantiateExtensions;

internal class OnInstantiateDataModel : MemberDataModel {
    private static readonly SymbolDisplayFormat ParameterFormat = new(
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
        parameterOptions: SymbolDisplayParameterOptions.IncludeParamsRefOut |
                          SymbolDisplayParameterOptions.IncludeType | SymbolDisplayParameterOptions.IncludeName |
                          SymbolDisplayParameterOptions.IncludeDefaultValue,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes);

    public string MethodName { get; }
    public string MethodSignature { get; }
    public string MethodCallArgs { get; }
    public string ResourcePath { get; }

    public OnInstantiateDataModel(IMethodSymbol method, AnalyzerConfigOptions options) : base(method) {
        MethodName = method.Name;
        var parameters = method.Parameters;
        MethodSignature = string.Join(", ", parameters.Select(p => p.ToDisplayString(ParameterFormat)));
        MethodCallArgs = string.Join(", ", parameters.Select(FormatArgumentPassing));
        ResourcePath = method.ContainingType.ClassPath().ToResourcePath(options).ToScenePath();
    }

    private static string FormatArgumentPassing(IParameterSymbol parameter) {
        var prefix = parameter.RefKind switch {
            RefKind.Ref => "ref ",
            RefKind.Out => "out ",
            RefKind.In => "in ",
            _ => string.Empty
        };
        return prefix + parameter.Name;
    }
}
