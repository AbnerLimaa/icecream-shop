using System.Text.RegularExpressions;

namespace IceCreamShopApi;

public static partial class Extensions
{
    public static string ToSnakeCase(this string value)
    {
        if (string.IsNullOrEmpty(value)) return value;

        var startUnderscores = StartUnderscore().Match(value);
        return startUnderscores + LettersAndNumbers().Replace(value, "$1_$2").ToLower();
    }

    [GeneratedRegex(@"([a-z0-9])([A-Z])")]
    private static partial Regex LettersAndNumbers();
    
    [GeneratedRegex(@"^_+")]
    private static partial Regex StartUnderscore();
}