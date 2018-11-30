using System.Collections.Generic;
using ToolGood.Datas;
using ToolGood.Infrastructure;
using ToolGood.TransDto.Admins;

namespace ToolGood.Application
{
    public interface IAdminMenuApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="topId"></param>
        /// <returns></returns>
        OperateResult<List<TopAdminMenu>> GetTopAdminMenu(int roleId, int topId = 1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="topId"></param>
        /// <returns></returns>
        OperateResult<List<TopAdminMenu>> GetTopAdminMenu(int roleId, string code);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="topId"></param>
        /// <returns></returns>
        OperateResult<List<TreeAdminMenu>> GetAdminMenu(int roleId, int topId = 1);

        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        OperateResult<List<DbAdminMenu>> GetAdminMenuAll();

        /// <summary>
        /// 获取上级菜单
        /// </summary>
        /// <returns></returns>
        OperateResult<List<DbAdminMenu>> GetParentAdminMenu();

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperateResult<DbAdminMenu> GetMenuById(int id);

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="url"></param>
        /// <param name="actions"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        OperateResult AddMenu(int pid, string name, string code, string url, string actions, int sort);

        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pid"></param>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="url"></param>
        /// <param name="actions"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        OperateResult EditMenu(int id, int pid, string name, string code, string url, string actions, int sort);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperateResult DeleteMenu(int id);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="sorts"></param>
        /// <returns></returns>
        OperateResult MenuChangeSort(int[] ids, int[] sorts);
    }

    
}
