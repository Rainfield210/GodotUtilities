using Microsoft.CodeAnalysis;

namespace GodotUtilities.SourceGenerators.utils.data_model;

internal abstract class MemberDataModel(ISymbol symbol) : BaseDataModel(symbol.ContainingType);
