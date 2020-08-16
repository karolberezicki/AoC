using System;
using Microsoft.Extensions.Configuration;

namespace AoC
{
    public class Program
    {
        public static Config Config;

        public static void Main()
        {
            Config = GetConfig();
            var solution = GetSolution();
            solution.Execute();
        }

        private static ISolution GetSolution()
        {
            var type = Type.GetType($"{nameof(AoC)}._{Config.Year}.Day{Config.Day:D2}.Day{Config.Day:D2}");
            return Activator.CreateInstance(type!) as ISolution;
        }

        private static Config GetConfig()
        {
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<Program>();
            var config = builder.Build();
            
            return new Config
            {
                ProjectPath = config[nameof(Config.ProjectPath)],
                SessionCookie = config[nameof(Config.SessionCookie)],
                Year = int.Parse(config[nameof(Config.Year)]),
                Day = int.Parse(config[nameof(Config.Day)])
            };
        }
    }
}
