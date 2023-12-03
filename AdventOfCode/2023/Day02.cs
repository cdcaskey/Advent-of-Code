using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2023
{
    public class Day02(IInputLoader loader) : CodeChallenge(loader)
    {
        public override object PartA()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);
            var games = ParseGames(input);

            var result = 0;
            foreach (var game in games)
            {
                if (GameIsPossible(game, 12, 13, 14))
                {
                    result += game.Id;
                }
            }

            return result;
        }

        public override object PartB()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);
            var games = ParseGames(input);

            return games.Sum(x => CalculatePower(x));
        }

        private List<Game> ParseGames(string[] input)
        {
            var games = new List<Game>();
            foreach (var line in input)
            {
                var gameAndInstructions = line.Split(':');
                var gameId = int.Parse(gameAndInstructions[0][5..]);

                var game = new Game(gameId, []);
                foreach (var instruction in gameAndInstructions[1].Split(';'))
                {
                    var red = CalculateBalls(instruction, "red");
                    var green = CalculateBalls(instruction, "green");
                    var blue = CalculateBalls(instruction, "blue");

                    game.Instructions.Add(new(red, green, blue));
                }

                games.Add(game);
            }

            return games;
        }

        private static int CalculateBalls(string instruction, string colour)
        {
            var endPos = instruction.IndexOf($" {colour}");
            if (endPos > -1)
            {
                var startPos = endPos - 1;
                for (; startPos >= 0; startPos--)
                {
                    if (instruction[startPos] < '0' || instruction[startPos] > '9')
                    {
                        startPos++;
                        break;
                    }
                }

                return int.Parse(instruction[startPos..endPos]);
            }
            else
            {
                return 0;
            }
        }

        private static bool GameIsPossible(Game game, int maxRed, int maxGreen, int maxBlue)
        {
            foreach (var instruction in game.Instructions)
            {
                if (instruction.Red > maxRed || instruction.Green > maxGreen || instruction.Blue > maxBlue)
                {
                    return false;
                }
            }

            return true;
        }

        private static int CalculatePower(Game game) =>
            game.Instructions.Max(x => x.Red) * game.Instructions.Max(x => x.Green) * game.Instructions.Max(x => x.Blue);

        private record Game(int Id, List<Instruction> Instructions);
        private record Instruction(int Red, int Green, int Blue);
    }
}
