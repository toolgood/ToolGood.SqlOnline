/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using Newtonsoft.Json;

namespace ToolGood.SqlOnline.Dtos
{
    public class SqlDocDto
    {
        public int Id { get; set; }

        public int AdminId { get; set; }

        public int SqlDocId { get; set; }

        [JsonProperty("sqlType", NullValueHandling = NullValueHandling.Ignore)]
        public string SqlType { get; set; }
        

        public int SqlConnId { get; set; }
        public string DatabaseName { get; set; }

        public string Title { get; set; }

        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }

        public int ContentLen { get; set; }

        public bool IsShare { get; set; }

    }

}
