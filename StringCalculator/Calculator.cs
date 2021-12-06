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

            List<string> customDelimiters = FindDelimiters(inputString);

            string formatInputString = customDelimiters == null ? inputString : TruncateStringToNumbers(inputString);

            List<string> delimiters = customDelimiters ?? new List<string>() { ",", @"\n" };

            IEnumerable<int> numbers = ConvertStringToNumbers(formatInputString , delimiters);

            if (numbers.Any(i => i < 0))
            {
                string messageException = string.Join(" ", numbers.Where(n => n < 0));

                throw new Exception(string.Format("Negatives not allowed: {0}", messageException));
            }

            return numbers.Where(n => n < 1000).Sum();
        }

        private List<string> FindDelimiters(string inputString)
        {
            List<string> listDelimiters = null;

            if (inputString.StartsWith("//["))
            {
                listDelimiters = new List<string>();

                int startIndexDelimiters = 2;
                int sizeEndStr = 2;
                int lengthDelimiters = inputString.IndexOf(@"\n") - sizeEndStr;

                string stringDelimiters = inputString.Substring(startIndexDelimiters, lengthDelimiters);

                int count = 0;
                while (!(count == stringDelimiters.Length))
                {
                    string delimiters = string.Empty;

                    if (stringDelimiters[count++] == '[')
                    {
                        while (!(stringDelimiters[count] == ']' && ((count + 1) == stringDelimiters.Length || stringDelimiters[count + 1] == '[')))
                        {
                            delimiters += stringDelimiters[count++];
                        }
                    }

                    listDelimiters.Add(delimiters);
                    count++;
                }
            }
            else if (inputString.StartsWith("//"))
            {
                const int startIndexDelimiters = 2;
                return new List<string>() { inputString.Substring(startIndexDelimiters, 1) };
            }

            return listDelimiters;
        }

        private string TruncateStringToNumbers(string cutInputString)
        {
            int sizeEndDelimiters = 2;
            int startIndexNumber = cutInputString.IndexOf(@"\n") + sizeEndDelimiters;
            return cutInputString.Substring(startIndexNumber);
        }

        private IEnumerable<int> ConvertStringToNumbers(string cutInputString, List<string> listDelimiters)
        {
            //sorting from larger to smaller for a proper split
            listDelimiters.Sort();
            listDelimiters.Reverse();

            var arrDelimiters = listDelimiters.ToArray();

            return cutInputString.Split(arrDelimiters, StringSplitOptions.None).Select(n => Convert.ToInt32(n));
        }
    }
}