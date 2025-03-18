using System;

namespace OwonBinfileReader;

public class BinfileStreamReaderException(string message, Exception? innerException = null)
    : Exception(message, innerException)
{ }
