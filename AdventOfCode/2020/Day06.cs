using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    public class Day06 : CodeChallenge
    {
        public Day06(IInputLoader loader) : base(loader) { }

        public override int Year => 2020;

        public override int Day => 6;

        public override long PartA()
        {
            var input = inputLoader.LoadArray<string>(inputLocation, "\r\n\r\n");

            var count = 0;
            foreach(var group in input)
            {
                for (var question = 'a'; question <= 'z'; question++)
                {
                    if (group.Contains(question))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public override long PartB()
        {
            var input = inputLoader.LoadArray<string>(inputLocation, "\r\n\r\n");

            var count = 0;
            foreach (var group in input)
            {
                var people = group.Split("\r\n");
                var possibleCharacters = people[0].Distinct().ToList();

                for (var i = 1; i < people.Length; i++)
                {
                    var noLongerPossibleCharacters = new List<char>();
                    foreach (var character in possibleCharacters)
                    {
                        if (!people[i].Contains(character))
                        {
                            noLongerPossibleCharacters.Add(character);
                        }
                    }

                    possibleCharacters.RemoveAll(c => noLongerPossibleCharacters.Contains(c));
                }

                count += possibleCharacters.Count;
            }

            return count;
        }
    }
}