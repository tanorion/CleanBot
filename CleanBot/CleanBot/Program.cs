using System;
using Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddTransient<IConsoleWrapper, ConsoleWrapper>()
                .AddTransient<IInstructor, ConsoleInstructor>()
                .AddTransient<ICleaningBot, CleaningBot>()
                .AddTransient<ICleaningService, CleaningService>()
                .BuildServiceProvider();

            //configure console logging
            serviceProvider
                .GetService<ILoggerFactory>();
             

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            //do the actual work here
            var cleaningService = serviceProvider.GetService<ICleaningService>();
            cleaningService.Clean();
            Console.WriteLine($"Cleaned: {cleaningService.GetTilesCleaned()}");
            logger.LogDebug("All is clean");

        }
    }
}
