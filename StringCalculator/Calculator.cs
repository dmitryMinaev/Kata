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

            (int, string) tuple = FindDelimiters(numbers);

            numbers = numbers.Substring(tuple.Item1);
            IEnumerable<int> querySum = ConvertString(numbers, tuple.Item2);

            string message = String.Empty;

            if (querySum.Where(n => n < 0).Select(n => message += n).Count() > 0)
                throw new Exception(string.Format("negatives not allowed:{0}", message));

            return querySum.Sum();
        }

        private (int startIndex, string delimiters) FindDelimiters(string numbers)
        {
            int startIndex = 0;
            string delimiters = String.Empty;

            if (numbers.StartsWith("//["))
            {
                int i = 0;
                while (true)
                {
                    if (numbers[i++] == '[')
                    {
                        while (numbers[i] != ']')
                        {
                            delimiters += numbers[i++];
                        }
                    }

                    i++;
                    if (numbers[i] == '\n')
                    {
                        break;
                    }

                    delimiters += ' ';
                }

                startIndex = i;
            }
            else if (numbers.StartsWith("//"))
            {
                return (4, numbers[2].ToString());
            }

            return (startIndex, delimiters);
        }

        private IEnumerable<int> ConvertString(string numbers, string delimiters)
        {
            if (delimiters == String.Empty)
            {
                return numbers.Split(',', '\n').Select(n => Convert.ToInt32(n) < 1000 ? Convert.ToInt32(n) : 0);
            }

            var arrDelimiters = delimiters.Split(' ');
            if (arrDelimiters[0].Length == 1)
            {
                return numbers.Split(arrDelimiters.Select(n => Convert.ToChar(n)).ToArray()).Select(n => Convert.ToInt32(n) < 1000 ? Convert.ToInt32(n) : 0);
            }
            else
            {
                return numbers.Split(arrDelimiters, StringSplitOptions.None).Select(n => Convert.ToInt32(n) < 1000 ? Convert.ToInt32(n) : 0);
            }
        }
    }
}