using System;

namespace AdventOfCode._2021
{
    public class Day01 : CodeChallenge
    {
        private const string inputLocation = "Inputs\\2021\\Day01.txt";

        public Day01(IInputLoader loader) : base(loader) { }

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
            var input = inputLoader.LoadArray<int>(inputLocation);

            var result = 0;
            for (var i = 1; i < input.Length; i++)
            {
                if (input[i] > input[i-1])
                {
                    result++;
                }
            }

            return result;
        }

        public static int PartB()
        {
            var input = inputLoader.LoadArray<int>(inputLocation);

            var result = 0;
            var previousMeasurement = int.MaxValue;
            for (var i = 2; i < input.Length; i++)
            {
                var measurement = input[i] + input[i - 1] + input[i - 2];

                if (measurement > previousMeasurement)
                {
                    result++;
                }

                previousMeasurement = measurement;
            }

            return result;
        }
    }
}
