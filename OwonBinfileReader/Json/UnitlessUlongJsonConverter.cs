using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OwonBinfileReader.Json;

internal class UnitlessUlongJsonConverter : JsonConverter<ulong>
{
    public override ulong Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => (ulong)SIHelper.GetNormalizedValue(reader.GetString(), string.Empty);
    public override void Write(Utf8JsonWriter writer, ulong value, JsonSerializerOptions options)
        => throw new NotImplementedException();
}