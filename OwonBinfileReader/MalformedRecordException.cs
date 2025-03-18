using System;

namespace OwonBinfileReader;

public class MalformedRecordException(Type expectedType, long position, int expectedBytes, int readBytes)
    : BinfileStreamReaderException($"Failed to read record of type {expectedType.Name} at position {position}; expected {expectedBytes} bytes, read {readBytes} bytes.")
{
    public long Position { get; init; } = position;
    public Type ExpectedType { get; init; } = expectedType;
    public int ExpectedBytes { get; init; } = expectedBytes;
    public int ReadBytes { get; init; } = readBytes;
}
