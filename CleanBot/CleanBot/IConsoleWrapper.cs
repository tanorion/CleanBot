using System;

namespace CleanBot
{
    public interface IConsoleWrapper
    {
        string ReadLine();
    }

    public class ConsoleWrapper : IConsoleWrapper
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}