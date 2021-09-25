/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.SqlOnline.Datas
{
    [Table("SysAdminOperationLog")]
    [Index("AdminId")]
    [Index("Name")]
    public class DbSysAdminOperationLog
    {
        public int Id { get; set; }

        public int AdminId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [UserAgentLength]
        public string Name { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        [FieldLength(2000)]
        public string Message { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        [IpLength]
        public string Ip { get; set; }

        [ShortNameLength]
        public string SessionID { get; set; }

        [UserAgentLength]
        public string UserAgent { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime AddingTime { get; set; }

    }


}
