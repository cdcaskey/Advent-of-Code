using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023
{
    public class Day01 : CodeChallenge
    {
        private readonly Dictionary<string, string> spelledNumbers = new()
        {
            { "one", "1" },
            { "two", "2" },
            { "three", "3" },
            { "four", "4" },
            { "five", "5" },
            { "six", "6" },
            { "seven", "7" },
            { "eight", "8" },
            { "nine", "9" }
        };

        public Day01(IInputLoader loader) : base(loader) { }

        public override int Year => 2023;

        public override int Day => 1;

        public override long PartA()
        {
            var input = inputLoader.LoadArray<string>(inputLocation);
            return CalculateCalibration(input, false);
        }

        public override long PartB()
        {
            var input = inputLoader.LoadArray<string>(inputLocation);
            return CalculateCalibration(input, true);
        }

        private int CalculateCalibration(string[] input, bool includeSpelledNumbers)
        {
            var result = 0;

            foreach (var item in input)
            {
                var numbers = string.Empty;
                for (var i = 0; i < item.Length; i++)
                {
                    var number = CalculateNumber(item[i..], includeSpelledNumbers);
                    if (number != null)
                    {
                        numbers += number;
                        break;
                    }
                }

                for (var i = item.Length - 1; i >= 0; i--)
                {
                    var number = CalculateNumber(item[i..], includeSpelledNumbers);
                    if (number != null)
                    {
                        numbers += number;
                        break;
                    }
                }

                result += int.Parse($"{numbers[0]}{numbers[^1]}");
            }

            return result;
        }

        private string CalculateNumber(string input, bool includeSpelledNumbers)
        {
            if (input[0] >= '0' && input[0] <= '9')
            {
                return $"{input[0]}";
            }
            else if (includeSpelledNumbers)
            {
                foreach (var number in spelledNumbers)
                {
                    if (input.StartsWith(number.Key))
                    {
                        return number.Value;
                    }
                }
            }

            return null;
        }
    }
}