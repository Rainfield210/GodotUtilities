namespace GodotUtilities.SourceGenerators {
    public record DiagnosticDetail {
        public string ID { get; init; }
        public string Category { get; init; }
        public string Title { get; init; }
        public string Message { get; init; }
    }
}
