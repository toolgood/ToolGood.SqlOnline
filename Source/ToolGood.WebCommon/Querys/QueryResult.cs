/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Text;
using Newtonsoft.Json;
using ToolGood.Common;
using ToolGood.RcxCrypto;

namespace ToolGood.WebCommon
{
    /// <summary>
    /// 查询结果
    /// </summary>
    public class QueryResult
    {
        /// <summary>
        /// code码
        /// </summary>
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public int Code { get; set; }
        /// <summary>
        /// code描术
        /// </summary>
        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }

        /// <summary>
        /// 密文
        /// </summary>
        [JsonProperty("ciphertext", NullValueHandling = NullValueHandling.Ignore)]
        public string Ciphertext { get; set; }


        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="password"></param>
        public void EncryptData(byte[] password)
        {
            if (password != null && password.Length > 0) {
                var json = Data.ToJson();
                var bytes = Encoding.UTF8.GetBytes(json);

                //var bs = ThreeRCX.Encrypt(bytes, password);//解密
                var bs = RCX.Encrypt(bytes, password);//解密
                Ciphertext = Base64.ToBase64String(bs);
                Data = null;
            }
        }

    }
}
