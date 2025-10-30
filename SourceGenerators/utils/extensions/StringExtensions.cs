using System.Globalization;
using System.Text.RegularExpressions;

namespace GodotUtilities.SourceGenerators.utils.extensions;

internal static class StringExtensions {
    private const string SplitRegexStr = "[ _-]+|(?<=[a-z])(?=[A-Z])";

    private const string UnsafeCharsRegexStr = @"[^\w]+";

    private const string UnsafeFirstCharRegexStr = "^[^a-zA-Z_]+";
    private static readonly Regex SplitRegex = new(SplitRegexStr, RegexOptions.Compiled | RegexOptions.ExplicitCapture);
    private static readonly Regex UnsafeCharsRegex =
        new(UnsafeCharsRegexStr, RegexOptions.Compiled | RegexOptions.ExplicitCapture);
    private static readonly Regex UnsafeFirstCharRegex =
        new(UnsafeFirstCharRegexStr, RegexOptions.Compiled | RegexOptions.ExplicitCapture);

    public static readonly char DirSeparator = Path.DirectorySeparatorChar;
    public static readonly string DirSeparatorStr = Path.DirectorySeparatorChar.ToString();

    public static string ToTitleCase(this string source) =>
        CultureInfo.CurrentCulture.TextInfo.ToTitleCase(SplitRegex.Replace(source, " ").ToLower());

    public static string ToSafeName(this string source) {
        source = source.ToTitleCase().Replace(" ", "");
        source = UnsafeCharsRegex.Replace(source, "_");
        return UnsafeFirstCharRegex.IsMatch(source) ? $"_{source}" : source;
    }

    public static string Truncate(this string source, int maxChars) =>
        source.Length <= maxChars ? source : source[..maxChars];

    public static string TrimDirSeparator(this string source) => source.TrimEnd(DirSeparator);
}
