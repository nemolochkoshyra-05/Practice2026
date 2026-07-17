using System;
using System.Linq;

namespace task01
{
    public static class StringExtensions
    {
        public static bool IsPalindrome(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            var cleanedChars = input
                .Where(c => !char.IsPunctuation(c) && !char.IsWhiteSpace(c))
                .Select(char.ToLower)
                .ToArray();

            string cleanedString = new string(cleanedChars);

            if (cleanedString.Length == 0)
            {
                return false;
            }

            string reversedString = new string(cleanedChars.Reverse().ToArray());

            return cleanedString == reversedString;
        }
    }
}