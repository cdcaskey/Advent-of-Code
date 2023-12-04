using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2023
{
    public class Day03(IInputLoader loader) : CodeChallenge(loader)
    {
        public override object PartA()
        {
            var input = inputLoader.LoadGridofChars(InputLocation, out var maxX, out var maxY);
            var symbols = FindSymbolCoordinates(input, maxX, maxY);
            var numberList = FindNumberCoordinates(input, maxX, maxY);

            var partTotal = 0;
            foreach (var number in numberList)
            {
                if (IsPartNumber(number, symbols, out var partNumber))
                {
                    partTotal += partNumber;
                }
            }

            return partTotal;
        }

        public override object PartB()
        {
            var input = inputLoader.LoadGridofChars(InputLocation, out var maxX, out var maxY);
            var symbols = FindSymbolCoordinates(input, maxX, maxY, '*');
            var numberList = FindNumberCoordinates(input, maxX, maxY);

            var totalRatio = 0;
            foreach (var symbol in symbols)
            {
                if (SymbolIsTouching2Numbers(symbol, numberList, out var ratio1, out var ratio2))
                {
                    totalRatio += ratio1 * ratio2;
                }

            }

            return totalRatio;
        }

        private static List<(int X, int Y)> FindSymbolCoordinates(char[,] grid, int maxX, int maxY, params char[] specificSymbols)
        {
            var symbolList = new List<(int X, int Y)>();
            for (var x = 0; x < maxX; x++)
            {
                for (var y = 0; y < maxY; y++)
                {
                    if ((grid[x, y] < '0' || grid[x, y] > '9') && grid[x, y] != '.')
                    {
                        if (specificSymbols.Length == 0 || specificSymbols.Contains(grid[x, y]))
                        {
                            symbolList.Add((x, y));
                        }
                    }
                }
            }

            return symbolList;
        }
        private static List<(int X, int Y, int Length, int Number)> FindNumberCoordinates(char[,] grid, int maxX, int maxY)
        {
            var numberList = new List<(int X, int Y, int Length, int Number)>();

            var startPos = (0, 0);
            var newNumber = true;
            var number = string.Empty;

            for (var y = 0; y < maxY; y++)
            {
                for (var x = 0; x < maxX; x++)
                {
                    if (grid[x, y] >= '0' && grid[x, y] <= '9')
                    {
                        number += grid[x, y];
                        if (newNumber)
                        {
                            startPos = (x, y);
                            newNumber = false;
                        }
                    }
                    else
                    {
                        if (!newNumber)
                        {
                            numberList.Add((startPos.Item1, startPos.Item2, number.Length, int.Parse(number)));

                            startPos = (0, 0);
                            number = string.Empty;
                            newNumber = true;
                        }
                    }
                }
            }

            return numberList;
        }

        private static bool IsPartNumber((int X, int Y, int Length, int Number) number, List<(int X, int Y)> symbols, out int partNumber)
        {
            partNumber = default;

            foreach (var (symbolX, symbolY) in symbols)
            {
                for (var x = number.X; x < number.X + number.Length; x++)
                {
                    if (Math.Abs(x - symbolX) <= 1 && (Math.Abs(number.Y - symbolY) <= 1))
                    {
                        partNumber = number.Number;
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool SymbolIsTouching2Numbers((int X, int Y) symbol, List<(int X, int Y, int Length, int Number)> numberList, out int ratio1, out int ratio2)
        {
            var touchingRatios = new List<int>();

            foreach (var (numberX, numberY, numberLength, Number) in numberList)
            {
                for (var x = numberX; x < numberX + numberLength; x++)
                {
                    if (Math.Abs(x - symbol.X) <= 1 && (Math.Abs(numberY - symbol.Y) <= 1))
                    {
                        touchingRatios.Add(Number);
                        break;
                    }
                }
            }

            if (touchingRatios.Count == 2)
            {
                ratio1 = touchingRatios[0];
                ratio2 = touchingRatios[1];
                return true;
            }

            ratio1 = default;
            ratio2 = default;
            return false;
        }
    }
}
