using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ToolGood.Infrastructure
{
    /// <summary>
    /// A Base36 De- and Encoder
    /// </summary>
    /// <remarks>
    /// http://www.stum.de/2008/10/20/base36-encoderdecoder-in-c/
    /// </remarks>
    public static class Base36
    {
        private const string CharList = "0123456789abcdefghijklmnopqrstuvwxyz";
        //private static char[] CharArray = CharList.ToCharArray();


        //public static string ToBase36String(this byte[] toConvert, bool bigEndian = false)
        //{
        //    const string alphabet = "0123456789abcdefghijklmnopqrstuvwxyz";
        //    if (bigEndian) Array.Reverse(toConvert); // !BitConverter.IsLittleEndian might be an alternative
        //    BigInteger dividend = new BigInteger(toConvert);
        //    var builder = new StringBuilder();
        //    while (dividend != 0) {
        //        BigInteger remainder;
        //        dividend = BigInteger.DivRem(dividend, 36, out remainder);
        //        builder.Insert(0, alphabet[Math.Abs(((int)remainder))]);
        //    }
        //    return builder.ToString();
        //}

        const int kByteBitCount = 8; // number of bits in a byte
        const string kBase36Digits = "0123456789abcdefghijklmnopqrstuvwxyz";
        // constants that we use in ToBase36CharArray
        static readonly double kBase36CharsLengthDivisor = Math.Log(kBase36Digits.Length, 2);
        static readonly BigInteger kBigInt36 = new BigInteger(36);


        public static string ToBase36String(this byte[] bytes, bool bigEndian = false)
        {
            // Estimate the result's length so we don't waste time realloc'ing
            //int result_length = (int)Math.Ceiling(bytes.Length * kByteBitCount / kBase36CharsLengthDivisor);
            // We use a List so we don't have to CopyTo a StringBuilder's characters
            // to a char[], only to then Array.Reverse it later
            var result = new StringBuilder();
            //var result = new System.Collections.Generic.List<char>(result_length);

            var dividend = new BigInteger(bytes);
            // IsZero's computation is less complex than evaluating "dividend > 0"
            // which invokes BigInteger.CompareTo(BigInteger)
            while (!dividend.IsZero) {
                BigInteger remainder;
                dividend = BigInteger.DivRem(dividend, kBigInt36, out remainder);
                int digit_index = Math.Abs((int)remainder);
                //result.Add(kBase36Digits[digit_index]);
                if (bigEndian) {
                    result.Insert(0, kBase36Digits[digit_index]);
                } else {
                    result.Append(kBase36Digits[digit_index]);
                }
            }

            // orientate the characters in big-endian ordering
            //if (!bigEndian)
            //result.Reverse();
            // ToArray will also trim the excess chars used in length prediction
            //return new string(result.ToArray());
            return result.ToString();
        }



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
                result.Push(clistarr[input % 36]);
                input /= 36;
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
                result += CharList.IndexOf(c) * (long)Math.Pow(36, pos);
                pos++;
            }
            return result;
        }
    }
}
