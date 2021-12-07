using System;
using System.Linq;

namespace AdventOfCode._2021
{
    public class Day03 : CodeChallenge
    {
        public Day03(IInputLoader loader) : base(loader) { }

        public override int Year => 2021;

        public override int Day => 3;

        public override long PartA()
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

        public override long PartB()
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
