using System.Runtime.InteropServices;

namespace OwonBinfileReader;

[StructLayout(LayoutKind.Explicit, Size = 10)]
internal readonly record struct HeaderRecord
{
    [FieldOffset(0)] public readonly uint MagicHeaderBytes;
    [FieldOffset(4)] public readonly ushort Unknown;
    [FieldOffset(6)] public readonly uint HeaderSize;

    public const uint ExpectedMagicHeader = 0x58425053; // "SPBX"
}
