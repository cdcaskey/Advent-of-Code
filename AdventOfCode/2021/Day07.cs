using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public class Day07 : CodeChallenge
    {
        public Day07(IInputLoader loader) : base(loader) { }

        public override long PartA()
        {
            var input = inputLoader.LoadArray<int>(inputLocation, ",");

            var crabs = new Dictionary<int, int>();
            foreach (var crab in input)
            {
                if (crabs.TryGetValue(crab, out var existingNumber))
                {
                    crabs[crab]++;
                }
                else
                {
                    crabs[crab] = 1;
                }
            }

            var minFuel = int.MaxValue;
            for (var target = crabs.Keys.Min(); target <= crabs.Keys.Max(); target++)
            {
                var fuelToReachTarget = 0;
                foreach (var crab in crabs)
                {
                    fuelToReachTarget += Math.Abs(crab.Key - target) * crab.Value;
                }

                if (fuelToReachTarget < minFuel)
                {
                    minFuel = fuelToReachTarget;
                }
            }

            return minFuel;
        }

        public override long PartB()
        {
            var input = inputLoader.LoadArray<int>(inputLocation, ",");

            var crabs = new Dictionary<int, int>();
            foreach (var crabGroup in input)
            {
                if (crabs.TryGetValue(crabGroup, out var existingNumber))
                {
                    crabs[crabGroup]++;
                }
                else
                {
                    crabs[crabGroup] = 1;
                }
            }

            var minFuel = int.MaxValue;
            for (var target = crabs.Keys.Min(); target <= crabs.Keys.Max(); target++)
            {
                var fuelToReachTarget = 0;
                foreach (var crabGroup in crabs)
                {
                    var distanceToTarget = Math.Abs(crabGroup.Key - target);
                    var fuelForDistance = distanceToTarget * (distanceToTarget + 1) / 2;

                    fuelToReachTarget += fuelForDistance * crabGroup.Value;
                }

                if (fuelToReachTarget < minFuel)
                {
                    minFuel = fuelToReachTarget;
                }
            }

            return minFuel;
        }
    }
}