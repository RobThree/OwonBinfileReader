using System.Text.Json;

namespace OwonBinfileReader.Tests;

[TestClass]
public sealed class BinfileReaderTests
{
    [TestMethod]
    public async Task BinfileReader_Throws_On_NonExisting() => await Assert.ThrowsExactlyAsync<FileNotFoundException>(async () => await new BinfileReader().ReadAsync("testfiles/nonexisting.bin"));

    [TestMethod]
    public async Task BinfileReader_Throws_When_Not_At_Beginning()
    {
        using var fs = File.OpenRead("testfiles/empty.bin");
        fs.Seek(1, SeekOrigin.Begin);
        await Assert.ThrowsExactlyAsync<BinfileStreamReaderException>(async () => await new BinfileReader().ReadAsync(fs));
    }

    [TestMethod]
    public async Task BinfileReader_Throws_On_InvalidHeader()
    {
        using var fs = File.OpenRead("testfiles/invalidheader.bin");
        await Assert.ThrowsExactlyAsync<BinfileStreamReaderException>(async () => await new BinfileReader().ReadAsync(fs));
    }

    [TestMethod]
    public async Task BinfileReader_Throws_On_InvalidJsonSize()
    {
        using var fs = File.OpenRead("testfiles/invalidjsonsize.bin");
        await Assert.ThrowsExactlyAsync<MalformedRecordException>(async () => await new BinfileReader().ReadAsync(fs));
    }

    [TestMethod]
    public async Task BinfileReader_Throws_On_InvalidJson()
    {
        using var fs = File.OpenRead("testfiles/invalidjson.bin");
        await Assert.ThrowsExactlyAsync<JsonException>(async () => await new BinfileReader().ReadAsync(fs));
    }

    [TestMethod]
    public async Task BinfileReader_Throws_On_NullJson()
    {
        using var fs = File.OpenRead("testfiles/nulljson.bin");
        await Assert.ThrowsExactlyAsync<BinfileStreamReaderException>(async () => await new BinfileReader().ReadAsync(fs));
    }

    // TODO: Add more tests (especially for reading the actual measurements (ReadValuesAsync method))
}
