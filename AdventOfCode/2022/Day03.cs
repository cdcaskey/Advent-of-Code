using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2022
{
    public class Day03(IInputLoader loader) : CodeChallenge(loader)
    {
        public override int Year => 2022;

        public override int Day => 3;

        public override long PartA()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);

            var priority = 0;
            foreach (var line in input)
            {
                var length = line.Length;
                var part1 = line[..(length / 2)];
                var part2 = line[(length / 2)..];

                // Use first instead of single as compartment can have same thing multiple times
                var repeatedChar = part1.First(x => part2.Contains(x));

                if (repeatedChar > 'Z')
                {
                    priority += repeatedChar - 'a' + 1;
                }
                else
                {
                    priority += repeatedChar - 'A' + 27;
                }
            }

            return priority;
        }

        public override long PartB()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);

            var priority = 0;
            for (var i = 0; i < input.Length; i += 3)
            {
                var commonChar = input[i].First(x => input[i + 1].Contains(x) && input[i + 2].Contains(x));

                if (commonChar > 'Z')
                {
                    priority += commonChar - 'a' + 1;
                }
                else
                {
                    priority += commonChar - 'A' + 27;
                }
            }

            return priority;
        }
    }
}
