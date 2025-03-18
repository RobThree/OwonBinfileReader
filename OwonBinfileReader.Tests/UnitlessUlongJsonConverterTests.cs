using OwonBinfileReader.Json;
using System.Text;
using System.Text.Json;

namespace OwonBinfileReader.Tests;

[TestClass]
public sealed class UnitlessUlongJsonConverterTests
{
    private static readonly JsonSerializerOptions _options = new();

    [TestMethod]
    public void UnitlessJsonConverter_Returns_Correct_Results()
    {
        var json = "\"10M\"";
        var converter = new UnitlessUlongJsonConverter();
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
        reader.Read();
        Assert.AreEqual(10000000UL, converter.Read(ref reader, typeof(ulong), _options));
    }
}
