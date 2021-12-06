using System;
using System.Linq;

namespace AdventOfCode._2021
{
    public class Day03 : CodeChallenge
    {
        private const string inputLocation = "Inputs\\2021\\Day03.txt";

        public Day03(IInputLoader loader) : base(loader) { }

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
            var report = inputLoader.LoadArray<string>(inputLocation);

            var gammaBits = string.Empty;
            var epsilonBits = string.Empty;
            for (var i = 0; i < report[0].Length; i++)
            {
                var bitAverage = report.Select(r => r[i])
                                      .Average(r => r - '0');

                var commonBit = Convert.ToBoolean(Math.Round(bitAverage));

                gammaBits += commonBit ? "1" : "0";
                epsilonBits += commonBit ? "0" : "1";
            }

            var gammaValue = Convert.ToInt32(gammaBits, 2);
            var epsilonValue = Convert.ToInt32(epsilonBits, 2);

            return gammaValue * epsilonValue;
        }

        public static int PartB()
        {
            // Calculate Oxygen Generator Rating
            var report = inputLoader.LoadArray<string>(inputLocation).ToList();
            for (var i = 0; report.Count > 1; i++)
            {
                var bitAverage = report.Select(r => r[i])
                                      .Average(r => r - '0');

                var commonBit = (int)Math.Round(bitAverage, MidpointRounding.AwayFromZero);

                report.RemoveAll(r => r[i] != commonBit + '0');
            }

            var oxygenRating = report.Single();

            // Calculate CO2 Scrubber Rating
            report = inputLoader.LoadArray<string>(inputLocation).ToList();
            for (var i = 0; report.Count > 1; i++)
            {
                var bitAverage = report.Select(r => r[i])
                                      .Average(r => r - '0');

                var commonBit = (int)Math.Round(bitAverage, MidpointRounding.AwayFromZero);

                report.RemoveAll(r => r[i] == commonBit + '0');
            }

            var scrubberRating = report.Single();

            // Calculate result from Oxygen Generator and CO2 Scrubber ratings
            var oxygenValue = Convert.ToInt32(oxygenRating, 2);
            var scrubberValue = Convert.ToInt32(scrubberRating, 2);

            return oxygenValue * scrubberValue;
        }
    }
}
