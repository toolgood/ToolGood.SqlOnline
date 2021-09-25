/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ToolGood.SqlOnline.Dto
{
    public class ExecuteResultItem
    {
        [JsonProperty("exception", NullValueHandling = NullValueHandling.Ignore)]
        public string Exception { get; set; }

        [JsonProperty("sql", NullValueHandling = NullValueHandling.Ignore)]
        public string Sql { get; set; }

        [JsonProperty("startTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? StartTime { get; set; }

        [JsonProperty("runTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? RunTime { get; set; }

        [JsonProperty("changeCount", NullValueHandling = NullValueHandling.Ignore)]
        public int? ChangeCount { get; set; }

        [JsonProperty("isOverflow", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsOverflow { get; set; }

        [JsonProperty("columns", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Columns { get; set; }
 

        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public List<object[]> Values { get; set; }


        [JsonProperty("oldData", NullValueHandling = NullValueHandling.Ignore)]
        public string OldData { get; set; }
    }


}
