using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.Datas;

namespace ToolGood.TransDto
{

    public class DatabaseModel
    {
        public int Id { get; set; }


        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// sql 数据库类型
        /// </summary>
        public string SqlServerType { get; set; }


        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        public DbDatabaseInfo ToDatabaseInfo()
        {
            DbDatabaseInfo db = new DbDatabaseInfo() {
                Id = this.Id,
                Name = this.Name,
                ConnectionString = this.ConnectionString,
                SqlServerType = this.SqlServerType,
                AddingTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                Sort = this.Sort,
            };
            return db;
        }

        public UpDatabaseInfo ToUpDatabaseInfo()
        {
            UpDatabaseInfo db = new UpDatabaseInfo() {
                Id = this.Id,
                Name = this.Name,
                ConnectionString = this.ConnectionString,
                SqlServerType = this.SqlServerType,
                LastUpdateTime = DateTime.Now,
                Sort = this.Sort,
            };
            return db;
        }

    }
}
