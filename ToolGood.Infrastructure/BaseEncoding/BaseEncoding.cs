using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Contract = System.Diagnostics.Contracts.Contract;

namespace ToolGood.Infrastructure
{
    /// <summary>
    /// https://stackoverflow.com/questions/14110010/base-n-encoding-of-a-byte-array/
    /// 
    /// </summary>
    public class BaseEncoding
    {
        public enum EndianFormat
        {
            /// <summary>Least Significant Bit order (lsb)</summary>
            /// <remarks>Right-to-Left</remarks>
            /// <see cref="BitConverter.IsLittleEndian"/>
            Little,
            /// <summary>Most Significant Bit order (msb)</summary>
            /// <remarks>Left-to-Right</remarks>
            Big,
        };

        const int kByteBitCount = 8;

        readonly string kDigits;
        readonly double kBitsPerDigit;
        readonly BigInteger kRadixBig;
        readonly EndianFormat kEndian;
        readonly bool kIncludeProceedingZeros;

        /// <summary>Numerial base of this encoding</summary>
        public int Radix { get { return kDigits.Length; } }
        /// <summary>Endian ordering of bytes input to Encode and output by Decode</summary>
        public EndianFormat Endian { get { return kEndian; } }
        /// <summary>True if we want ending zero bytes to be encoded</summary>
        public bool IncludeProceedingZeros { get { return kIncludeProceedingZeros; } }

        public override string ToString()
        {
            return string.Format("Base-{0} {1}", Radix.ToString(), kDigits);
        }

        /// <summary>Create a radix encoder using the given characters as the digits in the radix</summary>
        /// <param name="digits">Digits to use for the radix-encoded string</param>
        /// <param name="bytesEndian">Endian ordering of bytes input to Encode and output by Decode</param>
        /// <param name="includeProceedingZeros">True if we want ending zero bytes to be encoded</param>
        public BaseEncoding(string digits, EndianFormat bytesEndian = EndianFormat.Little, bool includeProceedingZeros = false)
        {
            Contract.Requires<ArgumentNullException>(digits != null);
            int radix = digits.Length;

            kDigits = digits;
            kBitsPerDigit = System.Math.Log(radix, 2);
            kRadixBig = new BigInteger(radix);
            kEndian = bytesEndian;
            kIncludeProceedingZeros = includeProceedingZeros;
        }

        // Number of characters needed for encoding the specified number of bytes
        int EncodingCharsCount(int bytesLength)
        {
            return (int)Math.Ceiling((bytesLength * kByteBitCount) / kBitsPerDigit);
        }
        // Number of bytes needed to decoding the specified number of characters
        int DecodingBytesCount(int charsCount)
        {
            return (int)Math.Ceiling((charsCount * kBitsPerDigit) / kByteBitCount);
        }

        /// <summary>Encode a byte array into a radix-encoded string</summary>
        /// <param name="bytes">byte array to encode</param>
        /// <returns>The bytes in encoded into a radix-encoded string</returns>
        /// <remarks>If <paramref name="bytes"/> is zero length, returns an empty string</remarks>
        public string Encode(byte[] bytes)
        {
            Contract.Requires<ArgumentNullException>(bytes != null);
            Contract.Ensures(Contract.Result<string>() != null);

            // Don't really have to do this, our code will build this result (empty string),
            // but why not catch the condition before doing work?
            if (bytes.Length == 0) return string.Empty;

            // if the array ends with zeros, having the capacity set to this will help us know how much
            // 'padding' we will need to add
            int result_length = EncodingCharsCount(bytes.Length);
            // List<> has a(n in-place) Reverse method. StringBuilder doesn't. That's why.
            var result = new List<char>(result_length);

            // HACK: BigInteger uses the last byte as the 'sign' byte. If that byte's MSB is set, 
            // we need to pad the input with an extra 0 (ie, make it positive)
            if ((bytes[bytes.Length - 1] & 0x80) == 0x80)
                Array.Resize(ref bytes, bytes.Length + 1);

            var dividend = new BigInteger(bytes);
            // IsZero's computation is less complex than evaluating "dividend > 0"
            // which invokes BigInteger.CompareTo(BigInteger)
            while (!dividend.IsZero) {
                BigInteger remainder;
                dividend = BigInteger.DivRem(dividend, kRadixBig, out remainder);
                int digit_index = System.Math.Abs((int)remainder);
                result.Add(kDigits[digit_index]);
            }

            if (kIncludeProceedingZeros)
                for (int x = result.Count; x < result.Capacity; x++)
                    result.Add(kDigits[0]); // pad with the character that represents 'zero'

            // orientate the characters in big-endian ordering
            if (kEndian == EndianFormat.Little)
                result.Reverse();
            // If we didn't end up adding padding, ToArray will end up returning a TrimExcess'd array, 
            // so nothing wasted
            return new string(result.ToArray());
        }

        void DecodeImplPadResult(ref byte[] result, int padCount)
        {
            if (padCount > 0) {
                int new_length = result.Length + DecodingBytesCount(padCount);
                Array.Resize(ref result, new_length); // new bytes will be zero, just the way we want it
            }
        }
        #region Decode (Little Endian)
        byte[] DecodeImpl(string chars, int startIndex = 0)
        {
            var bi = new BigInteger();
            for (int x = startIndex; x < chars.Length; x++) {
                int i = kDigits.IndexOf(chars[x]);
                if (i < 0) return null; // invalid character
                bi *= kRadixBig;
                bi += i;
            }

            return bi.ToByteArray();
        }
        byte[] DecodeImplWithPadding(string chars)
        {
            int pad_count = 0;
            for (int x = 0; x < chars.Length; x++, pad_count++)
                if (chars[x] != kDigits[0]) break;

            var result = DecodeImpl(chars, pad_count);
            DecodeImplPadResult(ref result, pad_count);

            return result;
        }
        #endregion
        #region Decode (Big Endian)
        byte[] DecodeImplReversed(string chars, int startIndex = 0)
        {
            var bi = new BigInteger();
            for (int x = (chars.Length - 1) - startIndex; x >= 0; x--) {
                int i = kDigits.IndexOf(chars[x]);
                if (i < 0) return null; // invalid character
                bi *= kRadixBig;
                bi += i;
            }

            return bi.ToByteArray();
        }
        byte[] DecodeImplReversedWithPadding(string chars)
        {
            int pad_count = 0;
            for (int x = chars.Length - 1; x >= 0; x--, pad_count++)
                if (chars[x] != kDigits[0]) break;

            var result = DecodeImplReversed(chars, pad_count);
            DecodeImplPadResult(ref result, pad_count);

            return result;
        }
        #endregion
        /// <summary>Decode a radix-encoded string into a byte array</summary>
        /// <param name="radixChars">radix string</param>
        /// <returns>The decoded bytes, or null if an invalid character is encountered</returns>
        /// <remarks>
        /// If <paramref name="radixChars"/> is an empty string, returns a zero length array
        /// 
        /// Using <paramref name="IncludeProceedingZeros"/> has the potential to return a buffer with an
        /// additional zero byte that wasn't in the input. So a 4 byte buffer was encoded, this could end up
        /// returning a 5 byte buffer, with the extra byte being null.
        /// </remarks>
        public byte[] Decode(string radixChars)
        {
            Contract.Requires<ArgumentNullException>(radixChars != null);

            if (kEndian == EndianFormat.Big)
                return kIncludeProceedingZeros ? DecodeImplReversedWithPadding(radixChars) : DecodeImplReversed(radixChars);
            else
                return kIncludeProceedingZeros ? DecodeImplWithPadding(radixChars) : DecodeImpl(radixChars);
        }




    }
}
