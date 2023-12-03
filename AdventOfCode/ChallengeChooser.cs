using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class ChallengeChooser
    {
        private readonly IEnumerable<ICodeChallenge> challenges;

        public ChallengeChooser(IEnumerable<ICodeChallenge> challenges)
        {
            this.challenges = challenges;
        }

        public void Start()
        {
            ChristmasHeader("AdventOfCode");

            if (OfferTodaysChallenge())
            {
                Console.Clear();
                ChristmasHeader("AdventOfCode");
            }

            while (true)
            {
                var year = SelectYear();

                if (year == null)
                {
                    return;
                }

                while (true)
                {
                    var challenge = SelectChallenge(year.Value);

                    if (challenge == null)
                    {
                        break;
                    }

                    challenge.Run();
                }
            }
        }

        /// <summary>
        /// If the challenge for today has been written, offer it as a shortcut.
        /// </summary>
        /// <returns>Whether today's challenge was offered.</returns>
        private bool OfferTodaysChallenge()
        {
            var today = DateTime.Now.Date;
            var todaysChallenge = challenges.FirstOrDefault(x => x.Year == today.Year && x.Day == today.Day);
            if (todaysChallenge != null)
            {
                Console.Write("Run today's challenge? ('y' or 'n'): ");
                var run = Console.ReadLine();

                if (run.ToUpper().StartsWith('Y'))
                {
                    todaysChallenge.Run();
                }

                Console.WriteLine();
                return true;
            }

            return false;
        }

        private int? SelectYear()
        {
            var availableYears = challenges.Select(c => c.Year)
                                            .Distinct()
                                            .OrderBy(y => y);

            ChristmasWrite("Available Years:\r\n" +
                           "     " + string.Join("\r\n     ", availableYears) + "\r\n",
                           ConsoleColor.DarkGreen,
                           ConsoleColor.Red);
            Console.Write("Select a year (or 'q' to exit): ");

            var yearString = Console.ReadLine();
            var yearParsed = int.TryParse(yearString, out var year);

            while (yearString.ToUpper() != "Q" &&
                   !yearParsed &&
                   !availableYears.Contains(year))
            {
                Console.Write("Invalid selection, Select a year (or 'q' to exit): ");

                yearString = Console.ReadLine();
                yearParsed = int.TryParse(yearString, out year);
            }

            // If a year was parsed, return the year, if not return null indicating a quit
            return yearParsed ? year : null;
        }

        private ICodeChallenge SelectChallenge(int year)
        {
            var challengeDays = challenges.Where(c => c.Year == year)
                                            .Select(c => c.Day)
                                            .OrderBy(d => d);

            Console.WriteLine();
            ChristmasHeader($"  {year}  ");
            var dayString = string.Empty;
            foreach (var day in challengeDays)
            {
                dayString += $"   Day {day:00}\r\n";
            }
            ChristmasWrite(dayString, ConsoleColor.DarkGreen, ConsoleColor.Red);

            while (true)
            {
                Console.Write("Select a challenge to run (or 'q' to quit): ");
                var selectedDayString = Console.ReadLine();
                Console.WriteLine();

                if (selectedDayString.ToUpper() == "Q")
                {
                    return null;
                }

                if (int.TryParse(selectedDayString, out var selectedDay))
                {
                    var challenge = challenges.SingleOrDefault(c => c.Year == year && c.Day == selectedDay);

                    if (challenge != null)
                    {
                        return challenge;
                    }
                }

                Console.Write("Invalid Selection, ");
            }
        }

        private static void ChristmasWrite(string text, ConsoleColor colour1 = ConsoleColor.Red, ConsoleColor colour2 = ConsoleColor.DarkGreen)
        {
            var useColour1 = true;
            foreach (var line in text.Split(Environment.NewLine))
            {
                Console.ForegroundColor = useColour1 ? colour1 : colour2;
                Console.WriteLine(line);
                useColour1 = !useColour1;
            }

            Console.ResetColor();
        }

        private static void ChristmasHeader(string text, ConsoleColor colour1 = ConsoleColor.Red, ConsoleColor colour2 = ConsoleColor.DarkGreen)
        {
            var headerLength = text.Length;
            var textToWrite = new string('=', headerLength + 4) + $"\r\n  {text}  \r\n" + new string('=', headerLength + 4);
            ChristmasWrite(textToWrite, colour1, colour2);
        }
    }
}
