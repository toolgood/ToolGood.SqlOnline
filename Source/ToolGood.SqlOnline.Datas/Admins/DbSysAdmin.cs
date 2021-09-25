/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.SqlOnline.Datas
{
    [Table("SysAdmin")]
    [Index("Name")]
    public class DbSysAdmin
    {
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [UserNameLength]
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [PasswrodLength]
        public string Password { get; set; }

        /// <summary>
        /// 管理密码
        /// </summary>
        [PasswrodLength]
        public string ManagerPassword { get; set; }

        /// <summary>
        /// 盐值
        /// </summary>
        [PasswrodLength]
        public string Salt { get; set; }

        /// <summary>
        /// 真名
        /// </summary>
        [UserNameLength]
        public string TrueName { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [UserNameLength]
        public string JobNo { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [PhoneLength]
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [EmailLength]
        public string Email { get; set; }

        /// <summary>
        /// 冻结状态，0未冻结，1冻结
        /// </summary>
        public int IsFrozen { get; set; }

        /// <summary>
        /// 最后一次浏览器密码
        /// </summary>
        [FieldLength(50)]
        public string LastBrowserPassword { get; set; }

        /// <summary>
        /// 最后一次SessionID
        /// </summary>
        [FieldLength(250)]
        public string LastSessionID { get; set; }


        public DateTime AddingTime { get; set; }
        public int AddingAdminId { get; set; }
        public bool IsDelete { get; set; }
        public DateTime ModifyTime { get; set; }
        public int ModifyAdminId { get; set; }
    }


}
