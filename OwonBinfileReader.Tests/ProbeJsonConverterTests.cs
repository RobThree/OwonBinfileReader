using OwonBinfileReader.Json;
using System.Text;
using System.Text.Json;

namespace OwonBinfileReader.Tests;

[TestClass]
public sealed class ProbeJsonConverterTests
{
    private static readonly JsonSerializerOptions _options = new();

    [TestMethod]
    public void ProbeJsonConverter_Returns_Correct_Results()
    {
        var json = "\"10X\"";
        var converter = new ProbeJsonConverter();
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
        reader.Read();
        Assert.AreEqual(10, converter.Read(ref reader, typeof(int), _options));
    }
}
