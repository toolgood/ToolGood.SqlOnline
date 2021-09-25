/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using Newtonsoft.Json;

namespace ToolGood.SqlOnline.Dtos
{
    public class AdminSqlDocDto
    {
        public int Id { get; set; }

        [JsonProperty("adminName", NullValueHandling = NullValueHandling.Ignore)]
        public string AdminName { get; set; }
        [JsonProperty("adminJobNo", NullValueHandling = NullValueHandling.Ignore)]
        public string AdminJobNo { get; set; }
        [JsonProperty("adminTrueName", NullValueHandling = NullValueHandling.Ignore)]
        public string AdminTrueName { get; set; }
        [JsonProperty("adminPhone", NullValueHandling = NullValueHandling.Ignore)]
        public string AdminPhone { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
        [JsonIgnore]
        public string Content { get; set; }
        public int ContentLen { get; set; }
        [JsonProperty("sqlType", NullValueHandling = NullValueHandling.Ignore)]
        public string SqlType { get; set; }
        [JsonProperty("sqlDocType", NullValueHandling = NullValueHandling.Ignore)]
        public int? SqlDocType { get; set; }

        [JsonProperty("sqlConnName", NullValueHandling = NullValueHandling.Ignore)]
        public string SqlConnName { get; set; }
        [JsonProperty("databaseName", NullValueHandling = NullValueHandling.Ignore)]
        public string DatabaseName { get; set; }

        [JsonProperty("lastOpenTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastOpenTime { get; set; }



        [JsonProperty("modifyTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ModifyTime { get; set; }

    }

}
