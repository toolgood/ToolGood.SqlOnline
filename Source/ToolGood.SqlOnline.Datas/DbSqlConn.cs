/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.SqlOnline.Datas
{
    /// <summary>
    /// 连接字符串
    /// </summary>
    [Table("SqlConn")]
    public class DbSqlConn
    {
        public int Id { get; set; }

        /// <summary>
        /// 连接字符串名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        public string SqlType { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        [FieldLength(200)]
        public string ConnectionString { get; set; }

        public bool UseDevelopment { get; set; }

        public DateTime AddingTime { get; set; }
        public int AddingAdminId { get; set; }
        public bool IsDelete { get; set; }
        public DateTime ModifyTime { get; set; }
        public int ModifyAdminId { get; set; }

    }
}
