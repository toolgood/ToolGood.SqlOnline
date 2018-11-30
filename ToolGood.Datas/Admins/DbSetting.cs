using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.Datas
{
    [Table("Setting")]
    [Index("Category")]
    [Index("Key")]
    [Serializable]
    public class DbSetting
    {
        public int Id { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        [FieldLength(50)]
        public string Category { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        [FieldLength(50)]
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [FieldLength(100)]
        public string Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [FieldLength(4000)]
        public string Describe { get; set; }


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
