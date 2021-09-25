/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using Newtonsoft.Json;

namespace ToolGood.WebCommon
{
    public class ZTreeNode2
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
        /// <para> 判断 treeNode 节点是否被隐藏。</para> 
        /// <para>1、初始化 zTree 时，如果节点设置 isHidden = true，会被自动隐藏</para> 
        /// <para>2、请勿对已加载的节点修改此属性，隐藏 / 显示 请使用 hideNode() / hideNodes() / showNode() / showNodes() 方法</para> 
        /// </summary>
        [JsonProperty("isHidden", NullValueHandling = NullValueHandling.Ignore)]
        public bool? isHidden { get; set; }
        /// <summary>
        /// 设置点击节点后在何处打开 url。[treeNode.url 存在时有效]
        /// </summary>
        [JsonProperty("target", NullValueHandling = NullValueHandling.Ignore)]
        public string target { get; set; }
        /// <summary>
        /// <para>节点链接的目标 URL</para> 
        /// <para>1、编辑模式(setting.edit.enable = true) 下此属性功能失效，如果必须使用类似功能，请利用 onClick 事件回调函数自行控制。</para> 
        /// <para>2、如果需要在 onClick 事件回调函数中进行跳转控制，那么请将 URL 地址保存在其他自定义的属性内，请勿使用 url</para> 
        /// </summary>
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string url { get; set; }


        /// <summary>
        /// <para>节点的 checkBox / radio 的 勾选状态。[setting.check.enable = true &amp; treeNode.nocheck = false 时有效]</para> 
        /// <para>1、如果不使用 checked 属性设置勾选状态，请修改 setting.data.key.checked</para> 
        /// <para>2、建立 treeNode 数据时设置 treeNode.checked = true 可以让节点的输入框默认为勾选状态</para> 
        /// <para>3、修改节点勾选状态，可以使用 treeObj.checkNode / checkAllNodes / updateNode 方法，具体使用哪种请根据自己的需求而定</para> 
        /// <para>4、为了解决部分朋友生成 json 数据出现的兼容问题, 支持 "false","true" 字符串格式的数据</para> 
        /// </summary>
        [JsonProperty("checked", NullValueHandling = NullValueHandling.Ignore)]
        public bool? isChecked { get; set; }
        /// <summary>
        /// <para>1、设置节点的 checkbox / radio 是否禁用 [setting.check.enable = true 时有效]</para> 
        /// <para>2、为了解决部分朋友生成 json 数据出现的兼容问题, 支持 "false","true" 字符串格式的数据</para> 
        /// <para>3、请勿对已加载的节点修改此属性，禁止 或 取消禁止 请使用 setChkDisabled() 方法</para> 
        /// <para>4、初始化时，如果需要子孙节点继承父节点的 chkDisabled 属性，请设置 setting.check.chkDisabledInherit 属性</para> 
        /// </summary>
        [JsonProperty("chkDisabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? chkDisabled { get; set; }
        /// <summary>
        /// <para> 强制节点的 checkBox / radio 的 半勾选状态。[setting.check.enable = true &amp; treeNode.nocheck = false 时有效]</para> 
        /// <para>1、强制为半勾选状态后，不再进行自动计算半勾选状态</para> 
        /// <para>2、设置 treeNode.halfCheck = false 或 null 才能恢复自动计算半勾选状态</para> 
        /// <para>3、为了解决部分朋友生成 json 数据出现的兼容问题, 支持 "false","true" 字符串格式的数据</para> 
        /// </summary>
        [JsonProperty("halfCheck", NullValueHandling = NullValueHandling.Ignore)]
        public bool? halfCheck { get; set; }
        /// <summary>
        /// <para>1、设置节点是否隐藏 checkbox / radio [setting.check.enable = true 时有效]</para> 
        /// <para>2、为了解决部分朋友生成 json 数据出现的兼容问题, 支持 "false","true" 字符串格式的数据</para> 
        /// </summary>
        [JsonProperty("nocheck", NullValueHandling = NullValueHandling.Ignore)]
        public bool? nocheck { get; set; }


        /// <summary>
        /// <para>节点自定义图标的 URL 路径。</para> 
        /// <para>1、父节点如果只设置 icon ，会导致展开、折叠时都使用同一个图标</para> 
        /// <para>2、父节点展开、折叠使用不同的个性化图标需要同时设置 treeNode.iconOpen / treeNode.iconClose 两个属性</para> 
        /// <para>3、如果想利用 className 设置个性化图标，需要设置 treeNode.iconSkin 属性</para> 
        /// </summary>
        [JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore)]
        public string icon { get; set; }
        /// <summary>
        /// <para>父节点自定义展开时图标的 URL 路径。</para> 
        /// <para>1、此属性只针对父节点有效</para> 
        /// <para>2、此属性必须与 iconClose 同时使用</para> 
        /// <para>3、如果想利用 className 设置个性化图标，需要设置 treeNode.iconSkin 属性</para> 
        /// </summary>
        [JsonProperty("iconOpen", NullValueHandling = NullValueHandling.Ignore)]
        public string iconOpen { get; set; }
        /// <summary>
        /// <para>父节点自定义折叠时图标的 URL 路径。 </para> 
        /// <para>1、此属性只针对父节点有效 </para> 
        /// <para>2、此属性必须与 iconOpen 同时使用 </para> 
        /// <para>3、如果想利用 className 设置个性化图标，需要设置 treeNode.iconSkin 属性</para> 
        /// </summary>
        [JsonProperty("iconClose", NullValueHandling = NullValueHandling.Ignore)]
        public string iconClose { get; set; }
        /// <summary>
        /// <para>节点自定义图标的 className</para> 
        /// <para>1、需要修改 css，增加相应 className 的设置</para> 
        /// <para>2、css 方式简单、方便，并且同时支持父节点展开、折叠状态切换图片</para> 
        /// <para>3、css 建议采用图片分割渲染的方式以减少反复加载图片，并且避免图片闪动</para> 
        /// <para>4、zTree v3.x 的 iconSkin 同样支持 IE6</para> 
        /// <para>5、如果想直接使用 图片的Url路径 设置节点的个性化图标，需要设置 treeNode.icon / treeNode.iconOpen / treeNode.iconClose 属性</para> 
        /// </summary>
        [JsonProperty("iconSkin", NullValueHandling = NullValueHandling.Ignore)]
        public string iconSkin { get; set; }

        [JsonProperty("extend", NullValueHandling = NullValueHandling.Ignore)]
        public object extend { get; set; }
        /// <summary>
        /// 字体
        /// </summary>
        [JsonProperty("font", NullValueHandling = NullValueHandling.Ignore)]
        public object font { get; set; }

    }

}
