using System.Collections.Generic;
using ToolGood.Datas;
using ToolGood.Infrastructure;

namespace ToolGood.Repository
{
    public partial interface IAdminMenuRepository 
    {
        /// <summary>
        /// 获取顶层菜单
        /// </summary>
        /// <param name="groudID">管理组ID</param>
        /// <returns></returns>
        List<DbAdminMenu> GetTopAdminMenu(int groudID);

        /// <summary>
        /// 获取非顶层菜单，二层
        /// </summary>
        /// <param name="groudID">管理组ID</param>
        /// <param name="topId">顶层菜单ID</param>
        /// <returns></returns>
        List<DbAdminMenu> GetAdminMenu(int groudID, int topId);
    }

}
