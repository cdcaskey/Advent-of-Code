using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public abstract class CodeChallenge : ICodeChallenge
    {
        protected IInputLoader inputLoader;

        public CodeChallenge(IInputLoader loader)
        {
            inputLoader = loader;
        }

        public abstract int Year { get; }

        public abstract int Day { get; }

        protected string inputLocation => $"Inputs\\{Year}\\Day{Day:00}.txt";

        public void Run()
        {
            while (true)
            {
                Console.Write("Part A or B? (or 'q' to quit): ");

                switch (Console.ReadLine().ToUpper())
                {
                    case "A":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Result: ");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine(PartA());
                        Console.ResetColor();
                        break;

                    case "B":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Result: ");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine(PartB());
                        Console.ResetColor();
                        break;

                    case "Q":
                        return;

                    default:
                        Console.Write("Invalid Input - ");
                        break;
                }
            }
        }

        public abstract long PartA();

        public abstract long PartB();
    }
}
