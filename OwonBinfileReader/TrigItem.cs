using OwonBinfileReader.Json;
using System;
using System.Text.Json.Serialization;

namespace OwonBinfileReader;

public record TrigItem
(
    [property: JsonPropertyName("Channel")]
    [property: JsonConverter(typeof(ChannelJsonConverter))]
    int Channel,

    [property: JsonPropertyName("Level")]
    [property: JsonConverter(typeof(VoltsJsonConverter))]
    double Level,

    [property: JsonPropertyName("Edge")]
    [property: JsonConverter(typeof(EnumJsonConverter<TriggerSlope>))]
    TriggerSlope Edge,

    [property: JsonPropertyName("Coupling")]
    [property: JsonConverter(typeof(EnumJsonConverter<Coupling>))]
    Coupling Coupling,

    [property: JsonPropertyName("HoldOff")]
    [property: JsonConverter(typeof(TimeSpanJsonConverter))]
    TimeSpan HoldOff
);
