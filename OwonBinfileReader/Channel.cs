using OwonBinfileReader.Json;
using System.Text.Json.Serialization;

namespace OwonBinfileReader;

public record Channel
(
    [property: JsonPropertyName("NAME")]
    [property: JsonConverter(typeof(ChannelJsonConverter))]
    int Number,

    [property: JsonPropertyName("DISPLAY")]
    [property: JsonConverter(typeof(OnOffJsonConverter))]
    bool Display,

    [property: JsonPropertyName("Current_Rate")]
    double CurrentRate,

    [property: JsonPropertyName("Current_Ratio")]
    double CurrentRatio,

    [property: JsonPropertyName("Measure_Current_Switch")]
    [property: JsonConverter(typeof(OnOffJsonConverter))]
    bool MeasureCurrentSwitch,

    [property: JsonPropertyName("COUPLING")]
    [property: JsonConverter(typeof(EnumJsonConverter<Coupling>))]
    Coupling Coupling,

    [property: JsonPropertyName("PROBE")]
    [property: JsonConverter(typeof(ProbeJsonConverter))]
    int Probe,

    [property: JsonPropertyName("SCALE")]
    [property: JsonConverter(typeof(VoltsJsonConverter))]
    double Scale,

    [property: JsonPropertyName("OFFSET")]
    double Offset,
    [property: JsonPropertyName("FREQUENCE")]
    double Frequency,

    [property: JsonPropertyName("INVERSE")]
    [property: JsonConverter(typeof(OnOffJsonConverter))]
    bool Inverse
);