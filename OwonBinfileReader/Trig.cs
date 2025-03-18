using OwonBinfileReader.Json;
using System.Text.Json.Serialization;

namespace OwonBinfileReader;

public record Trig
(
    [property: JsonPropertyName("Mode")]
    [property: JsonConverter(typeof(EnumJsonConverter<TriggerMode>))]
    TriggerMode Mode,

    [property: JsonPropertyName("Type")]
    [property: JsonConverter(typeof(EnumJsonConverter<TriggerType>))]
    TriggerType Type,

    [property: JsonPropertyName("Items")]
    TrigItem Items,

    [property: JsonPropertyName("Sweep")]
    string Sweep
);
