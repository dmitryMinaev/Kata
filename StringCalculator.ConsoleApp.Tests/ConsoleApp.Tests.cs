using NUnit.Framework;
using Moq;
using Moq.Protected;
using Moq.Internals;
using Moq.Language;
using StringCalculator_ConsoleApp;
using StringCalculator;

namespace ConsoleApp_Tests
{
    public class Tests
    {
        private ConsoleApp CreateConsoleApp(IConsole console, Calculator calculator)
        {
            return new ConsoleApp(console, calculator);
        }

        private Calculator CreateCalculator()
        {
            return new Calculator();
        }

        private ConsoleApp consoleApp;
        private Mock<IConsole> mockConsole;

        [SetUp]
        public void SetUp()
        {
            mockConsole = new Mock<IConsole>();

            var calculator = CreateCalculator();
            consoleApp = CreateConsoleApp(mockConsole.Object, calculator);
        }

        [Test]
        public void Run_WorkReadLine1()
        {
            mockConsole.Setup(r => r.ReadLine())
                .Returns("");

            consoleApp.Run();

            mockConsole.Verify(x => x.ReadLine(), Times.Exactly(1));
        }

        [Test]
        public void Run_WorkReadLine2()
        {
            mockConsole.SetupSequence(r => r.ReadLine())
                .Returns("1,2,3,4,5")
                .Returns("");

            consoleApp.Run();

            mockConsole.Verify(x => x.ReadLine(), Times.Exactly(2));
        }

        [Test]
        public void Run_WriteLineResultSumNumbers()
        {
            mockConsole.SetupSequence(r => r.ReadLine())
                .Returns("1,2,3")
                .Returns("");

            consoleApp.Run();

            mockConsole.Verify(x => x.WriteLine(It.Is<string>(t => t == "Result:6")), Times.Once());
        }

        [Test]
        public void Run_StartWriteMessage()
        {
            mockConsole.SetupSequence(r => r.ReadLine())
                .Returns("1,2,3")
                .Returns("");

            consoleApp.Run();

            mockConsole.Verify(x => x.Write(It.Is<string>(t => t == "Enter comma separated numbers (enter to exit): ")), Times.Once());
        }

        [Test]
        public void Run_WriteMessageToCycle()
        {
            mockConsole.SetupSequence(r => r.ReadLine())
                .Returns("1,2,3,5")
                .Returns("1,2")
                .Returns("");

            consoleApp.Run();

            mockConsole.Verify(x => x.Write(It.Is<string>(t => t == "you can enter other numbers (enter to exit)?\n>> ")), Times.Exactly(2));
        }
    }
}