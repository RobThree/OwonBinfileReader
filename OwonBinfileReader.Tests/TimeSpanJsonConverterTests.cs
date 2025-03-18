using OwonBinfileReader.Json;
using System.Text;
using System.Text.Json;

namespace OwonBinfileReader.Tests;

[TestClass]
public sealed class TimeSpanJsonConverterTests
{
    private static readonly JsonSerializerOptions _options = new();

    [TestMethod]
    public void TimeSpanJsonConverter_Returns_Correct_Results()
    {
        var json = "\"123ms\"";
        var converter = new TimeSpanJsonConverter();
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
        reader.Read();
        Assert.AreEqual(TimeSpan.FromMilliseconds(123), converter.Read(ref reader, typeof(TimeSpan), _options));
    }
}
