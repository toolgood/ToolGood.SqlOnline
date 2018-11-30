using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.Datas
{
    /// <summary>
    /// 登录日记
    /// </summary>
    [Table("AdminLoginLog")]
    [Serializable]
    public class DbAdminLoginLog
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [FieldLength(20)]
        public string Name { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        [FieldLength(20)]
        public string Message { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        [FieldLength(50)]
        public string Ip { get; set; }

        [FieldLength(40)]
        public string SessionID { get; set; }

        [FieldLength(50)]
        public string IpAddress { get; set; }


        public bool Success { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime AddingTime { get; set; }



    }
}
