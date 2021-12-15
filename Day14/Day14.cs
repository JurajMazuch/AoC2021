using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Day14
{
    class Day14
    {
        static void Main(string[] args)
        {
            string[] file = System.IO.File.ReadAllLines(@"..\..\Input_Test.txt");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine(" {0} >> {1} [ms]", Part1(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();
            Console.WriteLine(" {0} >> {1} [ms]", Part2(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Stop();

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static long Part1(string[] file)
        {
            return 0;
        }

        static long Part2(string[] file)
        {
            return 0;
        }
    }
}
