/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using Newtonsoft.Json;

namespace ToolGood.WebCommon
{
    public class Req<T> : EncryptedQueryArgs<T>, IRequest
    {
        [JsonIgnore]
        public int OperatorId { get; set; }

        [JsonIgnore]
        public string OperatorName { get; set; }

        [JsonIgnore]
        public string Message { get; set; }

        /// <summary>
        /// 操作人密码
        /// </summary>
        [JsonIgnore]
        public string OperatorPassword {
            get {
                if (JData != null) {
                    if (JData.ContainsKey("OperatorPassword")) {
                        return JData["OperatorPassword"].ToString();
                    }
                }
                return null;
            }
            set {
            }
        }

        [JsonIgnore]
        public int AdminModeTime {
            get {
                if (JData != null) {
                    if (JData.ContainsKey("AdminModeTime")) {
                        return JData["AdminModeTime"].ToString().ToSafeInt32(0);
                    }
                }
                return 0;
            }
            set { }
        }

    }
}
