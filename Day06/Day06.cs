using System;
using System.Linq;
using System.Diagnostics;

namespace Day06
{
    class Day06
    {
        static void Main(string[] args)
        {
            string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("There are {0} fish after 80 days. >> {1} [ms]", Reproduce(file, 80), stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();
            Console.WriteLine("There are {0} fish after 256 days. >> {1} [ms]", Reproduce(file, 256), stopwatch.ElapsedMilliseconds);
            stopwatch.Stop();

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static double Reproduce(string[] file, int numberOfDays)
        {
            //create array
            double[] fishCount = new double[9];
            for (int daysLeft = 0; daysLeft < 9; daysLeft++)
            {
                fishCount[daysLeft] = 0;
            }

            //set initial state
            foreach (int fish in file[0].Split(',').Select(x => int.Parse(x)).ToList())
            {
                fishCount[fish]++;
            }

            //reproduce
            for (int day = 1; day <= numberOfDays; day++)
            {
                double[] fishCountNew = new double[9];
                fishCount.CopyTo(fishCountNew, 0);

                for (int daysLeft = 0; daysLeft < 9; daysLeft++)
                {
                    if (daysLeft == 0)
                    {
                        fishCountNew[8] += fishCount[daysLeft];
                        fishCountNew[6] += fishCount[daysLeft];
                    }
                    else
                    {
                        fishCountNew[daysLeft - 1] += fishCount[daysLeft];
                    }

                    fishCountNew[daysLeft] -= fishCount[daysLeft];
                }
                fishCountNew.CopyTo(fishCount, 0);
            }

            return fishCount.Sum();
        }
    }
}
