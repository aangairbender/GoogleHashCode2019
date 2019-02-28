using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleHashCode2019.Solution.GreedySwap;

namespace GoogleHashCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            var io = new IO.IO(Console.In, Console.Out);
            var inputData = io.Read();

            var solver = new GreedySwapSolver();

            var outputData = solver.Solve(inputData);
            io.Write(outputData);
        }
    }
}
