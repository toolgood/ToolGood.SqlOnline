using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolGood.Infrastructure
{
    public static class Base26
    {
        private const string CharList = "abcdefghijklmnopqrstuvwxyz";
        private static char[] CharArray = CharList.ToCharArray();
        private const int count = 26;
        /// <summary>
        /// Encode the given number into a Base36 string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String Encode(long input)
        {
            if (input < 0) throw new ArgumentOutOfRangeException("input", input, "input cannot be negative");

            char[] clistarr = CharList.ToCharArray();
            var result = new Stack<char>();
            while (input != 0) {
                result.Push(clistarr[input % count]);
                input /= count;
            }
            return new string(result.ToArray());
        }

        /// <summary>
        /// Decode the Base36 Encoded string into a number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Int64 Decode(string input)
        {
            var reversed = input.ToLower().Reverse();
            long result = 0;
            int pos = 0;
            foreach (char c in reversed) {
                result += CharList.IndexOf(c) * (long)Math.Pow(count, pos);
                pos++;
            }
            return result;
        }
    }
}
