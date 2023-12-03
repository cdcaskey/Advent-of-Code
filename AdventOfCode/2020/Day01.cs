using System;

namespace AdventOfCode._2020
{
    public class Day01(IInputLoader loader) : CodeChallenge(loader)
    {
        public override long PartA()
        {
            var input = inputLoader.LoadArray<int>(InputLocation);

            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < input.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (input[i] + input[j] == 2020)
                    {
                        return input[i] * input[j];
                    }
                }
            }

            throw new Exception("Couldn't find 2 values that add to 2020.");
        }

        public override long PartB()
        {
            var input = inputLoader.LoadArray<int>(InputLocation);

            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < input.Length; j++)
                {
                    for (var k = 0; k < input.Length; k++)
                    {
                        if (input[i] == j || i == k || j == k)
                        {
                            continue;
                        }
                        if (input[i] + input[j] + input[k] == 2020)
                        {
                            return input[i] * input[j] * input[k];
                        }
                    }
                }
            }

            throw new Exception("Couldn't find 3 values that add to 2020.");
        }
    }
}
