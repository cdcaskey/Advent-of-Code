using System;
using System.Collections.Generic;

namespace AdventOfCode._2023
{
    public class Day06(IInputLoader loader) : CodeChallenge(loader)
    {
        public override object PartA()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);
            var races = ParseRaces(input);

            var winningCombos = new List<long>();
            foreach (var race in races)
            {
                winningCombos.Add(RunRace(race).Count);
            }

            var result = 1L;
            for (var i = 0; i < winningCombos.Count; i++)
            {
                result *= winningCombos[i];
            }

            return result;
        }

        public override object PartB()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);
            var race = ParseMegaRace(input);

            return RunRace(race).Count;
        }

        private static List<Race> ParseRaces(string[] input)
        {
            var times = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var distances = input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var races = new List<Race>();
            for (var i = 1; i < times.Length; i++)
            {
                races.Add(new(long.Parse(times[i]), long.Parse(distances[i])));
            }

            return races;
        }

        private static Race ParseMegaRace(string[] input)
        {
            var times = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var distances = input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var time = string.Empty;
            var distance = string.Empty;

            for (var i = 1; i < times.Length; i++)
            {
                time += times[i];
                distance += distances[i];
            }

            return new(long.Parse(time), long.Parse(distance));
        }

        private static List<long> RunRace(Race race)
        {
            var winningTimes = new List<long>();
            for (var i = 0; i < race.Time; i++)
            {
                var stepDistance = i;
                var steps = race.Time - i;
                var distance = stepDistance * steps;

                if (distance > race.MinDistance)
                {
                    winningTimes.Add(i);
                }
            }

            return winningTimes;
        }

        private record Race(long Time, long MinDistance);
    }
}
