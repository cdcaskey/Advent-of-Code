using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public class Day04(IInputLoader loader) : CodeChallenge(loader)
    {
        public override object PartA()
        {
            var input = inputLoader.LoadArray<string>(InputLocation, "\r\n\r\n");
            var fieldKeys = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

            var validPassports = 0;
            foreach (var item in input)
            {
                var valid = true;
                foreach (var key in fieldKeys)
                {
                    if (!item.Contains($"{key}:"))
                    {
                        valid = false;
                    }
                }

                if (valid)
                {
                    validPassports++;
                }
            }

            return validPassports;
        }

        public override object PartB()
        {
            var input = inputLoader.LoadArray<string>(InputLocation, "\r\n\r\n");
            var keys = new Dictionary<string, Func<string, bool>>()
            {
                { "byr", v => ValidateNumber(v, 1920, 2002) },
                { "iyr", v => ValidateNumber(v, 2010, 2020) },
                { "eyr", v => ValidateNumber(v, 2020, 2030) },
                { "hgt", v => ValidateHeight(v) },
                { "hcl", v => Regex.IsMatch(v, @"^#[0-9a-f]{6}$") },
                { "ecl", v => Regex.IsMatch(v, @"^(amb|blu|brn|gry|grn|hzl|oth)$") },
                { "pid", v => Regex.IsMatch(v, @"^[0-9]{9}$") },
            };

            var validPassports = 0;
            foreach (var entry in input)
            {
                var valid = true;
                foreach (var key in keys)
                {
                    var regex = Regex.Match(entry, $@"{key.Key}:(\S+)");

                    if (!regex.Success)
                    {
                        valid = false;
                        break;
                    }

                    if (!key.Value(regex.Groups[1].Value))
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid)
                {
                    validPassports++;
                }
            }

            return validPassports;
        }

        private static bool ValidateNumber(string input, int minValid, int maxValid)
        {
            if (!int.TryParse(input, out var number))
            {
                return false;
            }

            return number >= minValid && number <= maxValid;
        }

        private static bool ValidateHeight(string input)
        {
            var height = Regex.Match(input, @"^(\d+)(cm|in)$");
            if (!height.Success)
            {
                return false;
            }

            return height.Groups[2].Value switch
            {
                "cm" => ValidateNumber(height.Groups[1].Value, 150, 193),
                "in" => ValidateNumber(height.Groups[1].Value, 59, 76),
                _ => false,
            };
        }
    }
}
