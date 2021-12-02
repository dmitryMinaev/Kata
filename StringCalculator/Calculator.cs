using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        private List<char> delimitersChar = new List<char>();
        private List<string> delimitersString = new List<string>();

        public int Add(string numbers)
        {
            if (numbers == String.Empty)
            {
                return 0;
            }

            int startIndex = FindDelimiters(numbers);
            IEnumerable<int> querySum = ConvertString(numbers, startIndex);

            string message = String.Empty;

            if (querySum.Where(n => n < 0).Select(n => message += n).Count() > 0)
                throw new Exception(string.Format("negatives not allowed:{0}", message));

            return querySum.Sum();
        }

        private int FindDelimiters(string numbers)
        {
            int startIndex = 0;

            if (numbers.StartsWith("//["))
            {
                int i = 0;
                while (true)
                {
                    if (numbers[i++] == '[')
                    {
                        string temp = String.Empty;
                        while (numbers[i] != ']')
                        {
                            temp += numbers[i];
                            i++;
                        }

                        if (temp.Length < 1)
                        {
                            delimitersChar.Add(Convert.ToChar(temp));
                        }
                        else
                        {
                            delimitersString.Add(temp);
                        }
                    }

                    i++;
                    if (numbers[i] == '\n')
                    {
                        break;
                    }
                }

                startIndex = i;
            }
            else if (numbers.StartsWith("//"))
            {
                delimitersChar.Add(numbers[2]);
                startIndex = 4;
            }

            return startIndex;
        }

        private IEnumerable<int> ConvertString(string numbers, int startIndex)
        {
            if (delimitersChar.Any())
            {
                return numbers.Substring(startIndex).Split(delimitersChar.ToArray()).Select(n => Convert.ToInt32(n) < 1000 ? Convert.ToInt32(n) : 0);
            }
            else if (delimitersString.Any())
            {
                return numbers.Substring(startIndex).Split(delimitersString.ToArray(), StringSplitOptions.None).Select(n => Convert.ToInt32(n) < 1000 ? Convert.ToInt32(n) : 0);
            }
            else
            {
                return numbers.Split(',', '\n').Select(n => Convert.ToInt32(n) < 1000 ? Convert.ToInt32(n) : 0);
            }
        }
    }
}
