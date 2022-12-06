using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2022
{
    public class Day06(IInputLoader loader) : CodeChallenge(loader)
    {
        public override object PartA() => CalculateSequenceStart(4);

        public override object PartB() => CalculateSequenceStart(14);

        private int CalculateSequenceStart(int requiredChars)
        {
            var input = inputLoader.LoadInput(InputLocation);

            var chars = new Queue<char>();
            for (var i = 0; i < requiredChars; i++)
            {
                chars.Enqueue(input[i]);
            }

            for (var i = requiredChars; i < input.Length; i++)
            {
                if (chars.Distinct().Count() == chars.Count)
                {
                    return i;
                }

                chars.Dequeue();
                chars.Enqueue(input[i]);
            }

            throw new Exception("No Sequence Found");
        }
    }
}