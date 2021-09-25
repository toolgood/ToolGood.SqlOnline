/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.SqlOnline.Datas
{
    /// <summary>
    /// IP黑名单
    /// </summary>
    [Table("SysIpBanlist")]
    public class DbSysIpBanlist
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        [IpLength]
        public string Ip { get; set; }

        [ShortNameLength]
        public string Name { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisable { get; set; }


        public DateTime AddingTime { get; set; }
        public int AddingAdminId { get; set; }
        public bool IsDelete { get; set; }
        public DateTime ModifyTime { get; set; }
        public int ModifyAdminId { get; set; }
    }
}
