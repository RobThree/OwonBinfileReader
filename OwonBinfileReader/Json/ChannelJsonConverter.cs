using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace OwonBinfileReader.Json;

internal class ChannelJsonConverter : JsonConverter<int>
{
    private static readonly Regex _channelregex = new("CH(\\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var channel = reader.GetString();
        var m = _channelregex.Match(channel);
        return m.Success ? int.Parse(m.Groups[1].Value) : throw new Exception($"Unable to parse channel '{channel}'");
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        => throw new NotImplementedException();
}
