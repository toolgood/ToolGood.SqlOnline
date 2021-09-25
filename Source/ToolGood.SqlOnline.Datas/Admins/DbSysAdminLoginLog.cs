/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.SqlOnline.Datas
{
    [Table("SysAdminLoginLog")]
    [Index("Name")]
    public class DbSysAdminLoginLog
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [UserNameLength]
        public string Name { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        [CommentLength]
        public string Message { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        [IpLength]
        public string Ip { get; set; }

        [ShortNameLength]
        public string SessionID { get; set; }


        public bool Success { get; set; }

        [UserAgentLength]
        public string UserAgent { get; set; }

        [FieldLength(50)]
        public string MachineCode { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime AddingTime { get; set; }
    }


}
