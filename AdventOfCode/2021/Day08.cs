using System;
using System.Linq;

namespace AdventOfCode._2021
{
    public class Day08 : CodeChallenge
    {
        public Day08(IInputLoader loader) : base(loader) { }

        public override int Year => 2021;

        public override int Day => 8;

        public override long PartA()
        {
            var input = inputLoader.LoadArray<string>(inputLocation);

            var counter = 0;
            foreach (var line in input)
            {
                var output = line.Split('|')[1];

                var segments = output.Split(' ');
                foreach (var segment in segments)
                {
                    if (segment.Length == 2 || segment.Length == 3 || segment.Length == 4 || segment.Length == 7)
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }

        public override long PartB()
        {
            var input = inputLoader.LoadArray<string>(inputLocation);

            var outputs = 0;
            foreach (var line in input)
            {
                outputs += CalculateDigits(line);
            }

            return outputs;
        }

        private int CalculateDigits(string line)
        {
            var pattern = line.Split('|');
            var inputSegments = pattern[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var outputSegments = pattern[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);


            /* The 7 segment display is organised using the signal wire indices as below:
             *
             *  00
             * 1  2
             * 1  2
             *  33
             * 4  5
             * 4  5
             *  66
             */
            var signalWire = new char[7];
            var numbers = new string[10];

            // Find segments of numbers with unique lengths
            numbers[1] = inputSegments.Single(s => s.Length == 2);
            numbers[4] = inputSegments.Single(s => s.Length == 4);
            numbers[7] = inputSegments.Single(s => s.Length == 3);
            numbers[8] = inputSegments.Single(s => s.Length == 7);

            // First, calculte index 0 - this is the one which appears in '7', but not in '1'
            signalWire[0] = numbers[7].Single(c => !numbers[1].Contains(c));

            // Nine is wire with 6 segments, containing all of 4's segments
            numbers[9] = inputSegments.Single(s => s.Length == 6 &&
                                                 s.Intersect(numbers[4]).Count() == 4);

            // Index 4 is the one which appears in 8, but not 9
            signalWire[4] = numbers[8].Single(c => !numbers[9].Contains(c));

            // 2 is the only one with 5 segments, containing index 4
            numbers[2] = inputSegments.Single(s => s.Length == 5 &&
                                                   s.Contains(signalWire[4]));

            // 5 is the one with 5 segments, with 3 overlapping sections with 2
            numbers[5] = inputSegments.Single(s => s.Length == 5 &&
                                                   s.Intersect(numbers[2]).Count() == 3);

            // 3 is the one with 5 segments, with 4 overlapping sections with 2
            numbers[3] = inputSegments.Single(s => s.Length == 5 &&
                                                   s.Intersect(numbers[2]).Count() == 4);

            // 0 is the one with 6 segments, containing all of 1 and 7's segments, and 5 of 9's segments
            numbers[0] = inputSegments.Single(s => s.Length == 6 &&
                                                   s.Intersect(numbers[1]).Count() == 2 &&
                                                   s.Intersect(numbers[7]).Count() == 3 &&
                                                   s.Intersect(numbers[9]).Count() == 5);

            // 6 is the one with 6 segments, containing all of 5's segments, that is not 8 or 9
            numbers[6] = inputSegments.Single(s => s.Length == 6 &&
                                                   s.Intersect(numbers[5]).Count() == 5 &&
                                                   s != numbers[8] &&
                                                   s != numbers[9]);

            // Index 2 is the one which appears in 8, but not 6
            signalWire[2] = numbers[8].Single(c => !numbers[6].Contains(c));

            // Index 3 is the one which appears in 8, but not 0
            signalWire[3] = numbers[8].Single(c => !numbers[0].Contains(c));

            // Index 5 is the one which appears in 1, but not 2
            signalWire[5] = numbers[1].Single(c => !numbers[2].Contains(c));

            // Index 1 is the one which appears in 4, but not 3
            signalWire[1] = numbers[4].Single(c => !numbers[3].Contains(c));

            // Index 6 is the one left
            signalWire[6] = numbers[8].Single(c => !signalWire.Contains(c));

            return ParseNumberFromSegments(outputSegments, signalWire);
        }

        private int ParseNumberFromSegments(string[] outputSegments, char[] wires)
        {
            var digits = string.Empty;
            foreach (var segment in outputSegments)
            {
                var litSegments = 0b0000000;
                for (var i = 0; i < 7;i++)
                {
                    if (segment.Contains(wires[i]))
                    {
                        litSegments |= 0b1 << 6 - i;
                    }
                }

                switch (litSegments)
                {
                    case 0b1110111:
                        digits += "0";
                        break;

                    case 0b0010010:
                        digits += "1";
                        break;

                    case 0b1011101:
                        digits += "2";
                        break;

                    case 0b1011011:
                        digits += "3";
                        break;

                    case 0b0111010:
                        digits += "4";
                        break;

                    case 0b1101011:
                        digits += "5";
                        break;

                    case 0b1101111:
                        digits += "6";
                        break;

                    case 0b1010010:
                        digits += "7";
                        break;

                    case 0b1111111:
                        digits += "8";
                        break;

                    case 0b1111011:
                        digits += "9";
                        break;

                    default:
                        throw new Exception("Unable to get a digit from this signal combo.");
                }
            }

            return int.Parse(digits);
        }
    }
}
