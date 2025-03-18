using OwonBinfileReader;
using System.Globalization;

namespace TestApp;

// Usage: testapp.exe <directory> [<directory> ...]
// This example program reads all .bin files in the specified directories and writes the data to a .csv file per channel in that same directory.
internal class Program
{
    private static readonly BinfileReader _bfr = new();

    private static async Task Main(string[] args)
    {
        foreach (var a in args.Where(Directory.Exists))
        {
            await ProcessDirectory(a);
        }
    }

    private static async Task ProcessDirectory(string path)
    {
        foreach (var f in Directory.EnumerateFiles(path, "*.bin"))
        {
            var filename = Path.GetFileNameWithoutExtension(f);
            var binfile = await _bfr.ReadAsync(f);

            foreach (var channel in binfile.MetaData.Channels)
            {
                Console.WriteLine($"Channel: {channel.Number}\tDisplay: {channel.Display}\tProbe: {channel.Probe}\tScale: {channel.Scale}\tOffset: {channel.Offset}\tCurrentRatio: {channel.CurrentRatio}\tCurrentRate: {channel.CurrentRate}\tFreq: {channel.Frequency}");
                if (channel.Display)
                {
                    using var csvfile = File.CreateText(Path.Combine(path, $"{filename}_ch{channel.Number}.csv"));
                    {
                        csvfile.Write(string.Join("\n", binfile.Measurements[channel.Number].Select(v => v.ToString("N8", CultureInfo.InvariantCulture))));
                    }
                }
            }
        }
    }
}
