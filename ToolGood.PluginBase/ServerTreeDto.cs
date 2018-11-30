using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.PluginBase
{
    public class ServerTree
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否展开状态（默认false）
        /// </summary>
        public bool Spread { get; set; }

        /// <summary>
        /// 同nodes节点，可无限延伸。
        /// </summary>
        public List<DatabaseTreeNode> Children { get; set; }
    }


    public class DatabaseTreeNode
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否展开状态（默认false）
        /// </summary>
        public bool Spread { get; set; }

        /// <summary>
        /// 同nodes节点，可无限延伸。
        /// </summary>
        public List<TableTreeNode> Children { get; set; }

    }


    public class TableTreeNode
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; set; }

    }


}
