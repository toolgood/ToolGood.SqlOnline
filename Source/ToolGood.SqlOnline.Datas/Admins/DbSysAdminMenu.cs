/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.SqlOnline.Datas
{
    [Table("SysAdminMenu")]
    [Index("ParentId")]
    [Index("MenuCode")]
    public class DbSysAdminMenu
    {
        public int Id { get; set; }

        [Ignore]
        public string Ids {
            get {
                return ParentIds + Id + "-";
            }
        }

        /// <summary>
        /// 父Id的完整
        /// </summary>
        [FieldLength(200)]
        public string ParentIds { get; set; }
        /// <summary>
        /// 父Id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        [ShortNameLength]
        public string MenuCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [ShortNameLength]
        public string MenuName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [CommentLength]
        public string Comment { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [UrlLength]
        public string Url { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        [ShortNameLength]
        public string Buttons { get; set; }

        public int OrderNum { get; set; }

        public bool IsDelete { get; set; }

    }
}
