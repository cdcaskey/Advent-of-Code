namespace AdventOfCode._2022
{
    public class Day04(IInputLoader loader) : CodeChallenge(loader)
    {
        public override object PartA()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);

            var overlaps = 0;
            foreach (var pairString in input)
            {
                var pair = ParsePair(pairString);

                if (CompleteOverlap(pair[0], pair[1]))
                {
                    overlaps++;
                }
            }

            return overlaps;
        }

        public override object PartB()
        {
            var input = inputLoader.LoadArray<string>(InputLocation);

            var overlaps = 0;
            foreach (var pairString in input)
            {
                var pair = ParsePair(pairString);

                if (PartialOverlap(pair[0], pair[1]))
                {
                    overlaps++;
                }
            }

            return overlaps;
        }


        private static (int Min, int Max)[] ParsePair(string pairString)
        {
            var pair = new (int, int)[2];

            var elves = pairString.Split(',');
            for (var i = 0; i < elves.Length; i++)
            {
                var range = elves[i].Split('-');
                pair[i].Item1 = int.Parse(range[0]);
                pair[i].Item2 = int.Parse(range[1]);
            }

            return pair;
        }

        private static bool CompleteOverlap((int Min, int Max) value1, (int Min, int Max) value2)
            => (value1.Min >= value2.Min && value1.Max <= value2.Max) || (value2.Min >= value1.Min && value2.Max <= value1.Max);

        private static bool PartialOverlap((int Min, int Max) value1, (int Min, int Max) value2)
            => (value1.Min >= value2.Min && value1.Min <= value2.Max) || (value2.Min >= value1.Min && value2.Min <= value1.Max);
    }
}
