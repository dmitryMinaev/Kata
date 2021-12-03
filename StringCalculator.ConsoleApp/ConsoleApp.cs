using System;
using StringCalculator;

namespace StringCalculator_ConsoleApp
{
    public class ConsoleApp
    {
        private readonly IConsole console;
        private readonly Calculator calculator;

        public ConsoleApp(IConsole console, Calculator calculator)
        {
            this.console = console;
            this.calculator = calculator;
        }

        public void Run()
        {
            console.Write("Enter comma separated numbers (enter to exit): ");

            while (true)
            {
                string numbers = console.ReadLine();

                if (numbers == "")
                {
                    break;
                }

                int result = calculator.Add(numbers);

                console.WriteLine($"Result:{result}");

                console.Write("you can enter other numbers (enter to exit)?\n>> ");
            }
        }
    }
}
