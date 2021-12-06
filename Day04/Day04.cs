using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            Console.WriteLine("My final score if I choose the first winning board will be {0} >> {1} [ms]", Part1(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();
            Console.WriteLine("My final score if I choose the last winning board will be {0} >> {1} [ms]", Part2(file), stopwatch.ElapsedMilliseconds);
            stopwatch.Stop();

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static double Part1(string[] file)
        {
            int[] numbers;
            int[][][] boards, boardResults;
            ReadInput(file.ToList(), out numbers, out boards, out boardResults);

            foreach (int number in numbers)
            {
                for (int i = 0; i < boards.Length; i++)
                {
                    FindNumber(ref boards[i], number, ref boardResults[i]);

                    if (IsWinning(boardResults[i]))
                    {
                        return SumNotCalled(boards[i], boardResults[i]) * number;
                    }
                }
            }
            return 0;
        }

        static double Part2(string[] file)
        {
            int[] numbers;
            int[][][] boards, boardResults;
            ReadInput(file.ToList(), out numbers, out boards, out boardResults);

            bool[] bingo = new bool[boards.Length];
            for (int i = 0; i < boards.Length; i++)
            {
                bingo[i] = false;
            }

            foreach (int number in numbers)
            {
                for (int i = 0; i < boards.Length; i++)
                {
                    if (bingo[i])
                        continue;

                    FindNumber(ref boards[i], number, ref boardResults[i]);

                    if (IsWinning(boardResults[i]))
                    {
                        bingo[i] = true;
                    }

                    if (bingo.Where(x => x == false).Count() == 0)
                    {
                        return SumNotCalled(boards[i], boardResults[i]) * number;
                    }
                }
            }
            return 0;
        }
        
        static void FindNumber(ref int[][] board, int number, ref int[][] boardResults)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (board[i][j] == number)
                    {
                        boardResults[i][j] = 1;
                        return;
                    }
                }
            }
        }

        static bool IsWinning(int[][] boardResults)
        {
            for (int i = 0; i < 5; i++)
            {
                int sumRow = 0;
                int sumCol = 0;
                for (int j = 0; j < 5; j++)
                {
                    sumRow += boardResults[i][j];
                    sumCol += boardResults[j][i];
                }
                if (sumRow == 5 || sumCol == 5)
                {
                    return true;
                }
            }
            return false;
        }

        static int SumNotCalled(int[][] board, int[][] boardResults)
        {
            int sum = 0;
            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    if (boardResults[j][k] == 0)
                    {
                        sum += board[j][k];
                    }
                }
            }
            return sum;
        }

        static void ReadInput(List<string> input, out int[] numbers, out int[][][] boards, out int[][][] boardResults)
        {
            numbers = input[0].Split(',').Select(x => int.Parse(x)).ToArray();
            input.RemoveAt(0);

            int boardCount = (int)input.Count() / 6;
            boards = new int[boardCount][][];
            int line = 0;
            for (int i = 0; i < boardCount; i++)
            {
                line++;
                boards[i] = new int[5][];
                for (int j = 0; j < 5; j++)
                {
                    input[line] = Regex.Replace(input[line], " +", " ").Trim();
                    int[] row = input[line].Split(' ').Select(x => int.Parse(x)).ToArray();
                    boards[i][j] = new int[5];
                    for (int k = 0; k < 5; k++)
                    {
                        boards[i][j][k] = row[k];
                    }
                    line++;
                }
            }

            boardResults = new int[boards.Length][][];
            for (int i = 0; i < boards.Length; i++)
            {
                boardResults[i] = new int[5][];
                for (int j = 0; j < 5; j++)
                {
                    boardResults[i][j] = new int[5];
                    for (int k = 0; k < 5; k++)
                    {
                        boardResults[i][j][k] = 0;
                    }
                }
            }
        }
    }
}
