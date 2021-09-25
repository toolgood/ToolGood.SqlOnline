/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using Newtonsoft.Json;

namespace ToolGood.SqlOnline.Dtos
{
    public class SqlTreeDto
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("pId")]
        public string pId { get; set; }

        /// <summary>
        /// <para>节点名称。</para> 
        /// <para>1、如果不使用 name 属性保存节点名称，请修改 setting.data.key.name</para> 
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }
        /// <summary>
        ///  <para> 记录 treeNode 节点是否为父节点。</para> 
        ///  <para> 1、初始化节点数据时，根据 treeNode.children 属性判断，有子节点则设置为 true，否则为 false</para> 
        ///  <para> 2、初始化节点数据时，如果设定 treeNode.isParent = true，即使无子节点数据，也会设置为父节点</para> 
        ///  <para> 3、为了解决部分朋友生成 json 数据出现的兼容问题, 支持 "false","true" 字符串格式的数据</para> 
        /// </summary>
        [JsonProperty("isParent", NullValueHandling = NullValueHandling.Ignore)]
        public bool? isParent { get; set; }
        /// <summary>
        /// <para> 记录 treeNode 节点的 展开 / 折叠 状态。</para> 
        /// <para> 1、初始化节点数据时，如果设定 treeNode.open = true，则会直接展开此节点</para> 
        /// <para> 2、叶子节点 treeNode.open = false</para> 
        /// <para> 3、为了解决部分朋友生成 json 数据出现的兼容问题, 支持 "false","true" 字符串格式的数据</para> 
        /// <para> 4、非异步加载模式下，无子节点的父节点设置 open = true 后，可显示为展开状态，但异步加载模式下不会生效。（v3.5.15+） </para> 
        /// </summary>
        [JsonProperty("open", NullValueHandling = NullValueHandling.Ignore)]
        public bool? open { get; set; }


        /// <summary>
        /// <para>节点自定义图标的 URL 路径。</para> 
        /// <para>1、父节点如果只设置 icon ，会导致展开、折叠时都使用同一个图标</para> 
        /// <para>2、父节点展开、折叠使用不同的个性化图标需要同时设置 treeNode.iconOpen / treeNode.iconClose 两个属性</para> 
        /// <para>3、如果想利用 className 设置个性化图标，需要设置 treeNode.iconSkin 属性</para> 
        /// </summary>
        [JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore)]
        public string icon { get; set; }


        [JsonProperty("sqlConnId", NullValueHandling = NullValueHandling.Ignore)]
        public int SqlConnId { get; set; }

        [JsonProperty("sqlType", NullValueHandling = NullValueHandling.Ignore)]
        public string SqlType { get; set; }

        [JsonProperty("database", NullValueHandling = NullValueHandling.Ignore)]
        public string Database { get; set; }

        [JsonProperty("schema", NullValueHandling = NullValueHandling.Ignore)]
        public string Schema { get; set; }

        [JsonProperty("searchType", NullValueHandling = NullValueHandling.Ignore)]
        public string SearchType { get; set; }

        [JsonProperty("search", NullValueHandling = NullValueHandling.Ignore)]
        public string Search { get; set; }



    }
}
