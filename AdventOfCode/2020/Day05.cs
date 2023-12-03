using System;

namespace AdventOfCode._2020
{
    public class Day05(IInputLoader loader) : CodeChallenge(loader)
    {
        public override object PartA()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);

            var maxId = 0;
            foreach (var password in input)
            {
                var (Row, Column) = CalculateSeat(password);

                var id = (8 * Row) + Column;

                if (id > maxId)
                {
                    maxId = id;
                }
            }

            return maxId;
        }

        public override object PartB()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);
            var seats = new int[128, 8];

            foreach (var password in input)
            {
                var (Row, Column) = CalculateSeat(password);
                seats[Row, Column] = CalculateId(Row, Column);
            }

            for (var y = 0; y < 128; y++)
            {
                for (var x = 0; x < 7; x++)
                {
                    if (seats[y, x] == 0)
                    {
                        var seatId = CalculateId(y, x);
                        if (TwoDimensionArrayContains(seats, seatId - 1) && TwoDimensionArrayContains(seats, seatId + 1))
                        {
                            return CalculateId(y, x);
                        }
                    }
                }
            }

            throw new Exception("Could not find an empty seat.");
        }

        private static (int Row, int Column) CalculateSeat(string input)
        {
            var minRow = 0;
            var maxRow = 128;
            for (var i = 0; minRow != maxRow - 1; i++)
            {
                var step = Math.Pow(2, i + 1);
                if (input[i] == 'F')
                {
                    maxRow -= 128 / (int)step;
                }
                else
                {
                    minRow += 128 / (int)step;
                }
            }

            var minCol = 0;
            var maxCol = 8;
            for (var i = 7; minCol != maxCol - 1; i++)
            {
                var step = Math.Pow(2, i - 6);
                if (input[i] == 'L')
                {
                    maxCol -= 8 / (int)step;
                }
                else
                {
                    minCol += 8 / (int)step;
                }
            }

            return (minRow, minCol);
        }

        private static int CalculateId(int row, int column) => 8 * row + column;

        private static bool TwoDimensionArrayContains(int[,] array, int value)
        {
            var width = array.GetLength(0);
            var height = array.GetLength(1);

            for (var y = 0; y < width; y++)
            {
                for (var x = 0; x < height; x++)
                {
                    if (array[y, x] == value)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
