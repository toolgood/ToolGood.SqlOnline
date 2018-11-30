using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.Datas;
using ToolGood.Infrastructure;

namespace ToolGood.TransDto
{
    public class AdminDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PasswordMd5 { get; set; }

        /// <summary>
        /// 真名
        /// </summary>
        public string TrueName { get; set; }

        /// <summary>
        /// 管理组Id
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 管理名
        /// </summary>
        public string AdminGroupName { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiryTime { get; set; }

        public AdminDto() { }

        public AdminDto(DbAdmin admin)
        {
            Id = admin.Id;
            Name = admin.Name;
            PasswordMd5 = Hash.GetMd5String(admin.Password);
            TrueName = admin.TrueName;
            GroupId = admin.GroupId;
            AdminGroupName = admin.AdminGroupName;
        }

    }
}
