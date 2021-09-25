/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.SqlOnline.Datas
{
    [Table("SysAdminGroup")]
    [Index("Name")]
    public class DbSysAdminGroup
    {
        public int Id { get; set; }

        /// <summary>
        /// 管理组名称
        /// </summary>
        [UserNameLength]
        public string Name { get; set; }

        /// <summary>
        /// 描述 200
        /// </summary>
        [CommentLength]
        public string Comment { get; set; }

        public int OrderNum { get; set; }

        public bool IsSystem { get; set; }

        public DateTime AddingTime { get; set; }
        public int AddingAdminId { get; set; }
        public bool IsDelete { get; set; }
        public DateTime ModifyTime { get; set; }
        public int ModifyAdminId { get; set; }
    }
}
