using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.Datas
{
    /// <summary>
    /// 数据库
    /// </summary>
    [Table("DatabaseInfoHistory")]
    [Serializable]
    public class DbDatabaseInfoHistory
    {
        public int Id { get; set; }

        /// <summary>
        /// 数据库ID
        /// </summary>
        public int DatabaseInfoId { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        [FieldLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        [FieldLength(200)]
        public string ConnectionString { get; set; }

        /// <summary>
        /// sql 数据库类型
        /// </summary>
        [FieldLength(50)]
        public string SqlServerType { get; set; }



        #region IAdminBase

        /// <summary>
        /// 是否删除 
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime AddingTime { get; set; }

        #endregion

        /// <summary>
        /// 管理员ID
        /// </summary>
        public int ChangeAdminId { get; set; }

        /// <summary>
        /// 管理员姓名
        /// </summary>
        [FieldLength(20)]
        public string ChangeAdminName { get; set; }

        public DbDatabaseInfoHistory() { }

        public DbDatabaseInfoHistory(DbDatabaseInfo db)
        {
            DatabaseInfoId = db.Id;
            Name = db.Name;
            ConnectionString = db.ConnectionString;
            SqlServerType = db.SqlServerType;
            IsDelete = false;
            AddingTime = DateTime.Now;
            ChangeAdminId = db.ChangeAdminId;
            ChangeAdminName = db.ChangeAdminName;
        }

    }
}
