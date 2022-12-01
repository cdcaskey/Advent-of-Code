using System.Collections.Generic;

namespace AdventOfCode
{
    public interface IInputLoader
    {
        string LoadInput(string filePath);

        T[] LoadArray<T>(string filePath, string arrayDelimiter = "\r\n");

        List<T[]> LoadListOfArrays<T>(string filePath, string arrayDelimiter = "\r\n", string listDelimiter = "\r\n\r\n");
    }
}
