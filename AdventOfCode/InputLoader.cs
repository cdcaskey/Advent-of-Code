using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public char[,] LoadGridofChars(string filePath, out int maxX, out int maxY, char fillChar = ' ', string arrayDelimiter = "\r\n")
        {
            var input = LoadArray<string>(filePath, arrayDelimiter);

            maxX = input.Length;
            maxY = input.Max(x => x.Length);

            var grid = new char[input.Max(x => x.Length), input.Length];
            for (var y = 0; y < maxY; y++)
            {
                for (var x = 0; x < maxX; x++)
                {
                    if (x >= input[y].Length)
                    {
                        grid[x, y] = fillChar;
                    }
                    else
                    {
                        grid[x, y] = input[y][x];
                    }
                }
            }

            return grid;
        }
    }
}
