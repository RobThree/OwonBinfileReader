using OwonBinfileReader.Json;
using System.Text;
using System.Text.Json;

namespace OwonBinfileReader.Tests;

[TestClass]
public sealed class EnumJsonConverterTests
{
    private static readonly JsonSerializerOptions _options = new();

    [TestMethod]
    public void EnumJsonConverter_Returns_Correct_Results()
    {
        var converter = new EnumJsonConverter<Coupling>();
        var testvalues = new Dictionary<string, Coupling>
        {
            {"AC", Coupling.AC },
            {"DC", Coupling.DC },
            {"GND", Coupling.GND },
            {"Ac", Coupling.AC },
            {"Dc", Coupling.DC },
            {"Gnd", Coupling.GND }
        };

        foreach (var tv in testvalues)
        {
            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes($"\"{tv.Key}\""));
            reader.Read();
            Assert.AreEqual(tv.Value, converter.Read(ref reader, typeof(Coupling), _options));
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void EnumJsonConverter_Throws_On_Unknown_Value()
    {
        var converter = new EnumJsonConverter<TriggerType>();
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes($"\"FOO\""));
        reader.Read();
        converter.Read(ref reader, typeof(TriggerType), _options);
    }
}