using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2022
{
    public class Day01 : CodeChallenge
    {
        public Day01(IInputLoader loader) : base(loader) { }

        public override int Year => 2022;

        public override int Day => 1;

        public override long PartA()
        {
            var input = inputLoader.LoadListOfArrays<int>(inputLocation);

            var elfCalories = new List<int>();
            foreach (var elf in input)
            {
                elfCalories.Add(elf.Sum());
            }

            return elfCalories.Max();
        }

        public override long PartB()
        {
            var input = inputLoader.LoadListOfArrays<int>(inputLocation);

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
