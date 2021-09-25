/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.SqlOnline.Datas
{

    [Table("SysSettingValue")]
    [Index("CategoryName")]
    [Index("SettingName")]
    [Index("Code")]
    public class DbSysSettingValue
    {
        public int Id { get; set; }

        [ShortNameLength]
        public string CategoryName { get; set; }

        [ShortNameLength]
        public string SettingName { get; set; }

        [ShortNameLength]
        public string Code { get; set; }

        [Text]
        public string Value { get; set; }

        [CommentLength]
        public string Comment { get; set; }

 
        public bool IsDelete { get; set; }
        public DateTime ModifyTime { get; set; }
        public int ModifyAdminId { get; set; }
    }
}
