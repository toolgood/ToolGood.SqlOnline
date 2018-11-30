using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.Infrastructure
{
    public class Base58
    {
        private static char[] ALPHABET = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz".ToCharArray();
        private static int BASE_58 = ALPHABET.Length;
        private static int BASE_256 = 256;

        private static int[] INDEXES = new int[128];
        static Base58()
        {
            for (int i = 0; i < INDEXES.Length; i++) {
                INDEXES[i] = -1;
            }
            for (int i = 0; i < ALPHABET.Length; i++) {
                INDEXES[ALPHABET[i]] = i;
            }
        }


        public static String Encode(byte[] input)
        {
            if (input.Length == 0) {
                // paying with the same coin
                return "";
            }

            //
            // Make a copy of the input since we are going to modify it.
            //
            input = copyOfRange(input, 0, input.Length);

            //
            // Count leading zeroes
            //
            int zeroCount = 0;
            while (zeroCount < input.Length && input[zeroCount] == 0) {
                ++zeroCount;
            }

            //
            // The actual encoding
            //
            byte[] temp = new byte[input.Length * 2];
            int j = temp.Length;

            int startAt = zeroCount;
            while (startAt < input.Length) {
                byte mod = divmod58(input, startAt);
                if (input[startAt] == 0) {
                    ++startAt;
                }

                temp[--j] = (byte)ALPHABET[mod];
            }

            //
            // Strip extra '1' if any
            //
            while (j < temp.Length && temp[j] == ALPHABET[0]) {
                ++j;
            }

            //
            // Add as many leading '1' as there were leading zeros.
            //
            while (--zeroCount >= 0) {
                temp[--j] = (byte)ALPHABET[0];
            }

            byte[] output = copyOfRange(temp, j, temp.Length);
            return Encoding.UTF8.GetString(output);
            //return new String(output);
        }


        public static byte[] Decode(String input)
        {
            if (input.Length == 0) {
                // paying with the same coin
                return new byte[0];
            }

            byte[] input58 = new byte[input.Length];
            //
            // Transform the String to a base58 byte sequence
            //
            for (int i = 0; i < input.Length; ++i) {
                char c = input[i];

                int digit58 = -1;
                if (c >= 0 && c < 128) {
                    digit58 = INDEXES[c];
                }
                if (digit58 < 0) {
                    throw new Exception("Not a Base58 input: " + input);
                }

                input58[i] = (byte)digit58;
            }

            //
            // Count leading zeroes
            //
            int zeroCount = 0;
            while (zeroCount < input58.Length && input58[zeroCount] == 0) {
                ++zeroCount;
            }

            //
            // The encoding
            //
            byte[] temp = new byte[input.Length];
            int j = temp.Length;

            int startAt = zeroCount;
            while (startAt < input58.Length) {
                byte mod = divmod256(input58, startAt);
                if (input58[startAt] == 0) {
                    ++startAt;
                }

                temp[--j] = mod;
            }

            //
            // Do no add extra leading zeroes, move j to first non null byte.
            //
            while (j < temp.Length && temp[j] == 0) {
                ++j;
            }

            return copyOfRange(temp, j - zeroCount, temp.Length);
        }


        private static byte divmod58(byte[] number, int startAt)
        {
            int remainder = 0;
            for (int i = startAt; i < number.Length; i++) {
                int digit256 = (int)number[i] & 0xFF;
                int temp = remainder * BASE_256 + digit256;
                number[i] = (byte)(temp / BASE_58);
                remainder = temp % BASE_58;
            }

            return (byte)remainder;
        }
        private static byte divmod256(byte[] number58, int startAt)
        {
            int remainder = 0;
            for (int i = startAt; i < number58.Length; i++) {
                int digit58 = (int)number58[i] & 0xFF;
                int temp = remainder * BASE_58 + digit58;
                number58[i] = (byte)(temp / BASE_256);
                remainder = temp % BASE_256;
            }

            return (byte)remainder;
        }

        private static byte[] copyOfRange(byte[] source, int from, int to)
        {
            byte[] range = new byte[to - from];
            Array.Copy(source, from, range, 0, range.Length);
            return range;
        }


        

    }
}
