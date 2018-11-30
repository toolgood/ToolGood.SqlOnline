using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.Datas
{
    /// <summary>
    /// 管理员分组
    /// </summary>
    [Table("AdminGroup")]
    [Serializable]
    public class DbAdminGroup  
    {
        public int Id { get; set; }

        #region IBaseContent
        /// <summary>
        /// 管理组名称
        /// </summary>
        [FieldLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 描述 200
        /// </summary>
        [FieldLength(200)]
        public string Description { get; set; } 
        #endregion

        #region IBase
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 是否删除 
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime AddingTime { get; set; }

        #endregion

    }
}
