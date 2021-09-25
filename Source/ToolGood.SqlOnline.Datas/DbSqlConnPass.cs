/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.SqlOnline.Datas
{
    [Table("SqlConnPass")]
    public class DbSqlConnPass
    {
        public int Id { get; set; }

        public int AdminGroupId { get; set; }

        public int ConnId { get; set; }

        public bool CanRead { get; set; }

        public bool CanEdit { get; set; }

        public bool CanDelete { get; set; }

        public bool AllPermissions { get; set; }

        public bool CanDownload { get; set; }

        public int ReadMaxRows { get; set; }
        public int ChangeMaxRows { get; set; }



        public DateTime AddingTime { get; set; }
        public int AddingAdminId { get; set; }
        public bool IsDelete { get; set; }
        public DateTime ModifyTime { get; set; }
        public int ModifyAdminId { get; set; }
    }
}
