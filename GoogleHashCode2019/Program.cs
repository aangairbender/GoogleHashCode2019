﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleHashCode2019.Solution.GreedySwap;
using GoogleHashCode2019.Solution.Grouper;
using GoogleHashCode2019.Solution.Same_unique;

namespace GoogleHashCode2019
{
    class Program
    {
        public static string OutputPath = @"C:/tmp/";
        public static string InputExt = ".txt";
        public static string OutputExt = ".txt";

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                SingleSolve(new IO.IO(Console.In, Console.Out));
                return;
            }
            foreach (var dataSet in args)
            {
                Directory.CreateDirectory(OutputPath);
                var io = new IO.IO(
                    new StreamReader(@"../../DataSets/" + dataSet + InputExt), 
                    new StreamWriter(OutputPath + dataSet + OutputExt));
                Console.Out.Write("Working on " + dataSet + " ...");
                var sw = new Stopwatch();
                sw.Start();
                SingleSolve(io);
                sw.Stop();
                Console.Out.WriteLine($"Ok. Elapsed {sw.ElapsedMilliseconds / 1000f}");
            }
        }

        private static void SingleSolve(IO.IO io)
        {
            var inputData = io.Read();

            var solver = new Same_unique();


            var outputData = solver.Solve(inputData);
            io.Write(outputData);
        }
    }
}
