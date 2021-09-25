/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Text.Json.Serialization;

namespace ToolGood.SqlOnline.Dtos
{
    public class SqlConnPassDto
    {
        public int Id { get; set; }

        [JsonIgnore]
        public int AdminGroupId { get; set; }

        public string AdminGroupName { get; set; }


        [JsonIgnore]
        public int ConnId { get; set; }


        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public string SqlType { get; set; }


        /// <summary>
        /// 连接字符串名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        public bool CanRead { get; set; }

        public bool CanEdit { get; set; }

        public bool CanDelete { get; set; }
        public bool AllPermissions { get; set; }

        public bool CanDownload { get; set; }


        public int ReadMaxRows { get; set; }
        public int ChangeMaxRows { get; set; }

    }
}
