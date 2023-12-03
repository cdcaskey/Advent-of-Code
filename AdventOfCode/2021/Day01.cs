namespace AdventOfCode._2021
{
    public class Day01(IInputLoader loader) : CodeChallenge(loader)
    {
        public override object PartA()
        {
            var input = inputLoader.LoadArray<int>(InputLocation);

            var result = 0;
            for (var i = 1; i < input.Length; i++)
            {
                if (input[i] > input[i - 1])
                {
                    result++;
                }
            }

            return result;
        }

        public override object PartB()
        {
            var input = inputLoader.LoadArray<int>(InputLocation);

            var result = 0;
            var previousMeasurement = int.MaxValue;
            for (var i = 2; i < input.Length; i++)
            {
                var measurement = input[i] + input[i - 1] + input[i - 2];

                if (measurement > previousMeasurement)
                {
                    result++;
                }

                previousMeasurement = measurement;
            }

            return result;
        }
    }
}
