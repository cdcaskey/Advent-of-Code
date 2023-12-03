using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2021
{
    public class Day04(IInputLoader loader) : CodeChallenge(loader)
    {
        public override long PartA()
        {
            var input = inputLoader.LoadInput(InputLocation);
            var drawnNumbers = ParseDrawnNumbers(input);
            var boards = ParseBoards(input);

            foreach (var number in drawnNumbers)
            {
                foreach (var board in boards)
                {
                    if (board.MarkNumber(number))
                    {
                        var unmarkedNumberTotal = board.GetScore();
                        return unmarkedNumberTotal * number;
                    }
                }
            }

            throw new Exception("Unable to find a winning board.");
        }

        public override long PartB()
        {
            var input = inputLoader.LoadInput(InputLocation);
            var drawnNumbers = ParseDrawnNumbers(input);
            var boards = ParseBoards(input);

            foreach (var number in drawnNumbers)
            {
                foreach (var board in boards.Where(b => !b.Complete))
                {
                    if (board.MarkNumber(number))
                    {
                        // If the final board has just been cleared, calculate its score
                        if (!boards.Any(b => !b.Complete))
                        {
                            var unmarkedNumberTotal = board.GetScore();
                            return unmarkedNumberTotal * number;
                        }
                    }
                }
            }

            throw new Exception("Unable to find a winning board.");
        }

        private static int[] ParseDrawnNumbers(string input)
        {
            var lines = input.Split("\r\n");

            // The first line is the numbers being drawn, the following lines are the boards
            var numberStrings = lines.First().Split(',');

            var numbers = new int[numberStrings.Length];
            for(var i = 0; i < numberStrings.Length; i++)
            {
                numbers[i] = int.Parse(numberStrings[i]);
            }

            return numbers;
        }

        private static List<BingoBoard> ParseBoards(string input)
        {
            var boardValues = input.Split("\r\n\r\n");
            var boards = new List<BingoBoard>();

            for (var i = 1; i < boardValues.Length; i++)
            {
                boards.Add(new BingoBoard(boardValues[i]));
            }

            return boards;
        }

        private class BingoBoard
        {
            private readonly int?[,] Board = new int?[5, 5];

            public BingoBoard(string numbers)
            {
                var values = numbers.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0, y = 0; y < 5; y++)
                {
                    for (var x = 0; x < 5; x++, i++)
                    {
                        Board[y, x] = int.Parse(values[i]);
                    }
                }
            }

            public bool Complete { get; private set; } = false;

            /// <summary>
            /// Mark a number off the bingo board.
            /// </summary>
            /// <param name="number">The number to be marked off.</param>
            /// <returns>true if the number being marked results in the board being complete.</returns>
            public bool MarkNumber(int number)
            {
                var numberMarked = false;

                for (var y = 0; y < 5; y++)
                {
                    for (var x = 0; x < 5; x++)
                    {
                        if (Board[y, x] == number)
                        {
                            Board[y, x] = null;
                            numberMarked = true;
                        }
                    }
                }

                if (numberMarked)
                {
                    return CheckWinner();
                }

                return false;
            }

            public int GetScore()
            {
                var score = 0;
                foreach (var number in Board)
                {
                    if (number != null)
                    {
                        score += number.Value;
                    }
                }

                return score;
            }

            private bool CheckWinner()
            {
                // Go through the board, checking if there is a column which has been ticked off
                for (var y = 0; y < 5; y++)
                {
                    var unmarkedNumber = false;

                    for (var x = 0; x < 5; x++)
                    {
                        if (Board[x, y] != null)
                        {
                            unmarkedNumber = true;
                            break;
                        }
                    }

                    if (!unmarkedNumber)
                    {
                        Complete = true;
                        return true;
                    }
                }

                // Go through the board, checking if there is a row which has been ticked off
                for (var x = 0; x < 5; x++)
                {
                    var unmarkedNumber = false;

                    for (var y = 0; y < 5; y++)
                    {
                        if (Board[x, y] != null)
                        {
                            unmarkedNumber = true;
                            break;
                        }
                    }

                    if (!unmarkedNumber)
                    {
                        Complete = true;
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
