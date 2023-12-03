namespace AdventOfCode
{
    public interface ICodeChallenge
    {
        int Year { get; }

        int Day { get; }

        void Run();

        object PartA();

        object PartB();
    }
}
