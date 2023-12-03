using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021
{
    public class Day05(IInputLoader loader) : CodeChallenge(loader)
    {
        public override object PartA()
        {
            var lines = ParseLines(out var maxX, out var maxY);
            var grid = new int[maxX, maxY];

            foreach (var line in lines)
            {
                if (!line.IsHorizontalOrVertical())
                {
                    continue;
                }

                if (line.StartX > line.EndX || line.StartY > line.EndY)
                {
                    line.Flip();
                }

                for (var x = line.StartX; x <= line.EndX; x++)
                {
                    for (var y = line.StartY; y <= line.EndY; y++)
                    {
                        grid[x, y]++;
                    }
                }
            }

            var overlaps = 0;
            foreach (var cell in grid)
            {
                if (cell > 1)
                {
                    overlaps++;
                }
            }

            return overlaps;
        }

        public override object PartB()
        {
            var lines = ParseLines(out var maxX, out var maxY);
            var grid = new int[maxX, maxY];

            foreach (var line in lines)
            {
                if (line.IsHorizontalOrVertical())
                {
                    if (line.StartX > line.EndX || line.StartY > line.EndY)
                    {
                        line.Flip();
                    }

                    for (var x = line.StartX; x <= line.EndX; x++)
                    {
                        for (var y = line.StartY; y <= line.EndY; y++)
                        {
                            grid[x, y]++;
                        }
                    }
                }
                else if (line.Is45DegreeDiagonal())
                {
                    if (line.StartX > line.EndX)
                    {
                        line.Flip();
                    }

                    if (line.StartY <= line.EndY)
                    {
                        for (int x = line.StartX, y = line.StartY; y <= line.EndY; x++, y++)
                        {
                            grid[x, y]++;
                        }
                    }
                    else
                    {
                        for (int x = line.StartX, y = line.StartY; y >= line.EndY; x++, y--)
                        {
                            grid[x, y]++;
                        }
                    }
                }
            }

            var overlaps = 0;
            foreach (var cell in grid)
            {
                if (cell > 1)
                {
                    overlaps++;
                }
            }

            return overlaps;
        }

        private List<Line> ParseLines(out int maxX, out int maxY)
        {
            var input = inputLoader.LoadInput(InputLocation);
            var lines = new List<Line>();
            maxX = 0;
            maxY = 0;
            foreach (var line in input.Split("\r\n"))
            {
                var regex = Regex.Match(line, @"(\d+),(\d+) -> (\d+),(\d+)");

                var startX = int.Parse(regex.Groups[1].Value);
                var startY = int.Parse(regex.Groups[2].Value);
                var endX = int.Parse(regex.Groups[3].Value);
                var endY = int.Parse(regex.Groups[4].Value);

                maxX = Math.Max(startX + 1, maxX);
                maxX = Math.Max(endX + 1, maxX);
                maxY = Math.Max(startY + 1, maxY);
                maxY = Math.Max(endY + 1, maxY);

                lines.Add(new Line(startX, startY, endX, endY));
            }

            return lines;
        }

        private class Line(int startX, int startY, int endX, int endY)
        {
            public int StartX { get; set; } = startX;

            public int StartY { get; set; } = startY;

            public int EndX { get; set; } = endX;

            public int EndY { get; set; } = endY;

            public bool IsHorizontalOrVertical() => (StartX - EndX == 0) || (StartY - EndY == 0);

            public bool Is45DegreeDiagonal() => Math.Abs(StartX - EndX) == Math.Abs(StartY - EndY);

            public void Flip()
            {
                var tempX = EndX;
                var tempY = EndY;

                EndX = StartX;
                EndY = StartY;

                StartX = tempX;
                StartY = tempY;
            }
        }
    }
}
