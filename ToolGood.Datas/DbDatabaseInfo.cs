using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.Datas
{
    /// <summary>
    /// 数据库
    /// </summary>
    [Table("DatabaseInfo")]
    [Serializable]
    public class DbDatabaseInfo
    {
        public int Id { get; set; }


        /// <summary>
        /// 数据库名称
        /// </summary>
        [FieldLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 读取连接字符串
        /// </summary>
        [FieldLength(200)]
        public string ReadConnectionString { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        [FieldLength(200)]
        public string RoleName { get; set; }

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
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否删除 
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime AddingTime { get; set; }

        #endregion

        /// <summary>
        /// 管理员ID
        /// </summary>
        public int CreateAdminId { get; set; }

        /// <summary>
        /// 管理员姓名
        /// </summary>
        [FieldLength(20)]
        public string CreateAdminName { get; set; }

        /// <summary>
        /// 管理员ID
        /// </summary>
        public int ChangeAdminId { get; set; }

        /// <summary>
        /// 管理员姓名
        /// </summary>
        [FieldLength(20)]
        public string ChangeAdminName { get; set; }



    }
}
