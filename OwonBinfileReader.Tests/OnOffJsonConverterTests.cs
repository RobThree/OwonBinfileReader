using OwonBinfileReader.Json;
using System.Text;
using System.Text.Json;

namespace OwonBinfileReader.Tests;

[TestClass]
public sealed class OnOffJsonConverterTests
{
    private static readonly JsonSerializerOptions _options = new();

    [TestMethod]
    public void OnOffJsonConverter_Returns_Correct_Result()
    {
        var converter = new OnOffJsonConverter();

        var testvalues = new Dictionary<string, bool>
        {
            {"ON", true },
            {"on", true },
            {"On", true },
            {"OFF", false },
            {"off", false },
            {"Off", false },
            {"XYZ", false },
        };

        foreach (var tv in testvalues)
        {
            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes($"\"{tv.Key}\""));
            reader.Read();
            Assert.AreEqual(tv.Value, converter.Read(ref reader, typeof(bool), _options));
        }
    }
}
