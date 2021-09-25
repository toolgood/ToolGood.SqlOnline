/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */

namespace ToolGood.SqlOnline.Datas.Databases
{
    public class ViewColumnEntity
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
        /// 视图名
        /// </summary>
        public string ViewName { get; set; }

        public int Index { get; set; }
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
        /// 备注
        /// </summary>
        public string Comment { get; set; }




        public string GetViewName()
        {
            return $"[{DatabaseName}].[{SchemaName}].[{ViewName}]";
        }

        public override string ToString()
        {
            return $"[{DatabaseName}].[{SchemaName}].[{ViewName}].[{ColumnName}]";
        }


    }

}
