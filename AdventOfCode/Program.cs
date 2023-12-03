using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace AdventOfCode
{
    class Program
    {
        static ServiceProvider provider;

        static void Main()
        {
            provider = RegisterServices();

            var challengeChooser = new ChallengeChooser(provider.GetServices<ICodeChallenge>());
            challengeChooser.Start();
        }

        static ServiceProvider RegisterServices()
        {
            var serviceProvider = new ServiceCollection();

            var assembly = Assembly.GetEntryAssembly();
            var challenges = assembly.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(ICodeChallenge)));

            foreach (var challenge in challenges)
            {
                if (!challenge.IsAbstract)
                {
                    serviceProvider.AddTransient(typeof(ICodeChallenge), challenge);
                }
            }

            serviceProvider.AddTransient<IInputLoader, InputLoader>();

            return serviceProvider.BuildServiceProvider();
        }
    }
}
