using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace OwonBinfileReader;

public class BinfileReader(Encoding? jsonencoding = null)
{
    private readonly Encoding _jsonencoding = jsonencoding ?? Encoding.UTF8;

    public async Task<Binfile> ReadAsync(string path, CancellationToken cancellationToken = default)
    {
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        return await ReadAsync(stream, cancellationToken);
    }

    public async Task<Binfile> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        if (stream.Position != 0)
        {
            throw new BinfileStreamReaderException("Not at start of the stream.");
        }

        var header = await ReadHeaderAsync(stream, cancellationToken);
        if (header.MagicHeaderBytes != HeaderRecord.ExpectedMagicHeader)
        {
            throw new BinfileStreamReaderException("Invalid magic header bytes.");
        }

        var jsonbuffer = new byte[header.HeaderSize];
        var bytesread = await stream.ReadAsync(jsonbuffer, 0, jsonbuffer.Length, cancellationToken);
        if (bytesread != jsonbuffer.Length)
        {
            throw new MalformedRecordException(typeof(MetaData), stream.Position, jsonbuffer.Length, bytesread);
        }

        var metadata = JsonSerializer.Deserialize<MetaData>(_jsonencoding.GetString(jsonbuffer))
            ?? throw new BinfileStreamReaderException("Unable to deserialize JSON contained in bin file.");

        var measurements = new Dictionary<int, double[]>();
        foreach (var c in metadata.Channels.Where(c => c.Display))
        {
            measurements.Add(c.Number, await ReadValuesAsync(stream, c.CurrentRatio / c.CurrentRate, metadata.Sample.DataLength, cancellationToken).ToArrayAsync(cancellationToken));
        }
        return new Binfile(
            metadata,
            new Measurements(measurements)
        );
    }

    private static async IAsyncEnumerable<double> ReadValuesAsync(Stream stream, double scale, int expectedDataLength, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var buffer = new byte[4];
        var bytesread = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
        if (bytesread != buffer.Length)
        {
            throw new MalformedRecordException(typeof(IEnumerable<double>), stream.Position, buffer.Length, bytesread);
        }
        var datalength = BitConverter.ToInt32(buffer, 0);
        if (expectedDataLength * 2 != datalength)  // 16 bits per sample
        {
            throw new BinfileStreamReaderException("Data length mismatch.");
        }

        buffer = new byte[datalength];
        var pos = 0;
        while (pos < datalength & !cancellationToken.IsCancellationRequested)
        {
            bytesread = await stream.ReadAsync(buffer, pos, datalength - pos, cancellationToken);
            if (bytesread == 0)
            {
                throw new IOException("Unexpected end of stream.");
            }
            pos += bytesread;
        }

        pos = 0;
        while (pos < datalength & !cancellationToken.IsCancellationRequested)
        {
            yield return BitConverter.ToInt16(buffer, pos) * scale;
            pos += 2;
        }
    }

    private static Task<HeaderRecord> ReadHeaderAsync(Stream stream, CancellationToken cancellationToken = default)
        => ReadStruct<HeaderRecord>(stream, cancellationToken);

    private static async Task<T> ReadStruct<T>(Stream stream, CancellationToken cancellationToken = default) where T : struct
    {
        cancellationToken.ThrowIfCancellationRequested();

        var size = Marshal.SizeOf<T>();
        var buffer = new byte[size];
        var bytesread = await stream.ReadAsync(buffer, 0, size, cancellationToken);
        return bytesread == size
            ? MemoryMarshal.Read<T>(buffer)
            : throw new MalformedRecordException(typeof(T), stream.Position, buffer.Length, bytesread);
    }
}
