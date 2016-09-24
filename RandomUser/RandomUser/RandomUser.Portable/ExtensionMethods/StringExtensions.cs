using System;

namespace RandomUser.Portable.ExtensionMethods
{
    public static class StringExtensions
    {
        public static bool IsNeitherNullNorEmpty(this string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        public static bool IsNullOrEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }

        public static string FirstCharToUpper(this string input)
        {
            if (input.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(input));

            return input[0].ToString().ToUpper() + input.Substring(1);
        }
    }
}
