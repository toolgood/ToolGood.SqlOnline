using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.Datas
{
    /// <summary>
    /// 查询日志
    /// </summary>
    [Table("SqlQueryLog")]
    [Serializable]
    public class DbSqlQueryLog
    {
        public int Id { get; set; }

        /// <summary>
        /// 管理员ID
        /// </summary>
        public int AdminId { get; set; }

        /// <summary>
        /// 管理员姓名
        /// </summary>
        [FieldLength(20)]
        public string AdminName { get; set; }

        /// <summary>
        /// 数据库ID
        /// </summary>
        public int DatabaseInfoId { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        [FieldLength(20)]
        public string DatabaseInfoName { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        [FieldLength(50)]
        public string SqlServerType { get; set; }

        /// <summary>
        /// sql类型
        /// </summary>
        [FieldLength(20)]
        public string SqlType { get; set; }

        /// <summary>
        /// sql
        /// </summary>
        [FieldLength(4000)]
        public string Sql { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 是否删除 
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime AddingTime { get; set; }


    }
}
