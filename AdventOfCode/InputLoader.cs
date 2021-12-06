using System;
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

        public T[] LoadArray<T>(string filePath, string arrayDelimiter = "\r\n")
        {
            var rawInput = LoadInput(filePath);
            var inputStrings = rawInput.Split(arrayDelimiter);

            var output = new T[inputStrings.Length];
            for (var i = 0; i < output.Length; i++)
            {
                output[i] = ConvertType<T>(inputStrings[i]);
            }

            return output;
        }

        private T ConvertType<T>(string input)
        {
            return (T)Convert.ChangeType(input, typeof(T));
        }
    }
}
