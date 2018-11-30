using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.Datas
{
    /// <summary>
    /// 数据库权限
    /// </summary>
    [Table("AdminDatabasePass")]
    [Index("AdminGroupId")]
    [Serializable]
    public class DbAdminDatabasePass
    {
        /// <summary>
        /// 管理组Id
        /// </summary>
        public int AdminGroupId { get; set; }

        /// <summary>
        /// 数据库ID
        /// </summary>
        public int DatabaseInfoId { get; set; }
    }
}
