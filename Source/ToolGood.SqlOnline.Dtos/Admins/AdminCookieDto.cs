/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using Newtonsoft.Json;

namespace ToolGood.SqlOnline.Dtos
{
    public class AdminCookieDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int UserId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("n", NullValueHandling = NullValueHandling.Ignore)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [JsonProperty("pwd", NullValueHandling = NullValueHandling.Ignore)]
        public string PasswordHash { get; set; }

        /// <summary>
        /// 生成时间
        /// </summary>
        [JsonProperty("ct", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [JsonProperty("et", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ExpireTime { get; set; }

    }

}
