using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OwonBinfileReader;

public class Measurements : ReadOnlyDictionary<int, double[]>
{
    internal Measurements(IDictionary<int, double[]> measurements)
        : base(measurements) { }
}