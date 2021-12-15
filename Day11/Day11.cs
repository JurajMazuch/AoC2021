using System;
using System.Diagnostics;

namespace Day11
{
    class Day11
    {
        static void Main(string[] args)
        {
            string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");

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
            long sum = 0;
            bool[][] flashed = new bool[10][];
            int[][] octopuses = new int[10][];
            for (int i = 0; i < 10; i++)
            {
                octopuses[i] = Array.ConvertAll(file[i].ToCharArray(), d => int.Parse(d.ToString()));
                flashed[i] = new bool[10];
            }

            for (int step = 0; step < 100; step++)
            {
                //increment all
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        octopuses[i][j]++;
                        flashed[i][j] = false;
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (octopuses[i][j] > 9 && !flashed[i][j])
                        {
                            Flash(i, j, ref octopuses, ref flashed);
                        }
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (flashed[i][j])
                        {
                            octopuses[i][j] = 0;
                            sum++;
                        }
                    }
                }
            }
            return sum;
        }

        static void Flash(int x, int y, ref int[][] octopuses, ref bool[][] flashed)
        {
            flashed[x][y] = true;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;
                    if(x + i < 10 && x + i >= 0 && y + j < 10 && y + j >= 0)
                    {
                        octopuses[x + i][y + j]++;
                        if (octopuses[x + i][y + j] > 9 && !flashed[x + i][y + j])
                        {
                            Flash(x + i, y + j, ref octopuses, ref flashed);
                        }
                    }
                }
            }
        }

        static long Part2(string[] file)
        {
            long step = 0;
            int sum;
            bool[][] flashed = new bool[10][];
            int[][] octopuses = new int[10][];
            for (int i = 0; i < 10; i++)
            {
                octopuses[i] = Array.ConvertAll(file[i].ToCharArray(), d => int.Parse(d.ToString()));
                flashed[i] = new bool[10];
            }
            
            do
            {
                step++;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        octopuses[i][j]++;
                        flashed[i][j] = false;
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (octopuses[i][j] > 9 && !flashed[i][j])
                        {
                            Flash(i, j, ref octopuses, ref flashed);
                        }
                    }
                }

                sum = 0;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (flashed[i][j])
                        {
                            octopuses[i][j] = 0;
                            sum++;
                        }
                    }
                }
            } while (sum != 100);

            return step;
        }
    }
}
