/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.SqlOnline.Datas
{
    [Table("SysAdminMenuCheck")]
    [Index("MenuId")]
    [Index("MenuCode")]
    [Index("ButtonCode")]
    public class DbSysAdminMenuCheck
    {
        public int Id { get; set; }

        public int MenuId { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        [ShortNameLength]
        public string MenuCode { get; set; }

        [ShortNameLength]
        public string ButtonCode { get; set; }
 

        public DateTime AddingTime { get; set; }
        public int AddingAdminId { get; set; }
        public bool IsDelete { get; set; }

    }
}
