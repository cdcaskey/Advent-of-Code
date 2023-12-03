using System;

namespace AdventOfCode._2022
{
    public class Day02(IInputLoader loader) : CodeChallenge(loader)
    {
        public override long PartA()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);

            var score = 0;
            foreach (var line in input)
            {
                var opponent = (Option)(line[0] - 'A' + 1);
                var player = (Option)(line[2] - 'X' + 1);

                score += CalculateResult(player, opponent);
            }

            return score;
        }

        public override long PartB()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);

            var score = 0;
            foreach (var line in input)
            {
                var opponent = (Option)(line[0] - 'A' + 1);
                var result = (Result)(('X' - line[2]) * -1);

                var player = CalculateOption(opponent, result);

                score += (int)result * 3 + (int)player;
            }

            return score;
        }

        private static int CalculateResult(Option player, Option opponent)
        {
            Result result;

            if (player == opponent)
            {
                result = Result.Draw;
            }
            else if (player == Option.Rock)
            {
                if (opponent == Option.Scissors)
                {
                    result = Result.Win;
                }
                else
                {
                    result = Result.Loss;
                }
            }
            else if (player == Option.Paper)
            {
                if (opponent == Option.Rock)
                {
                    result = Result.Win;
                }
                else
                {
                    result = Result.Loss;
                }
            }
            else
            {
                if (opponent == Option.Paper)
                {
                    result = Result.Win;
                }
                else
                {
                    result = Result.Loss;
                }
            }

            return (int)result * 3 + (int)player;
        }

        private static Option CalculateOption(Option opponent, Result requiredResult)
        {
            if (requiredResult == Result.Draw)
            {
                return opponent;
            }

            if (requiredResult == Result.Win)
            {
                return opponent switch
                {
                    Option.Rock => Option.Paper,
                    Option.Paper => Option.Scissors,
                    Option.Scissors => Option.Rock,
                    _ => throw new NotImplementedException()
                };
            }
            else
            {
                return opponent switch
                {
                    Option.Rock => Option.Scissors,
                    Option.Paper => Option.Rock,
                    Option.Scissors => Option.Paper,
                    _ => throw new NotImplementedException()
                };
            }
        }

        private enum Result
        {
            Win = 2,
            Draw = 1,
            Loss = 0
        }

        private enum Option
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }
    }
}
