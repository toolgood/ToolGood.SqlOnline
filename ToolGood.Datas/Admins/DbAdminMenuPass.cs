using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.Datas
{
    /// <summary>
    /// 栏目权限
    /// </summary>
    [Table("AdminMenuPass")]
    [Index("AdminGroupId")]
    [Index("Code")]
    [Serializable]
    public class DbAdminMenuPass 
    {
        /// <summary>
        /// 管理组Id
        /// </summary>
        public int AdminGroupId { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 操作名
        /// </summary>
        [FieldLength(30)]
        public string Code { get; set; }

        /// <summary>
        /// 操作名
        /// </summary>
        [FieldLength(30)]
        public string ActionName { get; set; }

    }
}
