using System;
using StringCalculator;

namespace StringCalculator_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new Calculator();
            var consoleWrapper = new ConsoleWrapper();
            ConsoleApp consoleApp = new ConsoleApp(consoleWrapper, calculator);

            consoleApp.Run();
        }
    }
}
