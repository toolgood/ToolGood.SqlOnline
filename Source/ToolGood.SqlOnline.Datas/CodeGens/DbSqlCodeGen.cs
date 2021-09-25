/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */

using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.SqlOnline.Datas
{
    [Table("SqlCodeGen")]
    public class DbSqlCodeGen
    {
        public int Id { get; set; }

        /// <summary>
        /// 1) 表、视图，2）存储过程
        /// </summary>
        public int TplType { get; set; }

        [TitleNameLength]
        public string Title { get; set; }

        [FieldLength(100)]
        public string Namespace { get; set; }

        /// <summary>
        /// 编程语言
        /// </summary>
        [ShortNameLength]
        public string Language { get; set; }

        [Text()]
        public string Content { get; set; }

        [CommentLength]
        public string Comment { get; set; }

        

        public DateTime AddingTime { get; set; }
        public int AddingAdminId { get; set; }
        public bool IsDelete { get; set; }
        public DateTime ModifyTime { get; set; }
        public int ModifyAdminId { get; set; }

    }
}
