namespace AdventOfCode._2020
{
    public class Day03 : CodeChallenge
    {
        public Day03(IInputLoader loader) : base(loader) { }

        public override long PartA()
        {
            var slope = inputLoader.LoadArray<string>(inputLocation);

            return CalculateTrees(slope, 3, 1);
        }

        public override long PartB()
        {
            var slope = inputLoader.LoadArray<string>(inputLocation);

            var trees = 1;

            trees *= CalculateTrees(slope, 1, 1);
            trees *= CalculateTrees(slope, 3, 1);
            trees *= CalculateTrees(slope, 5, 1);
            trees *= CalculateTrees(slope, 7, 1);
            trees *= CalculateTrees(slope, 1, 2);

            return trees;
        }

        private int CalculateTrees(string[] slope, int xStep, int yStep)
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
