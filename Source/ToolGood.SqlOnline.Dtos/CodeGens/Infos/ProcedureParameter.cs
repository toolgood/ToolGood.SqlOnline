/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using Newtonsoft.Json;

namespace ToolGood.SqlOnline.Dtos.CodeGens
{
    public class ProcedureParameter
    {
        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("paramName")]
        public string ParamName { get; set; }

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
        /// 是否输出
        /// </summary>
        [JsonProperty("IsOutput")]
        public bool IsOutput { get; set; }
    }
}
