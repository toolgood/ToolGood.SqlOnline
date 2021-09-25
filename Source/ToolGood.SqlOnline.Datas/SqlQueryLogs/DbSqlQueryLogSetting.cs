/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.SqlOnline.Datas
{
    [Table("SqlQueryLogSetting")]
    public class DbSqlQueryLogSetting
    {
        public int Id { get; set; }

        /// <summary>
        /// 1）使用Sqlite存日志 
        /// 2）使用http请求
        /// 3）使用Sqlserver
        /// 4) 使用Mysql
        /// </summary>
        public int LogType { get; set; }

        [FieldLength(200)]
        public string Name { get; set; }

        [FieldLength(200)]
        public string Data { get; set; }

        /// <summary>
        /// 冻结状态，0未冻结，1冻结
        /// </summary>
        public int IsFrozen { get; set; }

        public DateTime AddingTime { get; set; }
        public int AddingAdminId { get; set; }
        public bool IsDelete { get; set; }
        public DateTime ModifyTime { get; set; }
        public int ModifyAdminId { get; set; }
    }

}
