using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2022
{
    public class Day05(IInputLoader loader) : CodeChallenge(loader)
    {
        public override object PartA()
        {
            var input = inputLoader.LoadListOfArrays<string>(InputLocation);

            var stacks = CreateStacks(input[0]);

            foreach (var line in input[1])
            {
                var instruction = ParseInstruction(line);
                PerformInstructionOneAtATime(instruction, stacks);
            }

            var result = string.Empty;
            foreach(var stack in stacks)
            {
                result += stack.Pop();
            }

            return result;
        }

        public override object PartB()
        {
            var input = inputLoader.LoadListOfArrays<string>(InputLocation);

            var stacks = CreateStacks(input[0]);

            foreach (var line in input[1])
            {
                var instruction = ParseInstruction(line);
                PerformInstructionMultipleAtOnce(instruction, stacks);
            }

            var result = string.Empty;
            foreach (var stack in stacks)
            {
                result += stack.Pop();
            }

            return result;
        }

        private static Stack<char>[] CreateStacks(string[] lines)
        {
            // Get number of stacks
            var stackNumbers = lines.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Initialise stacks
            var stacks = new Stack<char>[stackNumbers.Length];
            for (var i = 0; i < stacks.Length; i++)
            {
                stacks[i] = new();
            }

            // Go through the lines one by one filling the stacks
            for (var i = lines.Length - 2; i >= 0; i--)
            {
                for (int x = 1, stack = 0; x < lines[i].Length; x += 4, stack++)
                {
                    if (lines[i][x] != ' ')
                    {
                        stacks[stack].Push(lines[i][x]);
                    }
                }
            }

            return stacks;
        }

        private static Instruction ParseInstruction(string instructionString)
        {
            var instruction = instructionString.Split(new string[] { "move", "from", "to" }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            return new Instruction(int.Parse(instruction[0]), int.Parse(instruction[1]), int.Parse(instruction[2]));
        }

        private static void PerformInstructionOneAtATime(Instruction instruction, Stack<char>[] stacks)
        {
            for (var i = 0; i < instruction.Number; i++)
            {
                stacks[instruction.ToStack - 1].Push(stacks[instruction.FromStack - 1].Pop());
            }
        }

        private static void PerformInstructionMultipleAtOnce(Instruction instruction, Stack<char>[] stacks)
        {
            var movedChars = new Stack<char>();

            for (var i = 0; i < instruction.Number; i++)
            {
                movedChars.Push(stacks[instruction.FromStack - 1].Pop());
            }

            for (var i = 0; i < instruction.Number; i++)
            {
                stacks[instruction.ToStack - 1].Push(movedChars.Pop());
            }
        }

        private record Instruction(int Number, int FromStack, int ToStack);
    }
}