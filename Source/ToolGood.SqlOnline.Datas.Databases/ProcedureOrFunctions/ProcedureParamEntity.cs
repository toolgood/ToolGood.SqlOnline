/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */

namespace ToolGood.SqlOnline.Datas.Databases
{
    public class ProcedureParamEntity
    {
        /// <summary>
        /// 数据库名
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Schema名
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// 存储过程名
        /// </summary>
        public string ProcedureName { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string ParamName { get; set; }

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


        public bool HasDefaultValue { get; set; }
        /// <summary>
        /// 列默认值
        /// </summary>
        public string DefaultValue { get; set; }

        public bool IsOutput { get; set; }

        

        public string GetColumnType()
        {
            var ColumnType = Type.ToLower();
            if (ColumnType == "varchar" || ColumnType == "nvarchar" || ColumnType == "char" || ColumnType == "nchar") {
                ColumnType = ColumnType + "(" + Length + ")";
            } else if (ColumnType == "decimal" || ColumnType == "numeric") {
                ColumnType = ColumnType + "(" + Precision + "," + Scale + ")";
            }
            return ColumnType;
        }
    }

}
