namespace AdventOfCode._2021
{
    public class Day01 : CodeChallenge
    {
        public Day01(IInputLoader loader) : base(loader) { }

        public override int Year => 2021;

        public override int Day => 1;

        public override long PartA()
        {
            var input = inputLoader.LoadArray<int>(inputLocation);

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

        public override long PartB()
        {
            var input = inputLoader.LoadArray<int>(inputLocation);

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
