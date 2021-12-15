using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Day12
{
    class Day12
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
            Dictionary<string, List<string>> caves = GetCaves(file);
            
            List<List<string>> paths = new List<List<string>>();
            List<string> path = new List<string>();
            path.Add("start");
            CrawlCaves("start", caves, path, paths);

            return paths.Count;
        }

        static void CrawlCaves(string currentCave, Dictionary<string, List<string>> caves, List<string> path, List<List<string>> paths)
        {
            foreach (string cave in caves[currentCave])
            {
                List<string> p = new List<string>(path);

                if (cave == "start")
                    continue;

                if (cave == "end")
                {
                    p.Add(cave);
                    paths.Add(p);
                    continue;
                }

                if (cave.All(char.IsLower) && p.Contains(cave))
                    continue;

                p.Add(cave);
                CrawlCaves(cave, caves, p, paths);
            }
        }

        static long Part2(string[] file)
        {
            Dictionary<string, List<string>> caves = GetCaves(file);

            List<List<string>> paths = new List<List<string>>();
            List<string> path = new List<string>();
            path.Add("start");
            CrawlCaves2("start", caves, path, paths);

            return paths.Count;
        }

        static void CrawlCaves2(string currentCave, Dictionary<string, List<string>> caves, List<string> path, List<List<string>> paths)
        {
            foreach (string cave in caves[currentCave])
            {
                List<string> p = new List<string>(path);

                if (cave == "start")
                    continue;

                if (cave == "end")
                {
                    p.Add(cave);
                    paths.Add(p);
                    continue;
                }

                if (cave.All(char.IsLower) && p.Contains(cave))
                {
                    if (p.Where(c => c.All(char.IsLower)).Distinct().Count() < p.Where(c => c.All(char.IsLower)).Count())
                        continue;
                }

                p.Add(cave);
                CrawlCaves2(cave, caves, p, paths);
            }
        }

        private static Dictionary<string, List<string>> GetCaves(string[] file)
        {
            Dictionary<string, List<string>> caves = new Dictionary<string, List<string>>();

            for (int i = 0; i < file.Length; i++)
            {
                string[] nodes = file[i].Split('-');

                for (int j = 0; j <= 1; j++)
                {
                    string key = nodes[j];
                    string value = nodes[(j + 1) % 2];

                    if (!caves.ContainsKey(key))
                    {
                        List<string> strLst = new List<string>();
                        strLst.Add(value);
                        caves.Add(key, strLst);
                    }
                    else
                    {
                        if (!caves[key].Contains(value))
                            caves[key].Add(value);
                    }
                }
            }
            return caves;
        }
    }
}
