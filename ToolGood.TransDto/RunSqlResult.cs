using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ToolGood.TransDto
{
    public class RunSqlResult
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("sql")]
        public string Sql { get; set; }

        [JsonProperty("startTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? StartTime { get; set; }

        [JsonProperty("endTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? EndTime { get; set; }

        [JsonProperty("runTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? RunTime { get; set; }

        [JsonProperty("exception", NullValueHandling = NullValueHandling.Ignore)]
        public string Exception { get; set; }

        [JsonProperty("sqlResults", NullValueHandling = NullValueHandling.Ignore)]
        public List<ISqlResult> SqlResults { get; set; }

    }

    public interface ISqlResult { }


    public class SqlCountResult : ISqlResult
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("isRows")]
        public bool IsRows { get { return false; } }
    }

    public class SqlDataTableResult : ISqlResult
    {
        [JsonProperty("isRows")]
        public bool IsRows { get { return true; } }

        [JsonProperty("columns")]
        public List<string> Columns { get; set; }

        [JsonProperty("values")]
        public List<object[]> Values { get; set; }

        public SqlDataTableResult()
        {
            Columns = new List<string>();
            Values = new List<object[]>();
        }
    }



}
