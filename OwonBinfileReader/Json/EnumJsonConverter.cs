using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OwonBinfileReader.Json;

internal class EnumJsonConverter<T> : JsonConverter<T>
    where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => Enum.TryParse<T>(reader.GetString(), true, out var value)
            ? value
            : throw new Exception($"Unknown {typeof(T).Name} value '{value}'");
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        => throw new NotImplementedException();
}
