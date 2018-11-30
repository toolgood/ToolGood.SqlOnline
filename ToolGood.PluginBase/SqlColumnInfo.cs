using System;
using System.Text;

namespace ToolGood.PluginBase
{

    public class SqlColumnInfo
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 属性名
        /// </summary>
        public string ColumnType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 允许空
        /// </summary>
        public bool AllowNull { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultString { get; set; }
    }


}
