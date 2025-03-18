using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OwonBinfileReader.Json;

internal class TimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => TimeSpan.FromSeconds((double)SIHelper.GetNormalizedValue(reader.GetString(), "s"));

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        => throw new NotImplementedException();
}