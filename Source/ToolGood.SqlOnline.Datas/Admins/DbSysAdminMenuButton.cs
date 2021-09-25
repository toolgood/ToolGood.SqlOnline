/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.SqlOnline.Datas
{
    [Table("SysAdminMenuButton")]
    [Index("ButtonName")]
    [Index("ButtonCode")]
    public class DbSysAdminMenuButton
    {
        public int Id { get; set; }

        [ShortNameLength]
        public string ButtonCode { get; set; }

        [ShortNameLength]
        public string ButtonName { get; set; }

        public int OrderNum { get; set; }

        public bool IsDelete { get; set; }
    }
}
