using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace OwonBinfileReader.Json;

internal class ProbeJsonConverter : JsonConverter<int>
{
    private static readonly Regex _proberegex = new("(\\d+)X", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var probe = reader.GetString();
        var m = _proberegex.Match(probe);
        return m.Success ? int.Parse(m.Groups[1].Value) : throw new Exception($"Unable to parse probe '{probe}'");
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        => throw new NotImplementedException();
}