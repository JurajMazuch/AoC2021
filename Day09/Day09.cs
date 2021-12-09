using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace Day09
{
    class Day09
    {
        static void Main(string[] args)
        {
            string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("The sum of the risk levels of all low points on my heightmap is {0}. >> {1} [ms]", Part1(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();
            Console.WriteLine("Sizes of the three largest basins multiplied equals {0}. >> {1} [ms]", Part2(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Stop();

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static long Part1(string[] file)
        {
            int[][] heightMap = new int[file.Length][];
            for (int i = 0; i < file.Length; i++)
            {
                heightMap[i] = file[i].ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();
            }

            List<Point> lowPoints = GetLowPoints(heightMap);

            long sum = 0;

            foreach (Point point in lowPoints)
            {
                sum += heightMap[point.Y][point.X] + 1;
            }
            return sum;
        }

        static double Part2(string[] file)
        {
            int[][] heightMap = new int[file.Length][];
            for (int i = 0; i < file.Length; i++)
            {
                heightMap[i] = file[i].ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();
            }

            List<Point> lowPoints = GetLowPoints(heightMap);

            List<int> basinSize = new List<int>();
            foreach (Point point in lowPoints)
            {
                bool[] visitedPoints = new bool[heightMap.Length * heightMap[0].Length];
                int size = GetBasinSize(point, ref heightMap, ref visitedPoints);
                basinSize.Add(size);
            }

            return basinSize.OrderByDescending(x => x).Take(3).Aggregate((a, x) => a * x);
        }

        private static List<Point> GetLowPoints(int[][] heightMap)
        {
            List<Point> lowPoints = new List<Point>();

            for (int y = 0; y < heightMap.Length; y++)
            {
                for (int x = 0; x < heightMap[y].Length; x++)
                {
                    int N, W, S, E;
                    int height = heightMap[y][x];
                    N = (y - 1 >= 0) ? heightMap[y - 1][x] : int.MaxValue;
                    W = (x - 1 >= 0) ? heightMap[y][x - 1] : int.MaxValue;
                    S = (x + 1 < heightMap[y].Length) ? heightMap[y][x + 1] : int.MaxValue;
                    E = (y + 1 < heightMap.Length) ? heightMap[y + 1][x] : int.MaxValue;

                    if (height < N && height < W && height < S && height < E)
                    {
                        lowPoints.Add(new Point(x, y));
                    }
                }
            }

            return lowPoints;
        }

        private static int GetBasinSize(Point point, ref int[][] heightMap, ref bool[] visitedPoints)
        {
            int N, W, S, E;
            int n = heightMap[0].Length;
            int m = heightMap.Length;
            int x = point.X;
            int y = point.Y;
            int height = heightMap[y][x];

            visitedPoints[y * n + x] = true;
            int size = 1;

            N = (y - 1 >= 0) ? heightMap[y - 1][x] : int.MaxValue;
            W = (x - 1 >= 0) ? heightMap[y][x - 1] : int.MaxValue;
            S = (x + 1 < n) ? heightMap[y][x + 1] : int.MaxValue;
            E = (y + 1 < m) ? heightMap[y + 1][x] : int.MaxValue;

            if (N < 9 && !visitedPoints[(y - 1) * n + x])
            {
                size += GetBasinSize(new Point(x, y - 1), ref heightMap, ref visitedPoints);
            }
            if (W < 9 && !visitedPoints[y * n + x - 1])
            {
                size += GetBasinSize(new Point(x - 1, y), ref heightMap, ref visitedPoints);
            }
            if (S < 9 && !visitedPoints[y * n + x + 1])
            {
                size += GetBasinSize(new Point(x + 1, y), ref heightMap, ref visitedPoints);
            }
            if (E < 9 && !visitedPoints[(y + 1) * n + x])
            {
                size += GetBasinSize(new Point(x, y + 1), ref heightMap, ref visitedPoints);
            }

            return size;
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
