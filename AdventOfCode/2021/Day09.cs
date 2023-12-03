using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2021
{
    public class Day09(IInputLoader loader) : CodeChallenge(loader)
    {
        public override object PartA()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);

            var map = PopulateMap(input);

            var lowPoints = CalculateLowPoints(map);

            return lowPoints.Sum(p => map[p.X, p.Y] + 1);
        }

        public override object PartB()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);

            var map = PopulateMap(input);
            var lowPoints = CalculateLowPoints(map);

            var basins = new int[lowPoints.Length];
            for (var i = 0; i < basins.Length; i++)
            {
                basins[i] = CalculateBasinScore(map, lowPoints[i].X, lowPoints[i].Y);
            }

            Array.Sort(basins);

            var largestBasinScores = 1;
            for (var i = Math.Max(0, basins.Length - 3); i < basins.Length; i++)
            {
                largestBasinScores *= basins[i];
            }

            return largestBasinScores;
        }

        private static int[,] PopulateMap(string[] input)
        {
            var height = input.Length + 2;
            var width = input[0].Length + 2;
            var map = new int[width, height];
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (y == 0 || y == height - 1 ||
                        x == 0 || x == width - 1)
                    {
                        map[x, y] = 9;
                    }
                    else
                    {
                        map[x, y] = input[y - 1][x - 1] - '0';
                    }
                }
            }

            return map;
        }

        private static (int X, int Y)[] CalculateLowPoints(int[,] map)
        {
            var width = map.GetLength(0);
            var height = map.GetLength(1);

            var lowPoints = new List<(int, int)>();
            for (var y = 1; y < height - 1; y++)
            {
                for (var x = 1; x < width - 1; x++)
                {
                    if (map[x, y] < map[x - 1, y] &&
                        map[x, y] < map[x, y - 1] &&
                        map[x, y] < map[x, y + 1] &&
                        map[x, y] < map[x + 1, y])
                    {
                        lowPoints.Add((x, y));
                    }
                }
            }

            return lowPoints.ToArray();
        }

        private static int CalculateBasinScore(int[,] map, int x, int y)
        {
            var basinPoints = new List<(int X, int Y)>();

            FindBasinPoints(map, x, y, ref basinPoints);

            return basinPoints.Distinct().Count();
        }

        private static void FindBasinPoints(int[,] map, int x, int y, ref List<(int X, int Y)> basinPoints)
        {
            if (map[x,y] == 9)
            {
                return;
            }

            if (map[x, y] < map[x - 1, y])
            {
                FindBasinPoints(map, x - 1, y, ref basinPoints);
            }
            if (map[x, y] < map[x, y - 1])
            {
                FindBasinPoints(map, x, y - 1, ref basinPoints);
            }
            if (map[x, y] < map[x, y + 1])
            {
                FindBasinPoints(map, x, y + 1, ref basinPoints);
            }
            if (map[x, y] < map[x + 1, y])
            {
                FindBasinPoints(map, x + 1, y, ref basinPoints);
            }

            basinPoints.Add((x, y));
        }
    }
}
