using System.IO;

namespace AdventOfCode
{
    public class InputLoader : IInputLoader
    {
        public string LoadInput(string filePath)
        {
            using var reader = new StreamReader(filePath);
            return reader.ReadToEnd();
        }
    }
}
