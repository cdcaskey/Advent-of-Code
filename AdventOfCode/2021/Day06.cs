using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2021
{
    public class Day06(IInputLoader loader) : CodeChallenge(loader)
    {
        public override long PartA() => Simulate(80);

        public override long PartB() => Simulate(256);

        private long Simulate(int days)
        {
            var fish = new Dictionary<int, long>();
            for (var i = 0; i <= 8; i++)
            {
                fish[i] = 0;
            }

            var startFish = inputLoader.LoadArray<int>(InputLocation, ",");
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