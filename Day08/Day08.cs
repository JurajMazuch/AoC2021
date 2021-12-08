using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Day08
{
    class Day08
    {
        static void Main(string[] args)
        {
            string[] file = System.IO.File.ReadAllLines(@"..\..\Input.txt");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("In the output values, digits 1, 4, 7, or 8 appear {0} times. >> {1} [ms]", Part1(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();
            Console.WriteLine("If you add up all of the output values, you get {0}. >> {1} [ms]", Part2(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Stop();

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static double Part1(string[] file)
        {
            string[][] patterns = new string[file.Length][];
            string[][] output = new string[file.Length][];
            int[] digits = new int[10];
            for (int i = 0; i < file.Length; i++)
            {
                patterns[i] = file[i].Split('|')[0].Split(' ').Where(x => x != string.Empty).ToArray();
                output[i] = file[i].Split('|')[1].Split(' ').Where(x => x != string.Empty).ToArray();
            }

            for (int i = 0; i < file.Length; i++)
            {
                foreach (string digit in output[i])
                {
                    switch (digit.Length)
                    {
                        case 2:
                            digits[1]++;
                            break;
                        case 3:
                            digits[6]++;
                            break;
                        case 4:
                            digits[3]++;
                            break;
                        case 7:
                            digits[7]++;
                            break;
                        default:
                            break;
                    }
                }
            }
            return digits.Sum();
        }

        static double Part2(string[] file)
        {
            string[][] patterns = new string[file.Length][];
            string[][] output = new string[file.Length][];
            char top, middle, bottom, upperLeft, lowerLeft, upperRight, lowerRight;
            top = middle = bottom = upperLeft = lowerLeft = upperRight = lowerRight = ' ';
            long sum = 0;

            for (int i = 0; i < file.Length; i++)
            {
                patterns[i] = file[i].Split('|')[0].Split(' ').Where(x => x != string.Empty).ToArray();
                output[i] = file[i].Split('|')[1].Split(' ').Where(x => x != string.Empty).ToArray();
            }

            for (int i = 0; i < file.Length; i++)
            {
                string[] digits = new string[10];

                digits[1] = patterns[i].Where(p => p.Length == 2).First();
                digits[4] = patterns[i].Where(p => p.Length == 4).First();
                digits[7] = patterns[i].Where(p => p.Length == 3).First();
                digits[8] = patterns[i].Where(p => p.Length == 7).First();
                digits[9] = patterns[i].Where(p => p.Length == 6 && digits[4].All(c => p.Contains(c))).First();

                top = digits[7].ToList().Where(c => !digits[1].ToList().Contains(c)).First();
                bottom = digits[9].ToCharArray().ToList().Where(c => !digits[4].ToList().Contains(c) && c != top).First();
                lowerLeft = digits[8].ToList().Where(c => !digits[9].ToList().Contains(c)).First();

                digits[0] = patterns[i].Where(p => p.Length == 6 && digits[1].All(c => p.Contains(c)) && p.Contains(top) && p.Contains(bottom) && p.Contains(lowerLeft)).First();
                digits[6] = patterns[i].Where(p => p.Length == 6 && p != digits[9] && p != digits[0]).First();

                middle = digits[8].ToList().Where(c => !digits[0].ToList().Contains(c)).First();
                upperRight = digits[9].ToList().Where(c => !digits[6].ToList().Contains(c)).First();
                lowerRight = digits[1].ToList().Where(c => c != upperRight).First();
                upperLeft = digits[4].ToList().Where(c => c != upperRight && c != lowerRight && c != middle).First();

                digits[2] = patterns[i].Where(p => p.Length == 5 && p.Contains(top) && p.Contains(upperRight) && p.Contains(middle) && p.Contains(lowerLeft) && p.Contains(bottom)).First();
                digits[3] = patterns[i].Where(p => p.Length == 5 && p.Contains(top) && p.Contains(upperRight) && p.Contains(middle) && p.Contains(lowerRight) && p.Contains(bottom)).First();
                digits[5] = patterns[i].Where(p => p.Length == 5 && p.Contains(top) && p.Contains(upperLeft) && p.Contains(middle) && p.Contains(lowerRight) && p.Contains(bottom)).First();

                string value = "";
                foreach (string pattern in output[i])
                {
                    int digit = Array.IndexOf(digits ,patterns[i].Where(p => pattern.All(c => p.Contains(c)) && p.Length == pattern.Length).First());
                    value += digit.ToString();
                }
                
                sum += long.Parse(value);
            }
            return sum;
        }
    }
}
