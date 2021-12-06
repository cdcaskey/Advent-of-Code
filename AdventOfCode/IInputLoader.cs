namespace AdventOfCode
{
    public interface IInputLoader
    {
        string LoadInput(string filePath);

        T[] LoadArray<T>(string filePath, string arrayDelimiter = "\r\n");
    }
}
