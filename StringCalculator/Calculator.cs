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
            string formatInputString = TruncateInputString(inputString);

            List<string> delimiters = FindDelimiters(inputString);

            IEnumerable<int> numbers = ConvertStringToNumbers(formatInputString, delimiters);

            CheckQueryNegativeValues(numbers);

            return numbers.Where(n => n < 1000).Sum();
        }

        private List<string> FindDelimiters(string inputString)
        {
            if (inputString.StartsWith("//["))
            {
                const int startIndexDelimiters = 2;
                const int sizeEndStr = 2;
                int lengthDelimiters = inputString.IndexOf(@"\n") - sizeEndStr;

                string stringDelimiters = inputString.Substring(startIndexDelimiters, lengthDelimiters);

                return CutStringToDelimiters(stringDelimiters);
            }
            else if (inputString.StartsWith("//"))
            {
                const int startIndexDelimiters = 2;
                return new List<string>() { inputString.Substring(startIndexDelimiters, 1) };
            }

            return new List<string>() { ",", @"\n" };
        }

        private List<string> CutStringToDelimiters(string strDelimiters)
        {
            List<string> listDelimiters = new List<string>();

            int count = 0;
            while (!(count == strDelimiters.Length))
            {
                string delimiters = string.Empty;

                if (strDelimiters[count++] == '[')
                {
                    while (!(strDelimiters[count] == ']' && ((count + 1) == strDelimiters.Length || strDelimiters[count + 1] == '[')))
                    {
                        delimiters += strDelimiters[count++];
                    }
                }

                listDelimiters.Add(delimiters);
                count++;
            }

            return listDelimiters;
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
                const int sizeEndDelimiters = 2;
                int startIndexNumber = inputString.IndexOf(@"\n") + sizeEndDelimiters;
                return inputString.Substring(startIndexNumber);
            }

            return inputString;
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