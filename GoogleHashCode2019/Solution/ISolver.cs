using System.Collections.Generic;
using GoogleHashCode2019.IO;
using GoogleHashCode2019.Model;

namespace GoogleHashCode2019.Solution
{
    public interface ISolver
    {
        IEnumerable<Slide> Solve(InputData inputData);
    }
}