using System;
using System.Collections.Generic;
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
            return BuildArray<T>(rawInput);
        }

        public List<T[]> LoadListOfArrays<T>(string filePath, string arrayDelimiter = "\r\n", string listDelimiter = "\r\n\r\n")
        {
            var rawInput = LoadInput(filePath);
            var groups = rawInput.Split(listDelimiter);

            var output = new List<T[]>();
            foreach (var array in groups)
            {
                output.Add(BuildArray<T>(array, arrayDelimiter));
            }

            return output;
        }

        private static T ConvertType<T>(string input) => (T)Convert.ChangeType(input, typeof(T));

        private static T[] BuildArray<T>(string rawInput, string arrayDelimiter = "\r\n")
        {
            var inputStrings = rawInput.Split(arrayDelimiter);

            var output = new T[inputStrings.Length];
            for (var i = 0; i < output.Length; i++)
            {
                output[i] = ConvertType<T>(inputStrings[i]);
            }

            return output;
        }
    }
}
