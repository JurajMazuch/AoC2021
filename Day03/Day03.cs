using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("The power consumption of the submarine is {0} >> {1} [ms]", Part1(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();
            Console.WriteLine("The life support rating of the submarine is {0} >> {1} [ms]", Part2(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Stop();

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static double Part1(string[] file)
        {
            Dictionary<int, int> bitCounts = new Dictionary<int, int>();
            for (int i = 0; i < file[0].Length; i++)
                bitCounts.Add(i, 0);

            foreach (string line in file)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '1')
                    {
                        bitCounts[i]++;
                    }
                }
            }

            string gammaRate = "";
            string epsilonRate = "";
            foreach (int bitCount in bitCounts.Values)
            {
                if (bitCount > file.Length / 2)
                {
                    gammaRate += "1";
                    epsilonRate += "0";
                }
                else
                {
                    gammaRate += "0";
                    epsilonRate += "1";
                }
            }
            //Console.WriteLine("Gamma rate: {0}, Epsilon rate: {1}", gammaRate, epsilonRate);

            return BinaryStringToInteger(gammaRate) * BinaryStringToInteger(epsilonRate);
        }

        static double BinaryStringToInteger(string str)
        {
            int i = 0;
            double num = 0;
            foreach (char c in str.Reverse())
            {
                num += int.Parse(c.ToString()) * Math.Pow(2, i);
                i++;
            }
            
            return num;
        }

        static double Part2(string[] file)
        {
            return BinaryStringToInteger(RemoveUncommon(file.ToList(), 0)) * BinaryStringToInteger(RemoveCommon(file.ToList(), 0));
        }

        static string RemoveUncommon(List<string> input, int position)
        {
            if (input.Count == 1)
                return input[0];
            if (input.Count == 2)
            {
                return input.Where(x => x[position] == '1').ToList()[0];
            }
            char mostCommonBit;
            if (input.Where(x => x[position] == '1').Count() - input.Where(x => x[position] == '0').Count() >= 0)
                mostCommonBit = '1';
            else
                mostCommonBit = '0';
            
            return RemoveUncommon(input.Where(x => x[position] == mostCommonBit).ToList(), position + 1);
        }

        static string RemoveCommon(List<string> input, int position)
        {
            if (input.Count == 1)
                return input[0];
            if (input.Count == 2)
            {
                return input.Where(x => x[position] == '0').ToList()[0];
            }
            char leastCommonBit;
            if (input.Where(x => x[position] == '1').Count() - input.Where(x => x[position] == '0').Count() >= 0)
                leastCommonBit = '0';
            else
                leastCommonBit = '1';
            return RemoveCommon(input.Where(x => x[position] == leastCommonBit).ToList(), position + 1);
        }
    }
}
