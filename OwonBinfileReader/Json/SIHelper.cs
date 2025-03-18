using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Text.RegularExpressions;

internal static class SIHelper
{
    private static readonly CultureInfo _culture = CultureInfo.InvariantCulture;
    private static readonly ConcurrentDictionary<string, Regex> _regexes = new();

    private static Regex GetRegex(string unit)
        => _regexes.GetOrAdd(unit, u => new Regex($@"(?<value>\-?\d+(\.\d+)?)\s?(?<prefix>(P|T|G|M|k|K|h|da|d|c|m|u|µ|n|p|f)?{Regex.Escape(unit)})", RegexOptions.Compiled));

    public static double GetNormalizedValue(string? value, string unit)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            var m = GetRegex(unit).Match(value);
            if (m.Success)
            {
                var numericvalue = m.Groups["value"].Value;
                var siprefix = m.Groups["prefix"].Value[..^unit.Length];
                if (double.TryParse(numericvalue, NumberStyles.Number, _culture, out var result))
                {
                    return siprefix switch
                    {
                        "f" => result * 1e-15d,
                        "p" => result * 1e-12d,
                        "n" => result * 1e-9d,
                        "u" => result * 1e-6d,
                        "µ" => result * 1e-6d,
                        "m" => result * 1e-3d,
                        "c" => result * 1e-2d,
                        "d" => result * 1e-1d,
                        "" => result,
                        "da" => result * 1e1d,
                        "h" => result * 1e2d,
                        "k" => result * 1e3d,
                        "K" => result * 1e3d,
                        "M" => result * 1e6d,
                        "G" => result * 1e9d,
                        "T" => result * 1e12d,
                        "P" => result * 1e15d,
                        _ => throw new ArgumentException($"Unknown SI prefix '{siprefix}'")
                    };
                }
            }
        }
        throw new ArgumentException($"Unable to parse SI value '{value}'");
    }
}