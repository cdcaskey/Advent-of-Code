using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public class Day02(IInputLoader loader) : CodeChallenge(loader)
    {
        public override long PartA()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);

            var validPasswords = 0;
            foreach (var line in input)
            {
                var regex = Regex.Match(line, @"(\d+)-(\d+) ([a-z]): ([a-z]+)");

                var minTimes = int.Parse(regex.Groups[1].Value);
                var maxTimes = int.Parse(regex.Groups[2].Value);
                var letter = regex.Groups[3].Value[0];
                var password = regex.Groups[4].Value;

                var letterOccurances = password.Count(c => c == letter);

                if (letterOccurances >= minTimes && letterOccurances <= maxTimes)
                {
                    validPasswords++;
                }
            }

            return validPasswords;
        }

        public override long PartB()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);

            var validPasswords = 0;
            foreach (var line in input)
            {
                var regex = Regex.Match(line, @"(\d+)-(\d+) ([a-z]): ([a-z]+)");

                var pos1 = int.Parse(regex.Groups[1].Value) - 1;
                var pos2 = int.Parse(regex.Groups[2].Value) - 1;
                var letter = regex.Groups[3].Value[0];
                var password = regex.Groups[4].Value;

                if (password[pos1] == letter ^ password[pos2] == letter)
                {
                    validPasswords++;
                }
            }

            return validPasswords;
        }
    }
}
