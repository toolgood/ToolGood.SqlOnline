using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.ApplicationCommon
{

    public class LayuiTreeNode
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 是否展开状态（默认false）
        /// </summary>
        [JsonProperty("spread", NullValueHandling = NullValueHandling.Ignore)]
        public bool Spread { get; set; }

        /// <summary>
        /// 节点链接（可选），未设则不会跳转
        /// </summary>
        [JsonProperty("href", NullValueHandling = NullValueHandling.Ignore)]
        public string Href { get; set; }

        /// <summary>
        /// 同nodes节点，可无限延伸。
        /// </summary>
        [JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        public List<LayuiTreeNode> Children { get; set; }

        /// <summary>
        /// 自定义参数
        /// </summary>
        [JsonProperty("context", NullValueHandling = NullValueHandling.Ignore)]
        public string context { get; set; }
    }
}
