using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            if (numbers == String.Empty)
            {
                return 0;
            }

            string delimiters = FindDelimiters(numbers) ?? ", \n";

            if (FindDelimiters(numbers) != null)
            {
                numbers = TruncateStringToNumbers(numbers);
            }

            IEnumerable<int> querySum = ConvertStringToNumbers(numbers, delimiters);

            string messageException = string.Join(" ", querySum.Where(n => n < 0).Select(n => Convert.ToString(n)));

            if (messageException != String.Empty)
                throw new Exception(string.Format("Negatives not allowed: {0}",  messageException));

            return querySum.Where(n => n < 1000).Sum();
        }

        private string FindDelimiters(string numbers)
        {
            string delimiters = String.Empty;

            if (numbers.StartsWith("//["))
            {
                int count = numbers.IndexOf('[');
                while (true)
                {
                    if (numbers[count++] == '[')
                    {
                        while (numbers[count] != ']')
                        {
                            delimiters += numbers[count++];
                        }
                    }

                    count++;
                    if (numbers[count] == '\n')
                    {
                        break;
                    }

                    delimiters += ' ';
                }
            }
            else if (numbers.StartsWith("//"))
            {
                const int startIndexDelimiters = 2;
                return numbers.Substring(startIndexDelimiters, 1);
            }

            return delimiters == String.Empty ? null : delimiters;
        }

        private string TruncateStringToNumbers(string numbers)
        {
            int startIndexNumber = numbers.IndexOf('\n');
            return numbers.Substring(startIndexNumber);
        }

        private IEnumerable<int> ConvertStringToNumbers(string numbers, string delimiters)
        {
            return numbers.Split(delimiters.Split(' '), StringSplitOptions.None).Select(n => Convert.ToInt32(n));
        }
    }
}