using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public class Day06 : CodeChallenge
    {
        private const string inputLocation = "Inputs\\2021\\Day06.txt";

        public Day06(IInputLoader loader) : base(loader) { }

        public static void Main()
        {
            while (true)
            {
                Console.Write("Part A or B? (or 'q' to quit): ");

                switch (Console.ReadLine().ToUpper())
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

        public static long PartA() => Simulate(80);

        public static long PartB() => Simulate(256);

        private static long Simulate(int days)
        {
            var fish = new Dictionary<int, long>();
            for (var i = 0; i <= 8; i++)
            {
                fish[i] = 0;
            }

            var startFish = inputLoader.LoadArray<int>(inputLocation, ",");
            foreach (var f in startFish)
            {
                fish[f]++;
            }

            for (var day = 0; day < days; day++)
            {
                var fishToAdd = fish[0];
                for (var i = 1; i <= 8; i++)
                {
                    fish[i - 1] = fish[i];
                }

                fish[6] += fishToAdd;
                fish[8] = fishToAdd;
            }

            return fish.Sum(x => x.Value);
        }
    }
}