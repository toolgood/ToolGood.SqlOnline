using System.Collections.Generic;

namespace ToolGood.PluginBase
{
    public class SqlTableInfo
    {
        /// <summary>
        /// 数据库名
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// 模式名;
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool UseSchema { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }


        public List<SqlColumnInfo> Columns { get; set; }
    }


}
