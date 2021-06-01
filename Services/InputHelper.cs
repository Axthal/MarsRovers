using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarsRovers.Services
{
    class InputHelper
    {
        /// <summary>
        /// Query the user for some valid integer and returns it.
        /// </summary>
        /// <param name="queryText">Question to the user for integer input</param>
        /// <returns></returns>
        public static int GetIntFromInput(string queryText)
        {
            int input = 0;
            Console.WriteLine(queryText);
            while (!int.TryParse(Console.ReadLine(), out input) && input > 0)
                Console.WriteLine("Please, insert a valid input: ");
            return input;
        }
        /// <summary>
        /// Query the user for some valid text accordingly to a Regex pattern. Case Insensitive for default.
        /// </summary>
        /// <param name="queryText">Question to the user for text input</param>
        /// <param name="regexPattern">Regex pattern to validate</param>
        /// <returns></returns>
        public static string GetValidInputByRegex(string queryText, string regexPattern)
        {
            string Text = "";
            Console.WriteLine(queryText);
            while (!Regex.IsMatch(Text = Console.ReadLine(), regexPattern, RegexOptions.IgnoreCase))
                Console.WriteLine("Please, insert a valid input: ");
            return Text.Trim().ToUpper();
        }
    }
}