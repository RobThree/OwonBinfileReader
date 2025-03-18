using OwonBinfileReader.Json;
using System;
using System.Text.Json.Serialization;

namespace OwonBinfileReader;

public record Timebase
(
    [property: JsonPropertyName("SCALE")]
    [property: JsonConverter(typeof(TimeSpanJsonConverter))]
    TimeSpan Scale,

    [property: JsonPropertyName("HOFFSET")]
    double HorizontalOffset
);
