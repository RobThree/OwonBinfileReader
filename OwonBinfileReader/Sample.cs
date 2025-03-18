using OwonBinfileReader.Json;
using System.Text.Json.Serialization;

namespace OwonBinfileReader;

public record Sample
(
    [property: JsonPropertyName("FULLSCREEN")]
    int Fullscreen,

    [property: JsonPropertyName("SLOWMOVE")]
    int SlowMove,

    [property: JsonPropertyName("DATALEN")]
    int DataLength,

    [property: JsonPropertyName("SAMPLERATE")]
    string SampleRate,

    [property: JsonPropertyName("TYPE")]
    string Type,

    [property: JsonPropertyName("DEPMEM")]
    [property: JsonConverter(typeof(UnitlessUlongJsonConverter))]
    ulong MemoryDepth,

    [property: JsonPropertyName("SCREENOFFSET")]
    int ScreenOffset

);
