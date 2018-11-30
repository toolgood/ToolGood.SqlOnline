using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.Datas;
using ToolGood.Infrastructure;

namespace ToolGood.Repository
{
    public partial interface IAdminRepository  
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="name"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangePassword(string name, string oldPassword, string newPassword);

        /// <summary>
        /// 创建密码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="isMd5"></param>
        /// <returns></returns>
        string CreatePassword(string user, string password, bool isMd5 = false);


    }

}
