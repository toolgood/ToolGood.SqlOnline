//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace ToolGood.Infrastructure
//{
//    public class RC4
//    {
//        public byte[] keybox;
//        private const int keyLen = 256;


//        public RC4(string pass)
//        {
//            var ps = Encoding.Unicode.GetBytes(pass);
//            keybox = GetKey(ps, keyLen);
//        }
//        /// <summary>
//        /// Encrypt
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public byte[] Encrypt(byte[] data)
//        {
//            if (data == null) throw new ArgumentNullException("data");

//            byte[] mBox = new byte[keyLen];
//            Array.Copy(keybox, mBox, keyLen);
//            byte[] output = new byte[data.Length];
//            int i = 0, j = 0;
//             for (Int64 offset = 0; offset < data.Length; offset++) {
//                i = (++i) & 0xFF;
//                j = (j + mBox[i]) & 0xFF;
//                byte temp = mBox[i];
//                mBox[i] = mBox[j];
//                mBox[j] = temp;

//                byte a = data[offset];
//                byte b = mBox[(mBox[i] + mBox[j]) & 0xFF];
//                output[offset] = (byte)((int)a ^ (int)b);
//            }
//            return output;
//        }

//        /// <summary>
//        /// Encrypt
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="pass"></param>
//        /// <returns></returns>
//        public static byte[] Encrypt(byte[] data, byte[] pass)
//        {
//            if (data == null) throw new ArgumentNullException("data");
//            if (pass == null) throw new ArgumentNullException("pass");

//            byte[] mBox = GetKey(pass, keyLen);
//            byte[] output = new byte[data.Length];
//            int i = 0, j = 0;
//             for (Int64 offset = 0; offset < data.Length; offset++) {
//                i = (++i) & 0xFF;
//                j = (j + mBox[i]) & 0xFF;
//                byte temp = mBox[i];
//                mBox[i] = mBox[j];
//                mBox[j] = temp;

//                byte a = data[offset];
//                byte b = mBox[(mBox[i] + mBox[j]) & 0xFF];
//                output[offset] = (byte)((int)a ^ (int)b);
//            }
//            return output;
//        }
//        /// <summary>
//        /// Encrypt
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="pass"></param>
//        /// <returns></returns>
//        public static byte[] Encrypt(byte[] data, string pass)
//        {
//            if (data == null) throw new ArgumentNullException("data");
//            if (pass == null) throw new ArgumentNullException("pass");

//            return Encrypt(data, Encoding.Unicode.GetBytes(pass));
//        }

 
//        private static byte[] GetKey(byte[] pass, int kLen)
//        {
//            byte[] mBox = new byte[kLen];
//            for (Int64 i = 0; i < kLen; i++) {
//                mBox[i] = (byte)i;
//            }
//            Int64 j = 0;
//            for (Int64 i = 0; i < kLen; i++) {
//                j = (j + mBox[i] + pass[i % pass.Length]) % kLen;
//                byte temp = mBox[i];
//                mBox[i] = mBox[j];
//                mBox[j] = temp;
//            }
//            return mBox;
//        }
//    }
//}
