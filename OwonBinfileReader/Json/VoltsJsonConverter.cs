using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OwonBinfileReader.Json;

internal class VoltsJsonConverter : JsonConverter<double>
{
    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => SIHelper.GetNormalizedValue(reader.GetString(), "V");
    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        => throw new NotImplementedException();
}