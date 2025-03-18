using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OwonBinfileReader.Json;

internal class OnOffJsonConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => "ON".Equals(reader.GetString(), StringComparison.OrdinalIgnoreCase);

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        => throw new NotImplementedException();
}