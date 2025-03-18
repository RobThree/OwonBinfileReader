using System.Text.Json.Serialization;

namespace OwonBinfileReader;

public record MetaData
(
    [property: JsonPropertyName("TIMEBASE")]
    Timebase Timebase,

    [property: JsonPropertyName("SAMPLE")]
    Sample Sample,

    [property: JsonPropertyName("CHANNEL")]
    Channel[] Channels,

    [property: JsonPropertyName("DATATYPE")]
    string Datatype,

    [property: JsonPropertyName("RUNSTATUS")]
    string Runstatus,

    [property: JsonPropertyName("IDN")]
    string IDN,

    [property: JsonPropertyName("MODEL")]
    string Model,

    [property: JsonPropertyName("Trig")]
    Trig? Trig,

    [property: JsonPropertyName("IsSkipIfClosed")]
    bool? IsSkipIfClosed
);
