using OwonBinfileReader.Json;
using System.Text;
using System.Text.Json;

namespace OwonBinfileReader.Tests;

[TestClass]
public sealed class ChannelJsonConverterTests
{
    private static readonly JsonSerializerOptions _options = new();

    [TestMethod]
    public void ChannelJsonConverter_Returns_Correct_Results()
    {
        var json = "\"CH1\"";
        var converter = new ChannelJsonConverter();
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
        reader.Read();
        Assert.AreEqual(1, converter.Read(ref reader, typeof(int), _options));
    }
}
