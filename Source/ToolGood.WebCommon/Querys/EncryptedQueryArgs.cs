/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ToolGood.Common;
using ToolGood.RcxCrypto;
using ToolGood.WebCommon.Utils;

namespace ToolGood.WebCommon
{

    /// <summary>
    /// 加密过的查询参数
    /// </summary>
    public abstract class EncryptedQueryArgs : QueryArgs
    {
        /// <summary>
        /// 通信密码 
        /// </summary>
        [JsonIgnore]
        public string PasswordString { get; set; }

        /// <summary>
        /// 钥匙（RSA公匙加密过的）
        /// </summary>
        [JsonProperty("rsaKey")]
        public string RsaKey { get; set; }

        /// <summary>
        /// 密文
        /// </summary>
        [JsonProperty("ciphertext")]
        public string Ciphertext { get; set; }

        /// <summary>
        /// 时间截
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [JsonProperty("sign")]
        public string Sign { get; set; }


        /// <summary>
        /// 核对签名
        /// </summary>
        /// <returns></returns>
        public bool CheckSign(string privateKey, string modulus, string exponent, out string errMsg)
        {
            if (Timestamp <= 0) { errMsg = "timestamp is null."; return false; }
            var st = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(Timestamp);
            if (Math.Abs(st.TotalSeconds) >= 5) { errMsg = "timestamp is error."; return false; }

            if (string.IsNullOrWhiteSpace(RsaKey)) { errMsg = "rsaKey is null."; return false; }
            if (string.IsNullOrWhiteSpace(Ciphertext)) { errMsg = "ciphertext is null."; return false; }
            if (string.IsNullOrWhiteSpace(Sign)) { errMsg = "sign is null."; return false; }

            PasswordString = RsaUtil.PrivateDecrypt(privateKey, RsaKey);

            var txt = $"{Ciphertext.ToSafeString()}|{PasswordString}|{Timestamp.ToSafeString()}|{modulus}|{exponent}";
            var hash = HashUtil.GetMd5String(txt);
            if (Sign.Equals(hash, StringComparison.OrdinalIgnoreCase) == false) {
                errMsg = "sign is error.";
                return false;
            }
            errMsg = null;
            return true;
        }

        /// <summary>
        /// 核对签名
        /// </summary>
        /// <returns></returns>
        public bool CheckSign(string modulus, string exponent, out string errMsg)
        {
            // 如果 timestamp 为0  标签没有加 [FromBody, FromForm]
            if (Timestamp <= 0) { errMsg = "timestamp is null."; return false; }
            var st = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(Timestamp);
            if (Math.Abs(st.TotalSeconds) >= 15) { errMsg = "timestamp is error."; return false; }

            if (string.IsNullOrWhiteSpace(Ciphertext)) { errMsg = "ciphertext is null."; return false; }
            if (string.IsNullOrWhiteSpace(Sign)) { errMsg = "sign is null."; return false; }

            var txt = $"{Ciphertext.ToSafeString()}|{PasswordString}|{Timestamp.ToSafeString()}|{modulus}|{exponent}";
            var hash = HashUtil.GetMd5String(txt);
            if (Sign.Equals(hash, StringComparison.OrdinalIgnoreCase) == false) {
                errMsg = "sign is error.";
                return false;
            }
            errMsg = null;
            return true;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <returns></returns>
        public abstract bool DecryptData();

        /// <summary>
        /// 核对数据
        /// </summary>
        /// <returns></returns>
        public abstract bool CheckData(out string errMsg);
    }
    /// <summary>
    /// 加密过的查询参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EncryptedQueryArgs<T> : EncryptedQueryArgs
    {
        /// <summary>
        /// 解密后的参数
        /// </summary>
        [JsonIgnore]
        public T Data { get; set; }

        /// <summary>
        /// 解密后的参数 JSON 格式
        /// </summary>
        [JsonIgnore]
        public JObject JData { get; set; }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public override bool DecryptData()
        {
            string json = null;
            try {
                var bs = RCX.Encrypt(Base64.FromBase64ForUrlString(Ciphertext), PasswordString);//解密
                json = Encoding.UTF8.GetString(bs);

                JData = JObject.Parse(json, new JsonLoadSettings() {
                    CommentHandling = CommentHandling.Ignore,
                    LineInfoHandling = LineInfoHandling.Ignore,
                    DuplicatePropertyNameHandling = DuplicatePropertyNameHandling.Replace
                });
                Data = JData.ToObject<T>();
                return true;
            } catch (Exception ex) {


                LogUtil.Error(ex, json);
            }
            return false;
        }



        /// <summary>
        /// 核对数据
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public override bool CheckData(out string errMsg)
        {
            try {
                return CheckData(typeof(T), Data, null, out errMsg);
            } catch (Exception ex) {
                errMsg = ex.ToErrMsg();
                return false;
            }
        }

        private bool CheckData(Type type, object value, string baseName, out string errMsg)
        {
            if (type.IsClass == false) { errMsg = null; return true; }
            if (SimpleTypes.Contains(type)) { errMsg = null; return true; }

            var pis = type.GetProperties();
            foreach (var pi in pis) {
                if (pi.CanRead == false) { continue; }
                object obj = pi.GetGetMethod().Invoke(value, null);
                if (obj is DateTime && (DateTime)obj == DateTime.MinValue) {
                    errMsg = $"{GetPropertyName(pi)}为时间类型！";
                    return false;
                }

                var atts = pi.GetCustomAttributes<System.ComponentModel.DataAnnotations.ValidationAttribute>(true).ToList();
                if (atts.Count > 0) {
                    foreach (var att in atts) {
                        if (att.IsValid(obj) == false) {
                            errMsg = att.FormatErrorMessage(GetPropertyName(pi));
                            return false;
                        }
                    }
                }

                if (pi.PropertyType.IsClass && obj != null && SimpleTypes.Contains(pi.PropertyType) == false) {
                    if (obj is IList list) {
                        for (int i = 0; i < list.Count; i++) {
                            var item = list[i];
                            if (object.Equals(null, item) == false) {
                                var itemType = item.GetType();
                                if (itemType.IsClass == false) { continue; }
                                if (SimpleTypes.Contains(itemType)) { continue; }

                                if (CheckData(itemType, item, GetPropertyName(pi), out errMsg) == false) {
                                    return false;
                                }
                            }
                        }
                    } else {
                        if (CheckData(pi.PropertyType, obj, GetPropertyName(pi), out errMsg) == false) {
                            return false;
                        }
                    }
                }
            }
            errMsg = null;
            return true;
        }
        private static string GetPropertyName(PropertyInfo pi)
        {
            var att = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
            if (att.Length > 0) {
                return (att[0] as DisplayNameAttribute).DisplayName;
            }
            //att = pi.GetCustomAttributes(typeof(DescriptionAttribute), true);
            //if (att.Length > 0) {
            //    return (att[0] as DescriptionAttribute).Description;
            //}
            return pi.Name;
        }


        private static readonly HashSet<Type> SimpleTypes = new HashSet<Type>() {
            typeof(string)
            ,typeof(byte),typeof(sbyte),typeof(char),typeof(Boolean),typeof(Guid)
            ,typeof(UInt16),typeof(UInt32),typeof(UInt64),typeof(Int16),typeof(Int32),typeof(Int64)
            ,typeof(Single),typeof(Double),typeof(Decimal)
            ,typeof(DateTime),typeof(DateTimeOffset),typeof(TimeSpan)
            ,typeof(IntPtr),typeof(UIntPtr)
            ,typeof(byte?),typeof(sbyte?),typeof(char?), typeof(Boolean?),typeof(Guid?)
            ,typeof(UInt16?),typeof(UInt32?),typeof(UInt64?),typeof(Int16?),typeof(Int32?),typeof(Int64?)
            ,typeof(Single?),typeof(Double?),typeof(Decimal?)
            ,typeof(DateTime?),typeof(DateTimeOffset?),typeof(TimeSpan?)
            ,typeof(IntPtr?),typeof(UIntPtr?)
        };

        //private string GetPropertyName(string baseName, string propertyName, int index = -1)
        //{
        //    if (string.IsNullOrEmpty(baseName)) {
        //        if (index == -1) {
        //            return propertyName;
        //        }
        //        return $"{propertyName}[{index}]";
        //    }
        //    if (index == -1) {
        //        return $"{baseName}.{propertyName}";
        //    }
        //    return $"{baseName}.{propertyName}[{index}]";
        //}


    }




}
