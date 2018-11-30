using Newtonsoft.Json;
using System.Collections.Generic;
using ToolGood.PluginBase;

namespace ToolGood.TransDto
{
    public class ServerTreeDto
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
        /// 是否为服务器
        /// </summary>
        [JsonProperty("isServer")]
        public bool IsServer { get { return true; } }

        /// <summary>
        /// 是否为数据库
        /// </summary>
        [JsonProperty("isDatabase")]
        public bool IsDatabase { get { return false; } }

        /// <summary>
        /// 是否为表
        /// </summary>
        [JsonProperty("isTable")]
        public bool IsTable { get { return false; } }

        /// <summary>
        /// 同nodes节点，可无限延伸。
        /// </summary>
        [JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        public List<DatabaseTreeDto> Children { get; set; }


        public ServerTreeDto() { }
        public ServerTreeDto(ServerTree serverTree)
        {
            this.Name = serverTree.Name;
            this.Spread = serverTree.Spread;
            Children = new List<DatabaseTreeDto>();
            foreach (var item in serverTree.Children) {
                var dto = new DatabaseTreeDto(item, serverTree.Name);
                Children.Add(dto);
            }
        }

    }


    public class DatabaseTreeDto
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        [JsonProperty("serverName")]
        public string ServerName { get; set; }

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
        /// 是否为服务器
        /// </summary>
        [JsonProperty("isServer")]
        public bool IsServer { get { return false; } }

        /// <summary>
        /// 是否为数据库
        /// </summary>
        [JsonProperty("isDatabase")]
        public bool IsDatabase { get { return true; } }

        /// <summary>
        /// 是否为表
        /// </summary>
        [JsonProperty("isTable")]
        public bool IsTable { get { return false; } }

        /// <summary>
        /// 同nodes节点，可无限延伸。
        /// </summary>
        [JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        public List<TableTreeDto> Children { get; set; }

        public DatabaseTreeDto() { }
        public DatabaseTreeDto(DatabaseTreeNode treeNode, string serverName)
        {
            this.Name = treeNode.Name;
            this.ServerName = serverName;
            this.Spread = treeNode.Spread;
            Children = new List<TableTreeDto>();
            foreach (var item in treeNode.Children) {
                var dto = new TableTreeDto(item, serverName, treeNode.Name);
                Children.Add(dto);
            }
        }

    }


    public class TableTreeDto
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        [JsonProperty("serverName")]
        public string ServerName { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        [JsonProperty("databaseName")]
        public string DatabaseName { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 是否为服务器
        /// </summary>
        [JsonProperty("isServer")]
        public bool IsServer { get { return false; } }

        /// <summary>
        /// 是否为数据库
        /// </summary>
        [JsonProperty("isDatabase")]
        public bool IsDatabase { get { return false; } }

        /// <summary>
        /// 是否为表
        /// </summary>
        [JsonProperty("isTable")]
        public bool IsTable { get { return true; } }

        public TableTreeDto() { }
        public TableTreeDto(TableTreeNode treeNode, string serverName, string databaseName)
        {
            this.Name = treeNode.Name;
            ServerName = serverName;
            DatabaseName = databaseName;
        }

        public TableTreeDto(SqlTableInfo tableInfo, string serverName, string databaseName)
        {
            this.Name = tableInfo.TableName;
            ServerName = serverName;
            DatabaseName = databaseName;
        }

    }
     


}
