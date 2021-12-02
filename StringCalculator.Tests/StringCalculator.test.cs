using NUnit.Framework;
using System;

namespace StringCalculator.test
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator calculator;

        [SetUp]
        public void SetUp()
        {
            calculator = new Calculator();
        }

        [Test]
        public void Add_EmptyString_ReturnsZero()
        {
            int result = calculator.Add("");
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Add_SingleNumber_ReturnsSameNumber()
        {
            int result = calculator.Add("1");
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Add_SeveralNumber_ReturnsSumNumbers()
        {
            int result = calculator.Add("1, 2, 5, 6, 7, 8");
            Assert.AreEqual(29, result);
        }

        [Test]
        public void Add_SeveralDelimiters_ResurnsSumNumbers()
        {
            int result = calculator.Add("1\n2, 1\n 6, 0");
            Assert.AreEqual(10, result);
        }

        [Test]
        public void Add_DefaultDelimiters_ReturnsSumNumbers()
        {
            int result = calculator.Add("//;\n2;5;6;0");
            Assert.AreEqual(13, result);
        }

        [Test]
        public void Add_NegativeNumbers_ReturnsException()
        {
            Exception ex = Assert.Throws<Exception>(() => calculator.Add("-1\n-2, 1\n -6, 0"));

            Assert.AreEqual("negatives not allowed:-1-2-6", ex.Message);
        }

        [Test]
        public void Add_NumbersBigger_ReturnsSumNumbers()
        {
            int result = calculator.Add("1, 1002, 1");
            Assert.AreEqual(2, result);
        }

        [Test]
        public void Add_BigDelimiters_ReturnsSumNumbers()
        {
            int result = calculator.Add("//[***]\n1***2***3");
            Assert.AreEqual(6, result);
        }

        [Test]
        public void Add_SeveralDelimiters_ReturnsSumNumbers()
        {
            int result = calculator.Add("//[%][*]\n1*2%3");
            Assert.AreEqual(6, result);
        }

        [Test]
        public void Add_BigSeveralDelimiters_ReturnsSumNumbers()
        {
            int result = calculator.Add("//[***][%%]\n1%%2***3%%6");
            Assert.AreEqual(12, result);
        }
    }
}