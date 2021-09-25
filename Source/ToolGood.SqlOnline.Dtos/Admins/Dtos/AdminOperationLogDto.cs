/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;

namespace ToolGood.SqlOnline.Dtos
{
    public class AdminOperationLogDto
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; }

        public string IpAddress { get; set; }


        public string SessionID { get; set; }

        public string UserAgent { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime AddingTime { get; set; }
    }
}
