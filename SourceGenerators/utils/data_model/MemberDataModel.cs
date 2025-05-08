using Microsoft.CodeAnalysis;

namespace GodotUtilities.SourceGenerators.utils.data_model;

internal abstract class MemberDataModel : BaseDataModel {
    protected MemberDataModel(ISymbol symbol)
        : base(symbol, symbol.ContainingType) {
    }
}
