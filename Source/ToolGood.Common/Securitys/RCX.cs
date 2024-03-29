﻿/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Text;

namespace ToolGood.RcxCrypto
{
    /// <summary>
    /// RCX algorithm
    /// Author : Lin ZhiJun
    /// NickName : ToolGood
    /// Email : toolgood@qq.com
    /// 
    /// See https://github.com/toolgood/RCX
    /// </summary>
    public class RCX
    {
        private readonly byte[] keybox;
        private const int keyLen = 256;
        private readonly Encoding encoding;

        public RCX(byte[] pass)
        {
            encoding = Encoding.UTF8;
            keybox = GetKey(pass, keyLen);
        }


        public RCX(byte[] pass, Encoding encoding)
        {
            this.encoding = encoding;
            keybox = GetKey(pass, keyLen);
        }

        public RCX(string pass)
        {
            if (string.IsNullOrEmpty(pass)) throw new ArgumentNullException("pass");
            var ps = Encoding.UTF8.GetBytes(pass);
            encoding = Encoding.UTF8;
            keybox = GetKey(ps, keyLen);
        }

        public RCX(string pass, Encoding encoding)
        {
            if (string.IsNullOrEmpty(pass)) throw new ArgumentNullException("pass");
            var ps = encoding.GetBytes(pass);
            this.encoding = encoding;
            keybox = GetKey(ps, keyLen);
        }

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Encrypt(string data, OrderType order = OrderType.Asc)
        {
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException("data");
            return encrypt(encoding.GetBytes(data), order);
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public byte[] Encrypt(string data, Encoding encoding, OrderType order = OrderType.Asc)
        {
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException("data");
            return encrypt(encoding.GetBytes(data), order);
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] data, OrderType order = OrderType.Asc)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length == 0) throw new ArgumentNullException("data");
            return encrypt(data, order);
        }
        private unsafe byte[] encrypt(byte[] data, OrderType order)
        {

            byte[] mBox = new byte[keyLen];
            Array.Copy(keybox, mBox, keyLen);
            //Buffer.BlockCopy(keybox, 0, mBox, 0, keyLen);
            byte[] output = new byte[data.Length];


            if (order == OrderType.Asc) {
                fixed (byte* _mBox = &mBox[0])
                fixed (byte* _data = &data[0])
                fixed (byte* _output = &output[0]) {
                    var length = data.Length;
                    int i = 0, j = 0;
                    for (Int64 offset = 0; offset < length; offset++) {
                        i = (++i) & 0xFF;
                        j = (j + *(_mBox + i)) & 0xFF;

                        byte a = *(_data + offset);
                        byte c = (byte)(a ^ *(_mBox + ((*(_mBox + i) + *(_mBox + j)) & 0xFF)));
                        *(_output + offset) = c;

                        byte temp = *(_mBox + a);
                        *(_mBox + a) = *(_mBox + c);
                        *(_mBox + c) = temp;
                        j = (j + a + c);
                    }
                }
            } else {
                fixed (byte* _mBox = &mBox[0])
                fixed (byte* _data = &data[0])
                fixed (byte* _output = &output[0]) {
                    var length = data.Length;
                    int i = 0, j = 0;
                    for (int offset = data.Length - 1; offset >= 0; offset--) {
                        i = (++i) & 0xFF;
                        j = (j + *(_mBox + i)) & 0xFF;

                        byte a = *(_data + offset);
                        byte c = (byte)(a ^ *(_mBox + ((*(_mBox + i) + *(_mBox + j)) & 0xFF)));
                        *(_output + offset) = c;

                        byte temp = *(_mBox + a);
                        *(_mBox + a) = *(_mBox + c);
                        *(_mBox + c) = temp;
                        j = (j + a + c);
                    }
                }
            }

            //int i = 0, j = 0;
            //if (order == OrderType.Asc) {
            //    for (int offset = 0; offset < data.Length; offset++) {
            //        i = (++i) & 0xFF;
            //        j = (j + mBox[i]) & 0xFF;

            //        byte a = data[offset];
            //        byte c = (byte)(a ^ mBox[(mBox[i] + mBox[j]) & 0xFF]);
            //        output[offset] = c;

            //        byte temp2 = mBox[c];
            //        mBox[c] = mBox[a];
            //        mBox[a] = temp2;
            //        j = (j + a + c);
            //    }
            //} else {
            //    for (int offset = data.Length - 1; offset >= 0; offset--) {
            //        i = (++i) & 0xFF;
            //        j = (j + mBox[i]) & 0xFF;

            //        byte a = data[offset];
            //        byte c = (byte)(a ^ mBox[(mBox[i] + mBox[j]) & 0xFF]);
            //        output[offset] = c;

            //        byte temp2 = mBox[c];
            //        mBox[c] = mBox[a];
            //        mBox[a] = temp2;
            //        j = (j + a + c);
            //    }
            //}

            return output;
        }


        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pass"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] Encrypt(string data, string pass, Encoding encoding, OrderType order = OrderType.Asc)
        {
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException("data");
            if (string.IsNullOrEmpty(pass)) throw new ArgumentNullException("pass");

            return encrypt(encoding.GetBytes(data), encoding.GetBytes(pass), order);
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static byte[] Encrypt(string data, string pass, OrderType order = OrderType.Asc)
        {
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException("data");
            if (string.IsNullOrEmpty(pass)) throw new ArgumentNullException("pass");

            return encrypt(Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(pass), order);
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] data, string pass, Encoding encoding, OrderType order = OrderType.Asc)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length == 0) throw new ArgumentNullException("data");
            if (string.IsNullOrEmpty(pass)) throw new ArgumentNullException("pass");

            return encrypt(data, encoding.GetBytes(pass), order);
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] data, string pass, OrderType order = OrderType.Asc)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length == 0) throw new ArgumentNullException("data");
            if (string.IsNullOrEmpty(pass)) throw new ArgumentNullException("pass");

            return encrypt(data, Encoding.UTF8.GetBytes(pass), order);
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static byte[] Encrypt(string data, byte[] pass, OrderType order = OrderType.Asc)
        {
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException("data");
            if (pass == null) throw new ArgumentNullException("pass");
            if (pass.Length == 0) throw new ArgumentNullException("pass");

            return encrypt(Encoding.UTF8.GetBytes(data), pass, order);
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pass"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] Encrypt(string data, byte[] pass, Encoding encoding, OrderType order = OrderType.Asc)
        {
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException("data");
            if (pass == null) throw new ArgumentNullException("pass");
            if (pass.Length == 0) throw new ArgumentNullException("pass");

            return encrypt(encoding.GetBytes(data), pass, order);
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] data, byte[] pass, OrderType order = OrderType.Asc)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length == 0) throw new ArgumentNullException("data");
            if (pass == null) throw new ArgumentNullException("pass");
            if (pass.Length == 0) throw new ArgumentNullException("pass");

            return encrypt(data, pass, order);
        }
        private unsafe static byte[] encrypt(byte[] data, byte[] pass, OrderType order)
        {
            byte[] mBox = GetKey(pass, keyLen);
            byte[] output = new byte[data.Length];
            //int i = 0, j = 0;

            if (order == OrderType.Asc) {
                fixed (byte* _mBox = &mBox[0])
                fixed (byte* _data = &data[0])
                fixed (byte* _output = &output[0]) {
                    var length = data.Length;
                    int i = 0, j = 0;
                    for (Int64 offset = 0; offset < length; offset++) {
                        i = (++i) & 0xFF;
                        j = (j + *(_mBox + i)) & 0xFF;

                        byte a = *(_data + offset);
                        byte c = (byte)(a ^ *(_mBox + ((*(_mBox + i) + *(_mBox + j)) & 0xFF)));
                        *(_output + offset) = c;

                        byte temp = *(_mBox + a);
                        *(_mBox + a) = *(_mBox + c);
                        *(_mBox + c) = temp;
                        j = (j + a + c);
                    }
                }
            } else {
                fixed (byte* _mBox = &mBox[0])
                fixed (byte* _data = &data[0])
                fixed (byte* _output = &output[0]) {
                    var length = data.Length;
                    int i = 0, j = 0;
                    for (int offset = data.Length - 1; offset >= 0; offset--) {
                        i = (++i) & 0xFF;
                        j = (j + *(_mBox + i)) & 0xFF;

                        byte a = *(_data + offset);
                        byte c = (byte)(a ^ *(_mBox + ((*(_mBox + i) + *(_mBox + j)) & 0xFF)));
                        *(_output + offset) = c;

                        byte temp = *(_mBox + a);
                        *(_mBox + a) = *(_mBox + c);
                        *(_mBox + c) = temp;
                        j = (j + a + c);
                    }
                }
            }

            //if (order == OrderType.Asc) {
            //    for (int offset = 0; offset < data.Length; offset++) {
            //        i = (++i) & 0xFF;
            //        j = (j + mBox[i]) & 0xFF;

            //        byte a = data[offset];
            //        byte c = (byte)(a ^ mBox[(mBox[i] + mBox[j]) & 0xFF]);
            //        output[offset] = c;

            //        byte temp2 = mBox[c];
            //        mBox[c] = mBox[a];
            //        mBox[a] = temp2;
            //        j = (j + a + c);
            //    }
            //} else {
            //    for (int offset = data.Length - 1; offset >= 0; offset--) {
            //        i = (++i) & 0xFF;
            //        j = (j + mBox[i]) & 0xFF;

            //        byte a = data[offset];
            //        byte c = (byte)(a ^ mBox[(mBox[i] + mBox[j]) & 0xFF]);
            //        output[offset] = c;

            //        byte temp2 = mBox[c];
            //        mBox[c] = mBox[a];
            //        mBox[a] = temp2;
            //        j = (j + a + c);
            //    }
            //}
            return output;
        }



        private static unsafe byte[] GetKey(byte[] pass, int kLen)
        {
            byte[] mBox = new byte[kLen];
            fixed (byte* _mBox = &mBox[0]) {
                for (Int64 i = 0; i < kLen; i++) {
                    *(_mBox + i) = (byte)i;
                }
                Int64 j = 0;
                int lengh = pass.Length;
                fixed (byte* _pass = &pass[0]) {
                    for (Int64 i = 0; i < kLen; i++) {
                        j = (j + *(_mBox + i) + *(_pass + (i % lengh))) % kLen;
                        byte temp = *(_mBox + i);
                        *(_mBox + i) = *(_mBox + j);
                        *(_mBox + j) = temp;
                    }
                }
            }

            //for (Int64 i = 0; i < kLen; i++) {
            //    mBox[i] = (byte)i;
            //}
            //Int64 j = 0;
            //for (Int64 i = 0; i < kLen; i++) {
            //    j = (j + mBox[i] + pass[i % pass.Length]) % kLen;
            //    byte temp = mBox[i];
            //    mBox[i] = mBox[j];
            //    mBox[j] = temp;
            //}
            return mBox;
        }
    }
}
