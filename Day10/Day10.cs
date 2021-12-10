using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace Day10
{
    class Day10
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
            long points = 0;
            foreach (string line in file)
            {
                Stack<char> stack = new Stack<char>();

                foreach (char c in line.ToCharArray())
                {
                    //left bracket
                    if (c == '(' || c == '[' || c == '{' || c == '<')
                    {
                        stack.Push(c);
                        continue;
                    }

                    //correct right bracket
                    if (c == ')' && stack.Peek() == '(' || c == ']' && stack.Peek() == '[' || c == '}' && stack.Peek() == '{' || c == '>' && stack.Peek() == '<')
                    {
                        stack.Pop();
                        continue;
                    }

                    //incorrect right bracket
                    switch (c)
                    {
                        case ')':
                            points += 3;
                            break;
                        case ']':
                            points += 57;
                            break;
                        case '}':
                            points += 1197;
                            break;
                        case '>':
                            points += 25137;
                            break;
                        default:
                            break;
                    }
                    
                    break;
                }
            }
            
            return points;
        }

        static long Part2(string[] file)
        {
            List<long> pointsAll = new List<long>();
            foreach (string line in file)
            {
                long points = 0;
                Stack<char> stack = new Stack<char>();
                bool correct = true;
                foreach (char c in line.ToCharArray())
                {
                    //left bracket
                    if (c == '(' || c == '[' || c == '{' || c == '<')
                    {
                        stack.Push(c);
                        continue;
                    }

                    //correct right bracket
                    if (c == ')' && stack.Peek() == '(' || c == ']' && stack.Peek() == '[' || c == '}' && stack.Peek() == '{' || c == '>' && stack.Peek() == '<')
                    {
                        stack.Pop();
                        continue;
                    }
                    
                    correct = false;
                    break;
                }

                if (correct)
                {
                    while (stack.Count > 0)
                    {
                        points *= 5;

                        switch (stack.Pop())
                        {
                            case '(':
                                points += 1;
                                break;
                            case '[':
                                points += 2;
                                break;
                            case '{':
                                points += 3;
                                break;
                            case '<':
                                points += 4;
                                break;
                            default:
                                break;
                        }
                    }
                    pointsAll.Add(points);
                }
            }
            pointsAll.Sort();

            return pointsAll[(int)(pointsAll.Count() / 2)];
        }
    }
}