/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Datas.Databases
{
    public class TableShowEntity
    {
        /// <summary>
        /// Schema名
        /// </summary>
        public string SchemaName { get; set; }

        public string TableType { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }

        public string Comment { get; set; }


        /// <summary>
        /// 序号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 列类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 列长度
        /// </summary>
        public string Length { get; set; }
        public string Precision { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        public string Scale { get; set; }

        /// <summary>
        /// 是否可空
        /// </summary>
        public bool IsNullAble { get; set; }


        /// <summary>
        /// 列默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 是否自动增长
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string ColumnComment { get; set; }
    }

}
