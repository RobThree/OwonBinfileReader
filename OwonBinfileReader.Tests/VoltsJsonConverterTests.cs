using OwonBinfileReader.Json;
using System.Text;
using System.Text.Json;

namespace OwonBinfileReader.Tests;

[TestClass]
public sealed class VoltsJsonConverterTests
{
    private static readonly JsonSerializerOptions _options = new();

    [TestMethod]
    public void VoltsJsonConverter_Returns_Correct_Results()
    {
        var json = "\"123mV\"";
        var converter = new VoltsJsonConverter();
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
        reader.Read();
        Assert.AreEqual(0.123, converter.Read(ref reader, typeof(double), _options));
    }
}
