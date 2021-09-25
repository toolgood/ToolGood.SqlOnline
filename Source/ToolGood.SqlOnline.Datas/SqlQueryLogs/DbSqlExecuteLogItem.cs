/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using Newtonsoft.Json;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.SqlOnline.Datas
{
    [Table("SqlExecuteLogItem")]
    public class DbSqlExecuteLogItem
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int SqlExecuteLogId { get; set; }

        [FieldLength(4000)]
        public string Sql { get; set; }

        [Text]
        public string OldData { get; set; }
    }


}
