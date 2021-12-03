using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    /// <summary>
    /// Class to load the code challenges, based on Jon Skeet's DemoUtil project:
    /// <see href="https://github.com/jskeet/DemoCode/tree/main/DemoUtil"/>
    /// </summary>
    public class ApplicationChooser
    {
        /// <summary>
        /// Displays entry points and prompts the user to choose one.
        /// The assembly to find types in is the entry assembly.
        /// </summary>
        /// <param name="args">Arguments to pass in for methods which have a single string array parameter.</param>
        public static void Run(string[] args) => Run(Assembly.GetEntryAssembly(), args);

        /// <summary>
        /// Displays entry points and prompts the user to choose one.
        /// The assembly to find types in is the entry assembly.
        /// An empty set of arguments will be passed to any Main method
        /// with a string array parameter
        /// </summary>
        public static void Run() => Run(Assembly.GetEntryAssembly(), new string[0]);

        /// <summary>
        /// Displays entry points and prompts the user to choose one.
        /// </summary>
        /// <param name="type">Type within the assembly containing the applications. This type is
        /// not included in the list of entry points to run.</param>
        /// <param name="args">Arguments to pass in for methods which have a single string array parameter.</param>
        public static void Run(Type type, string[] args) =>
            Run(type.GetTypeInfo().Assembly, args);

        private static void Run(Assembly assembly, string[] args)
        {
            ChristmasHeader("AdventOfCode");

            var year = SelectYear(assembly);

            if (year.ToUpper() == "Q")
            {
                return;
            }

            OpenChallenges(assembly, year, args);
            Run(assembly, args);
        }

        private static string SelectYear(Assembly assembly)
        {
            var namespaces = assembly.DefinedTypes
                .Select(t => t.Namespace)
                .Where(n => n != null)
                .Distinct()
                .OrderBy(x => x);

            var years = new List<string>();

            foreach (var item in namespaces)
            {
                var regex = Regex.Match(item, @"AdventOfCode\._(\d{4})");
                if (regex.Success)
                {
                    years.Add(regex.Groups[1].Value);
                }
            }

            Console.WriteLine("Available Years:");
            ChristmasWrite("    " + string.Join("\r\n    ", years) + "\r\n");
            Console.Write("Select a year (or 'q' to exit): ");
            var year = Console.ReadLine();

            while (!years.Contains(year) && year.ToUpper() != "Q")
            {
                Console.Write("Invalid selection, Select a year (or 'q' to exit): ");
                year = Console.ReadLine();
            }

            return year;
        }

        private static void OpenChallenges(Assembly assembly, string year, string[] args)
        {
            var challenges = assembly.DefinedTypes
                .Where(t => t.Namespace != null && t.Namespace.Contains(year))
                .Select(t => GetEntryPoint(t))
                .Where(ep => ep != null && ep != assembly.EntryPoint)
                .OrderBy(ep => GetDescription(ep.DeclaringType))
                .ThenBy(ep => ep.DeclaringType.Name)
                .ToList();

            if (challenges.Count == 0)
            {
                Console.WriteLine("No challenges for this year. Press return to exit.");
                Console.ReadLine();
                return;
            }

            var day = string.Empty;
            MethodBase entryPoint = null;
            do
            {
                Console.WriteLine();
                Console.WriteLine();

                ChristmasHeader($"  {year}  ");
                foreach (var challenge in challenges)
                {
                    Console.WriteLine("   " + GetEntryPointName(challenge).Replace("day", "Day ", StringComparison.InvariantCultureIgnoreCase));
                }

                Console.Write("\r\nSelect a challenge to run (or 'q' to quit): ");
                day = Console.ReadLine();

                if (day.ToUpper() == "Q")
                {
                    return;
                }

                if (int.TryParse(day, out _) && day.Length == 1)
                {
                    day = "0" + day;
                }

                var selectedChallenge = challenges.FirstOrDefault(c => c.DeclaringType.Name.Contains(day));
                if (selectedChallenge == null)
                {
                    Console.WriteLine("Invalid choice.");
                }
                else
                {
                    entryPoint = selectedChallenge;
                }
            } while (entryPoint == null);

            try
            {
                MethodBase main = entryPoint;
                Task task = main.Invoke(null, main.GetParameters().Length == 0 ? null : new object[] { args }) as Task;
                if (task != null)
                {
                    task.GetAwaiter().GetResult();
                }
            }
            catch (Exception e)
            {
                // Normally we fail due to an exception within the
                // code invoked via reflection.
                // Unwrap the TargetInvocationException that would otherwise
                // be wrapped in.
                if (e is TargetInvocationException tie)
                {
                    e = tie.InnerException;
                }
                Console.WriteLine("Exception: {0}", e);
            }

            Console.WriteLine("Complete.");
            Console.ReadLine();
            OpenChallenges(assembly, year, args);
        }

        private static string GetEntryPointName(MethodBase methodBase)
        {
            Type type = methodBase.DeclaringType;
            string description = GetDescription(type);
            return description == null ? type.Name : $"[{description}] {type.Name}";
        }

        private static string GetDescription(Type type) =>
            type.GetTypeInfo()
                .GetCustomAttributes<DescriptionAttribute>()
                .FirstOrDefault()?.Description;

        /// <summary>
        /// Returns the entry point for a method, or null if no entry points can be used.
        /// An entry point taking string[] is preferred to one with no parameters.
        /// </summary>
        internal static MethodBase GetEntryPoint(TypeInfo type)
        {
            if (type.IsGenericTypeDefinition || type.IsGenericType)
            {
                return null;
            }

            var methods = type.DeclaredMethods
                .Where(m => m.IsStatic && m.Name == "Main" && !m.IsGenericMethodDefinition);

            MethodInfo parameterless = null;
            MethodInfo stringArrayParameter = null;

            foreach (MethodInfo method in methods)
            {
                ParameterInfo[] parameters = method.GetParameters();
                if (parameters.Length == 0)
                {
                    parameterless = method;
                }
                else
                {
                    if (parameters.Length == 1 &&
                        !parameters[0].IsOut &&
                        !parameters[0].IsOptional &&
                        parameters[0].ParameterType == typeof(string[]))
                    {
                        stringArrayParameter = method;
                    }
                }
            }

            // Prefer the version with parameters, return null if neither have been found
            return stringArrayParameter ?? parameterless;
        }

        private static void ChristmasWrite(string text, ConsoleColor colour1 = ConsoleColor.Red, ConsoleColor colour2 = ConsoleColor.DarkGreen)
        {
            bool useColour1 = true;
            foreach (var line in text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
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
            ChristmasWrite(textToWrite);
        }
    }
}