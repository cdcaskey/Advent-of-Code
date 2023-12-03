namespace AdventOfCode._2020
{
    public class Day03(IInputLoader loader) : CodeChallenge(loader)
    {
        public override object PartA()
        {
            var slope = inputLoader.LoadArray<string>(InputLocation);

            return CalculateTrees(slope, 3, 1);
        }

        public override object PartB()
        {
            var slope = inputLoader.LoadArray<string>(InputLocation);

            var trees = 1;

            trees *= CalculateTrees(slope, 1, 1);
            trees *= CalculateTrees(slope, 3, 1);
            trees *= CalculateTrees(slope, 5, 1);
            trees *= CalculateTrees(slope, 7, 1);
            trees *= CalculateTrees(slope, 1, 2);

            return trees;
        }

        private static int CalculateTrees(string[] slope, int xStep, int yStep)
        {
            var trees = 0;
            for (int x = 0, y = 0; y < slope.Length; x += xStep, y += yStep)
            {
                if (x >= slope[y].Length)
                {
                    x -= slope[y].Length;
                }

                if (slope[y][x] == '#')
                {
                    trees++;
                }
            }

            return trees;
        }
    }
}
