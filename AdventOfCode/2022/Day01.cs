using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2022
{
    public class Day01(IInputLoader loader) : CodeChallenge(loader)
    {
        public override object PartA()
        {
            var input = inputLoader.LoadListOfArrays<int>(InputLocation);

            var elfCalories = new List<int>();
            foreach (var elf in input)
            {
                elfCalories.Add(elf.Sum());
            }

            return elfCalories.Max();
        }

        public override object PartB()
        {
            var input = inputLoader.LoadListOfArrays<int>(InputLocation);

            var elfCalories = new List<int>();
            foreach (var elf in input)
            {
                elfCalories.Add(elf.Sum());
            }

            return elfCalories.OrderByDescending(x => x)
                              .Take(3)
                              .Sum();
        }
    }
}
