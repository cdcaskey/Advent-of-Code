using System;

namespace AdventOfCode._2021
{
    public class Day02 : CodeChallenge
    {
        private const string inputLocation = "Inputs\\2021\\Day02.txt";

        public Day02(IInputLoader loader) : base(loader) { }

        public static void Main()
        {
            while (true)
            {
                Console.Write("Part A or B? (or 'q' to quit): ");

                switch(Console.ReadLine().ToUpper())
                {
                    case "A":
                        Console.WriteLine($"Result: {PartA()}");
                        break;

                    case "B":
                        Console.WriteLine($"Result: {PartB()}");
                        break;

                    case "Q":
                        return;

                    default:
                        Console.Write("Invalid Input - ");
                        break;
                }
            }
        }

        public static int PartA()
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

        public static int PartB()
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
