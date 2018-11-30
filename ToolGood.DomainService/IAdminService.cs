using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.TransDto;

namespace ToolGood.DomainService
{
    public interface IAdminService
    {
        /// <summary>
        /// 创建Token
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        string CreateToken(AdminDto dto);
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        AdminDto GetToken(string cookie);


        /// <summary>
        /// 核对管理员
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool CheckAdmin(AdminDto token);

        /// <summary>
        /// 更新盐值，全部重新登录
        /// </summary>
        void ChangeSaltValue();
    }
}
