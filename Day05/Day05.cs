using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("At least two lines overlap in {0} points. >> {1} [ms]", Part1(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();
            Console.WriteLine("At least two lines overlap in {0} points. >> {1} [ms]", Part2(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Stop();

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static double Part1(string[] file)
        {
            List<List<Point>> lines;
            int[][] grid;
            ReadInput(file, out lines, out grid);

            foreach (List<Point> line in lines)
            {
                int y;
                if (line[0].x - line[1].x == 0) //vertical line
                {
                    for (y = Math.Min(line[0].y, line[1].y); y <= Math.Max(line[0].y, line[1].y); y++)
                    {
                        grid[y][line[0].x]++;
                    }
                    continue;
                }
                if (line[0].y - line[1].y == 0) //horizontal line
                {
                    for (int x = Math.Min(line[0].x, line[1].x); x <= Math.Max(line[0].x, line[1].x); x++)
                    {
                        grid[line[0].y][x]++;
                    }
                    continue;
                }
            }
            //PrintMatrix(grid);

            return CountOverlaps(grid);
        }
        
        static double Part2(string[] file)
        {
            List<List<Point>> lines;
            int[][] grid;
            ReadInput(file, out lines, out grid);

            foreach (List<Point> line in lines)
            {
                int y;
                if (line[0].x - line[1].x == 0) //vertical line
                {
                    for (y = Math.Min(line[0].y, line[1].y); y <= Math.Max(line[0].y, line[1].y); y++)
                    {
                        grid[y][line[0].x]++;
                    }
                    continue;
                }
                if (line[0].y - line[1].y == 0) //horizontal line
                {
                    for (int x = Math.Min(line[0].x, line[1].x); x <= Math.Max(line[0].x, line[1].x); x++)
                    {
                        grid[line[0].y][x]++;
                    }
                    continue;
                }

                //diagonal line
                List<Point> l = line.OrderBy(p => p.x).ToList();
                if (l[0].y < l[1].y) //top left to bottom right
                {
                    y = Math.Min(l[0].y, l[1].y);
                    for (int x = Math.Min(l[0].x, l[1].x); x <= Math.Max(l[0].x, l[1].x); x++, y++)
                    {
                        grid[y][x]++;
                    }
                }
                else //bottom left to top right
                {
                    y = Math.Max(l[0].y, l[1].y);
                    for (int x = Math.Min(l[0].x, l[1].x); x <= Math.Max(l[0].x, l[1].x); x++, y--)
                    {
                        grid[y][x]++;
                    }
                }
                
            }
            //PrintMatrix(grid);

            return CountOverlaps(grid);
        }

        struct Point
        {
            public int x;
            public int y;
        }

        static void PrintMatrix(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    Console.Write(matrix[i][j]);
                }
                Console.WriteLine();
            }
        }

        static void ReadInput(string[] file, out List<List<Point>> lines, out int[][] grid)
        {
            Regex r = new Regex(@"(?<x1>\d+),(?<y1>\d+) -> (?<x2>\d+),(?<y2>\d+)");
            int maxX = 0;
            int maxY = 0;
            lines = new List<List<Point>>();
            foreach (string line in file)
            {
                Match m = r.Match(line);
                Point p1 = new Point();
                Point p2 = new Point();
                p1.x = int.Parse(m.Groups["x1"].Value);
                p1.y = int.Parse(m.Groups["y1"].Value);
                p2.x = int.Parse(m.Groups["x2"].Value);
                p2.y = int.Parse(m.Groups["y2"].Value);

                if (Math.Max(p1.x, p2.x) > maxX)
                    maxX = Math.Max(p1.x, p2.x);

                if (Math.Max(p1.y, p2.y) > maxY)
                    maxY = Math.Max(p1.y, p2.y);
                List<Point> points = new List<Point>();
                points.Add(p1);
                points.Add(p2);
                lines.Add(points);
            }
            maxX++; maxY++;
            grid = new int[maxY][];
            for (int y = 0; y < maxY; y++)
            {
                grid[y] = new int[maxX];
                for (int x = 0; x < maxX; x++)
                {
                    grid[y][x] = 0;
                }
            }
        }

        static int CountOverlaps(int[][] matrix)
        {
            int count = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] > 1)
                        count++;
                }
            }
            return count;
        }
    }
}