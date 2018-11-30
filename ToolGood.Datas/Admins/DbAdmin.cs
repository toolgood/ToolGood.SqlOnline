using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.Datas
{
    /// <summary>
    /// 管理员
    /// </summary>
    [Table("Admin")]
    [Serializable]
    public partial class DbAdmin  
    {
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [FieldLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [FieldLength(40)]
        public string Password { get; set; }

        /// <summary>
        /// 真名
        /// </summary>
        [FieldLength(20)]
        public string TrueName { get; set; }

        /// <summary>
        /// 管理组Id
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 管理名
        /// </summary>
        [ResultColumn("AdminGroupName", "(select Name from AdminGroup as ag where ag.Id={0}.GroupId)")]
        public string AdminGroupName { get; set; }

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
