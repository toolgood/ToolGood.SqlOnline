/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ToolGood.ReadyGo3.Attributes;
using ToolGood.SqlOnline.Datas.Enums;

namespace ToolGood.SqlOnline.Datas
{
    [Table("SqlExecuteLog")]
    public class DbSqlExecuteLog
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int AdminId { get; set; }

        [FieldLength(20)]
        public string AdminName { get; set; }

        [FieldLength(20)]
        public string AdminTrueName { get; set; }

        [FieldLength(20)]
        public string AdminJobNo { get; set; }

        [FieldLength(20)]
        public string AdminPhone { get; set; }

        public int SqlConnectionId { get; set; }

        [FieldLength(20)]
        public string SqlConnectionName { get; set; }

        [FieldLength(50)]
        public string DatabaseName { get; set; }

        [FieldLength(4000)]
        public string Sql { get; set; }

        [Text]
        public string Exception { get; set; }

        public EnumRunReadResult RunReadResult { get; set; }

        public int RunTime { get; set; }

        public DateTime AddingTime { get; set; }

        public List<DbSqlExecuteLogItem> Items { get; set; }
    }


}
