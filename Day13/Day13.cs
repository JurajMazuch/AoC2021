using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Day13
{
    class Day13
    {
        static void Main(string[] args)
        {
            string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine(" {0} >> {1} [ms]", Part1(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();
            Part2(file);
            Console.WriteLine("{0} [ms]", stopwatch.ElapsedMilliseconds);
            stopwatch.Stop();

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static long Part1(string[] file)
        {
            List<Point> dots;
            List<string> instructions;
            ReadInput(file, out dots, out instructions);

            Fold(ref dots, instructions.Take(1).ToList());
            return dots.Count;
        }

        static void Part2(string[] file)
        {
            List<Point> dots;
            List<string> instructions;
            ReadInput(file, out dots, out instructions);

            Fold(ref dots, instructions);
            PrintCode(dots);
        }

        static void ReadInput(string[] file, out List<Point> dots, out List<string> instructions)
        {
            dots = new List<Point>();
            instructions = new List<string>();
            int i = 0;
            while (file[i] != string.Empty)
            {
                Point dot = new Point(int.Parse(file[i].Split(',')[0]), int.Parse(file[i].Split(',')[1]));
                dots.Add(dot);
                i++;
            }

            for (int j = i + 1; j < file.Length; j++)
            {
                instructions.Add(file[j].Replace("fold along ", string.Empty));
            }
        }

        private static void Fold(ref List<Point> dots, List<string> instructions)
        {
            foreach (string instruction in instructions)
            {
                List<Point> dotsNew = new List<Point>(dots);
                foreach (Point dot in dots)
                {
                    int axis = int.Parse(instruction.Substring(2));
                    switch (instruction[0])
                    {
                        case 'x':
                            if (dot.X > axis)
                            {
                                dotsNew.Remove(dot);
                                Point d = new Point(dot.X - 2 * (dot.X - axis), dot.Y);
                                if (!dots.Contains(d))
                                {
                                    dotsNew.Add(d);
                                }
                            }
                            break;
                        case 'y':
                            if (dot.Y > axis)
                            {
                                dotsNew.Remove(dot);
                                Point d = new Point(dot.X, dot.Y - 2 * (dot.Y - axis));
                                if (!dots.Contains(d))
                                {
                                    dotsNew.Add(d);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                dots = new List<Point>(dotsNew);
            }
        }

        static void PrintCode(List<Point> dots)
        {
            int m = dots.Max(d => d.X) + 1;
            int n = dots.Max(d => d.Y) + 1;

            char[][] code = new char[n][];
            for (int y = 0; y < n; y++)
            {
                code[y] = new char[m];
                for (int x = 0; x < m; x++)
                {
                    code[y][x] = '.';
                }
            }

            foreach (Point dot in dots)
            {
                code[dot.Y][dot.X] = '#';
            }

            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < m; x++)
                {
                    Console.Write(code[y][x]);
                }
                Console.WriteLine();
            }
        }

        public struct Point
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; }
            public int Y { get; }
        }
    }
}