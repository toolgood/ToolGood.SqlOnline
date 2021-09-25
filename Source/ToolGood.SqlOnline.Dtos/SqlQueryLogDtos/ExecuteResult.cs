/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ToolGood.SqlOnline.Dto
{
    public class ExecuteResult
    {
        [JsonIgnore]
        public bool IsException { get; set; }

        [JsonProperty("exception", NullValueHandling = NullValueHandling.Ignore)]
        public string Exception { get; set; }

        [JsonProperty("startTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? StartTime { get; set; }

        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public List<ExecuteResultItem> Result { get; set; }

    }


}
