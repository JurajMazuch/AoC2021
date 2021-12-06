using System;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("Horizontal position multiplied by depth equals {0} >> {1} [ms]", Part1(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();
            Console.WriteLine("Horizontal position multiplied by depth equals {0} >> {1} [ms]", Part2(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Stop();

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static double Part1(string[] file)
        {
            Regex regex = new Regex(@"(?<command>[a-z]+) (?<units>\d+)");
            int position = 0;
            int depth = 0;

            foreach (string line in file)
            {
                Match m = regex.Match(line);
                string command = m.Groups["command"].Value;
                int units = int.Parse(m.Groups["units"].Value);

                switch (command)
                {
                    case "forward":
                        position += units;
                        break;
                    case "down":
                        depth += units;
                        break;
                    case "up":
                        depth -= units;
                        break;
                    default:
                        break;
                }
                //Console.WriteLine("{0} {1}: position {2} depth {3}", command, units, position, depth);
            }
            return position * depth;
        }

        static double Part2(string[] file)
        {

            Regex regex = new Regex(@"(?<command>[a-z]+) (?<units>\d+)");
            int position = 0;
            int depth = 0;
            int aim = 0;

            foreach (string line in file)
            {
                Match m = regex.Match(line);
                string command = m.Groups["command"].Value;
                int units = int.Parse(m.Groups["units"].Value);

                switch (command)
                {
                    case "forward":
                        position += units;
                        depth += aim * units;
                        break;
                    case "down":
                        aim += units;
                        break;
                    case "up":
                        aim -= units;
                        break;
                    default:
                        break;
                }
                //Console.WriteLine("{0} {1}: position {2} depth {3}", command, units, position, depth);
            }
            return position * depth;
        }
    }
}
