using System;

namespace AdventOfCode._2021
{
    public class Day02 : CodeChallenge
    {
        public Day02(IInputLoader loader) : base(loader) { }

        public override int Year => 2021;

        public override int Day => 2;

        public override long PartA()
        {
            var input = inputLoader.LoadArray<string>(inputLocation);
            var instructions = ParseInstructions(input);

            var horizontal = 0;
            var depth = 0;
            foreach (var instruction in instructions)
            {
                switch (instruction.Direction)
                {
                    case Direction.Forward:
                        horizontal += instruction.Distance;
                        break;

                    case Direction.Up:
                        depth -= instruction.Distance;
                        break;

                    case Direction.Down:
                        depth += instruction.Distance;
                        break;
                }
            }

            return horizontal * depth;
        }

        public override long PartB()
        {
            var input = inputLoader.LoadArray<string>(inputLocation);
            var instructions = ParseInstructions(input);

            var horizontal = 0;
            var aim = 0;
            var depth = 0;

            foreach (var instruction in instructions)
            {
                switch (instruction.Direction)
                {
                    case Direction.Forward:
                        horizontal += instruction.Distance;
                        depth += aim * instruction.Distance;
                        break;

                    case Direction.Up:
                        aim -= instruction.Distance;
                        break;

                    case Direction.Down:
                        aim += instruction.Distance;
                        break;
                }
            }

            return horizontal * depth;
        }

        private enum Direction
        {
            Forward,
            Up,
            Down
        }

        private static (Direction Direction, int Distance)[] ParseInstructions(string[] instructions)
        {
            var parsedInstructions = new (Direction, int)[instructions.Length];
            for (var i = 0; i < instructions.Length; i++)
            {
                var instruction = instructions[i].Split(' ');
                var direction = Enum.Parse<Direction>(instruction[0], true);

                parsedInstructions[i] = (direction, int.Parse(instruction[1]));
            }

            return parsedInstructions;
        }
    }
}
