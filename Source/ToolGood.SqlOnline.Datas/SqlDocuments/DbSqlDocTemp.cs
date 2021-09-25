/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.SqlOnline.Datas
{
    [Table("SqlDocTemp")]
    public class DbSqlDocTemp
    {
        public int Id { get; set; }

        public int SqlDocId { get; set; }

        public int SqlConnId { get; set; }

        public string DatabaseName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int ContentLen { get; set; }


        public int AdminId { get; set; }


        public DateTime AddingTime { get; set; }
        public int AddingAdminId { get; set; }
        public bool IsDelete { get; set; }
        public DateTime ModifyTime { get; set; }
        public int ModifyAdminId { get; set; }

    }
}
