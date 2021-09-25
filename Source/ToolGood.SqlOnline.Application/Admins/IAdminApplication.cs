/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Datas;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon;

namespace ToolGood.SqlOnline.Application
{
    public interface IAdminApplication
    {
        #region 管理员操作

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<DbSysAdmin> AdminLogin(Req<AdminLoginDto> request);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        Task<bool> ChangePassword(Req<AdminChangePwdDto> request);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        Task<bool> ChangePassword(Req<AdminChangePwdAllDto> request);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        Task<bool> ChangeManagerPassword(Req<AdminChangePwdDto> request);

        /// <summary>
        /// 强制修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> ChangePassword(Req<AdminChangePwdForceDto> request);
        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddAdmin(Req<AdminAddDto> request);

        /// <summary>
        /// 修改管理员
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditAdmin(Req<AdminEditDto> request);

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteAdmin(Req<AdminIdDto> request);

        /// <summary>
        /// 核对密码，保证是本人操作
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> CheckPassword(Req<AdminCheckPasswordDto> request);
        Task<bool> CheckPassword(int adminId, string password, IRequest request);

        /// <summary>
        /// 获取 页面信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="groupId"></param>
        /// <param name="isFrozen"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<Page<AdminDto>> GetAdminPage(Req<GetAdminListDto> request);

        /// <summary>
        /// 获取管理员
        /// </summary>
        /// <returns></returns>
        Task<List<DbSysAdmin>> GetAdminAll();

        Task<List<AdminSimpleDto>> GetAdminSimpleAll();


        /// <summary>
        /// 获取 管理员信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DbSysAdmin> GetAdminById(int id);


        /// <summary>
        /// 是否有权限
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="menuCode"></param>
        /// <param name="btnCode"></param>
        /// <returns></returns>
        Task<bool> IsPass(int adminId, string menuCode, string btnCode);

        /// <summary>
        /// 获取 Admin SessionID
        /// </summary>
        /// <returns></returns>
        Task<List<DbSysAdmin>> GetAdminCookieList();

        /// <summary>
        /// 设置 SessionId
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        Task<bool> SetAdminCookie(int adminId, string sessionId, string password);


        #endregion

        #region 管理员组操作

        /// <summary>
        /// 添加管理组
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddAdminGroup(Req<AdminGroupEditDto> request);


        /// <summary>
        /// 修改管理组
        /// </summary>
        /// <param name="request"></param>
        Task<bool> EditAdminGroup(Req<AdminGroupEditDto> request);
        /// <summary>
        /// 删除管理组
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteAdminGroup(Req<AdminIdDto> request);


        /// <summary>
        /// 获取所有管理组
        /// </summary>
        /// <returns></returns>
        Task<List<DbSysAdminGroup>> GetAdminGroupAll();


        /// <summary>
        /// 获取所有管理组
        /// </summary>
        /// <returns></returns>
        Task<Page<AdminGroupDto>> GetAdminGroupPage(Req<GetAdminGroupListDto> request);

        /// <summary>
        /// 获取管理组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DbSysAdminGroup> GetAdminGroupById(int id);
        #endregion


        #region  菜单操作

        Task<List<DbSysAdminMenu>> GetMenuAll();

        Task<bool> GetMenuCheck(string menuCode, string btnCode);

        Task<List<DbSysAdminMenuCheck>> GetMenuChecksAll();

        Task<bool> EditMenuMode(Req<AdminModeEditDto> request);


        Task<List<DbSysAdminMenu>> GetTopMenu(int adminId);

        /// <summary>
        /// 获取侧边菜单
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        Task<TreeAdminMenuDto> GetSidebarMenu(int adminId, int menuId, string menuIds);

        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<DbSysAdminMenu> GetAdminMenu(string code);

        Task<List<DbSysAdminMenuButton>> GetMenuButtonAll();


        #endregion


        #region 配置操作

        Task<bool> EditSetting(Req<SettingValueEditDto> request);

        Task<Page<DbSysSettingValue>> GetSettingValuePage(Req<GetSettingListDto> request);

        Task<List<string>> GetSettingCategoryNameAll();

        Task<DbSysSettingValue> GetSettingValueById(int id);

        Task<DbSysSettingValue> GetSettingValueByCode(string code);

        Task<List<DbSysSettingValue>> GetSettingValueByCategoryName(string name);

        #endregion

        #region 菜单-管理组-管理员 映射关系
        /// <summary>
        /// 更新菜单权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateMenuPassByGroupId(Req<RelationshipDto> request);
        /// <summary>
        /// 更新组权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateGroupPassByMenuId(Req<RelationshipDto> request);
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateGroupPassByAdminId(Req<RelationshipDto> request);
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateAdminPassByGroupId(Req<RelationshipDto> request);



        /// <summary>
        /// 获取菜单 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<RelationshipDto>> GetMenusByGroupId(int id);
        /// <summary>
        /// 获取管理组
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<RelationshipDto>> GetGroupsByMenuId(int id);

        /// <summary>
        /// 获取管理组
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<RelationshipDto>> GetGroupsByAdminId(int id);

        /// <summary>
        /// 获取管理员
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<RelationshipDto>> GetAdminsByGroupId(int id);


        Task<List<RelationshipDto>> GetMenuCountByGroup();

        Task<List<RelationshipDto>> GetGroupCountByMenu();

        Task<List<RelationshipDto>> GetGroupCountByAdmin();

        Task<List<RelationshipDto>> GetAdminCountByGroup();




        /// <summary>
        /// 获取菜单 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IAdminButtonPass> GetButtonPassByAdminId(int id, string menuCode);
        #endregion



        Task<bool> EditEmailSend(Req<EmailSendDto> request);

        Task<EmailSendDto> GetEmailSendInfo();



        Task<bool> AddMachineCode(string mac);

        Task<bool> AddAdminMachineCode(Req<AdminMachineCodeEditDto> request);

        Task<bool> DeleteAdminMachineCode(Req<AdminIdDto> request);

        Task<Page<AdminMachineCodeDto>> GetAdminMachineCodePage(Req<AdminMachineCodeSearchDto> request);



    }

}
