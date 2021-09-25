/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ToolGood.SqlOnline.Dtos.CodeGens
{
    public class ProcedureInfo
    {
        [JsonProperty("adminName")]
        public string AdminName { get; set; }

        [JsonProperty("operationTime")]
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("serverName")]
        public string ServerName { get; set; }

        [JsonProperty("serverType")]
        public string ServerType { get; set; }

        /// <summary>
        /// 数据库名
        /// </summary>
        [JsonProperty("databaseName")]
        public string DatabaseName { get; set; }

        /// <summary>
        /// Schema名
        /// </summary>
        [JsonProperty("schemaName")]
        public string SchemaName { get; set; }

        [JsonProperty("namespace")]
        public string Namespace { get; set; }


        [JsonProperty("procedureName")]
        public string ProcedureName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("params")]
        public List<ProcedureParameter> Params { get; set; }
    }
}
