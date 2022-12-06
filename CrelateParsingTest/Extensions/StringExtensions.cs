namespace CrelateParsingTest.Extensions;

public static class StringExtensions
{
    public static string? NormalizePhoneNumber(this string? value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        // Given more time I would probably use RegEx to do this work with some better pattern matching
        return value.Replace("(", string.Empty).Replace(")", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).Trim();
    }
}
