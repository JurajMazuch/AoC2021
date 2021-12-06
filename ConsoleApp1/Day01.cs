using System;
using System.Linq;
using System.Diagnostics;

namespace AoC2021
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("The number of times a depth measurement increases is {0} >> {1} [ms]", Part1(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();
            Console.WriteLine("The number of times the sum of measurements in sliding window increases is {0} >> {1} [ms]", Part2(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Stop();

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static double Part1(string[] file)
        {
            double increments = 0;
            for (int i = 1; i < file.Length; i++)
            {
                if (int.Parse(file[i]) - int.Parse(file[i - 1]) > 0)
                {
                    increments++;
                }
            }
            return increments;
        }

        static double Part2(string[] file)
        {
            int[] measurements = file.Select(x => int.Parse(x)).ToArray();
            double increments = 0;
            for (int i = 0; i < measurements.Length - 3; i++)
            {
                int a = measurements[i] + measurements[i + 1] + measurements[i + 2];
                int b = measurements[i + 1] + measurements[i + 2] + measurements[i + 3];

                if (b - a > 0)
                {
                    increments++;
                }
            }
            return increments;
        }
    }
}
