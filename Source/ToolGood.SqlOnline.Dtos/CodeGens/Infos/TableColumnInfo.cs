/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using Newtonsoft.Json;

namespace ToolGood.SqlOnline.Dtos.CodeGens
{
    public class TableColumnInfo
    {
        /// <summary>
        /// 序号
        /// </summary>
        [JsonProperty("index")]
        public int Index { get; set; }

        /// <summary>
        /// 是否为主键
        /// </summary>
        [JsonProperty("isPrimaryKey")]
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        [JsonProperty("columnName")]
        public string ColumnName { get; set; }

        /// <summary>
        /// 列类型
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// 列长度
        /// </summary>
        [JsonProperty("length")]
        public string Length { get; set; }

        [JsonProperty("precision")]
        public string Precision { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        [JsonProperty("scale")]
        public string Scale { get; set; }

        /// <summary>
        /// 是否可空
        /// </summary>
        [JsonProperty("isNullAble")]
        public bool IsNullAble { get; set; }


        /// <summary>
        /// 列默认值
        /// </summary>
        [JsonProperty("defaultValue")]
        public string DefaultValue { get; set; }

        /// <summary>
        /// 是否自动增长
        /// </summary>
        [JsonProperty("isIdentity")]
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; }
    }

}
