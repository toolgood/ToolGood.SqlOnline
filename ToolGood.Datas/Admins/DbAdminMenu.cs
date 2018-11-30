using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.Datas
{
    /// <summary>
    /// 管理后台栏目
    /// </summary>
    [Table("AdminMenu")]
    [Index("Code")]
    [Serializable]
    public class DbAdminMenu 
    {
        public int Id { get; set; }
        /// <summary>
        /// 父Id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        [FieldLength(20)]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [FieldLength(20)]
        public string Name { get; set; }
        ///// <summary>
        ///// 图标
        ///// </summary>
        //[FieldLength(20)]
        //public string Icon { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [FieldLength(100)]
        public string Url { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        [FieldLength(100)]
        public string Actions { get; set; }

        #region IAdminBase
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否删除 
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime AddingTime { get; set; }

        #endregion
    }
}
