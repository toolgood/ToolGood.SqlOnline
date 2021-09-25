/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.SqlOnline.Datas
{
    [Table("SysAdminGroup_Menu")]
    [Index("GroupId")]
    [Index("MenuId")]
    [Index("MenuCode")]
    [Index("ButtonId")]
    [Index("ButtonCode")]
    public class DbSysAdminGroup_Menu
    {
        public int Id { get; set; }
        /// <summary>
        /// 管理组Id
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 操作名
        /// </summary>
         [ShortNameLength]
        public string MenuCode { get; set; }

        public int ButtonId { get; set; }

        [ShortNameLength]
        public string ButtonCode { get; set; }

    }
}
