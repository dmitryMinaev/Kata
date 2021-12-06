using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        public int Add(string inputString)
        {
            if (inputString == string.Empty)
            {
                return 0;
            }

            if (inputString.Contains(@"\n"))
            {
                inputString = inputString.Replace(@"\n", "\n");
            }

            string formatInputString = TruncateInputString(inputString);

            string[] delimiters = FindDelimiters(inputString);

            IEnumerable<int> numbers = ConvertStringToNumbers(formatInputString, delimiters);

            CheckQueryNegativeValues(numbers);

            return numbers.Where(n => n < 1000).Sum();
        }

        private string[] FindDelimiters(string inputString)
        {
            if (inputString.StartsWith("//["))
            {
                string[] arrDelimiters = CutStringToDelimiters(inputString);
                return arrDelimiters;
            }
            else if (inputString.StartsWith("//"))
            {
                const int startIndexDelimiters = 2;
                return new string[] { inputString.Substring(startIndexDelimiters, 1) };
            }

            return new string[] { ",", "\n" };
        }

        private string[] CutStringToDelimiters(string inputString)
        {
            const int startIndexDelimiters = 3;
            const int sizeEndStr = 4;
            int lengthDelimiters = inputString.IndexOf('\n') - sizeEndStr;

            string stringDelimiters = inputString.Substring(startIndexDelimiters, lengthDelimiters);

            string[] arrDelimiters = stringDelimiters.Split("][");

            return arrDelimiters;
        }

        private void CheckQueryNegativeValues(IEnumerable<int> numbers)
        {
            if (numbers.Any(i => i < 0))
            {
                string messageException = string.Join(" ", numbers.Where(n => n < 0));

                throw new Exception(string.Format("Negatives not allowed: {0}", messageException));
            }
        }

        private string TruncateInputString(string inputString)
        {
            if(inputString.StartsWith("//[") || inputString.StartsWith("//"))
            {
                const int sizeEndDelimiters = 1;
                int startIndexNumber = inputString.IndexOf('\n') + sizeEndDelimiters;
                return inputString.Substring(startIndexNumber);
            }

            return inputString;
        }

        private IEnumerable<int> ConvertStringToNumbers(string cutInputString, string[] arrDelimiters)
        {
            return cutInputString.Split(arrDelimiters, StringSplitOptions.None).Select(n => Convert.ToInt32(n));
        }
    }
}