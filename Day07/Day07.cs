using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Day07
{
    class Day07
    {
        static void Main(string[] args)
        {
            string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("{0} fuel must be spent to allign in one possition. >> {1} [ms]", Part1(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();
            Console.WriteLine("{0} fuel must be spent to allign in one possition. >> {1} [ms]", Part2(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Stop();

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static double Part1(string[] file)
        {
            List<int> numbers = file[0].Split(',').Select(int.Parse).ToList();
            int min = int.MaxValue;
            for (int i = numbers.Min(); i <= numbers.Max(); i++)
            {
                int sum = 0;
                foreach (int number in numbers)
                {
                    sum += Math.Abs(number - i);
                }
                
                min = sum < min ? sum : min;
            }
            return min;
        }

        static double Part2(string[] file)
        {
            List<int> numbers = file[0].Split(',').Select(int.Parse).ToList();
            int min = int.MaxValue;
            for (int i = numbers.Min(); i <= numbers.Max(); i++)
            {
                int sum = 0;
                foreach (int number in numbers)
                {
                    int n = Math.Abs(number - i);
                    sum += n * (n + 1) / 2;
                }
                
                min = sum < min ? sum : min;
            }
            return min;
        }
    }
}
