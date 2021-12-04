using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            if (numbers == string.Empty)
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

            if (messageException != string.Empty)
                throw new Exception(string.Format("Negatives not allowed: {0}",  messageException));

            return querySum.Where(n => n < 1000).Sum();
        }

        private string FindDelimiters(string numbers)
        {
            string delimiters = string.Empty;

            if (numbers.StartsWith("//["))
            {
                int startIndexDelimiters = 2;
                int lengthDelimiters = numbers.IndexOf('\n');

                numbers = numbers.Substring(startIndexDelimiters, lengthDelimiters);

                int count = 0;
                while (true)
                {
                    if (numbers[count++] == '[')
                    {
                        while (!(numbers[count] == ']' && (numbers[count + 1] == '\n' || numbers[count + 1] == '[')))
                        {
                            delimiters += numbers[count++];
                        }
                    }
                    
                    if (numbers[++count] == '\n')
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

            return delimiters == string.Empty ? null : delimiters;
        }

        private string TruncateStringToNumbers(string numbers)
        {
            int startIndexNumber = numbers.IndexOf('\n') + 1;
            return numbers.Substring(startIndexNumber);
        }

        private IEnumerable<int> ConvertStringToNumbers(string numbers, string delimiters)
        {
            return numbers.Split(delimiters.Split(' ').Reverse().ToArray(), StringSplitOptions.None).Select(n => Convert.ToInt32(n));
        }
    }
}