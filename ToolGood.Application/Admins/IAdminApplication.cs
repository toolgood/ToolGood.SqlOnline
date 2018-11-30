using System.Collections.Generic;
using ToolGood.Datas;
using ToolGood.Infrastructure;
using ToolGood.ReadyGo3;

namespace ToolGood.Application
{
    public interface IAdminApplication
    {
        /// <summary>
        /// RSA信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        OperateResult GetRsaInfo(out string Exponent, out string Modulus);


        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        OperateResult<Dictionary<string, string>> RsaDecryptToDict(string data);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="sessionId"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        OperateResult<DbAdmin> Login(string user, string password, string sessionId, string ip);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="name"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        OperateResult ChangePassword(string name, string oldPassword, string newPassword);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        OperateResult ChangePassword(int id, string newPassword);


        /// <summary>
        /// 获取所有管理组
        /// </summary>
        /// <returns></returns>
        OperateResult<List<DbAdminGroup>> GetAdminGroupAll();

        /// <summary>
        /// 添加管理组
        /// </summary>
        /// <param name="name"></param>
        /// <param name="describe"></param>
        /// <param name="sort"></param>
        /// <param name="ans"></param>
        /// <returns></returns>
        OperateResult<int> AddAdminGroup(string name, string describe, int sort, string[] ans);


        /// <summary>
        /// 修改管理组
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="describe"></param>
        /// <param name="sort"></param>
        /// <param name="ans"></param>
        /// <returns></returns>
        OperateResult EditAdminGroup(int id, string name, string describe, int sort, string[] ans);

        /// <summary>
        /// 删除管理组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperateResult DeleteAdminGroup(int id);

        /// <summary>
        /// 获取管理组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperateResult<DbAdminGroup> GetAdminGroupById(int id);

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperateResult<List<DbAdminMenuPass>> GetAdminMenuPassListByGroupId(int id);

        /// <summary>
        /// 获取管理员
        /// </summary>
        /// <returns></returns>
        OperateResult<List<DbAdmin>> GetAdminAll();

        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="name"></param>
        /// <param name="trueName"></param>
        /// <param name="password"></param>
        /// <param name="adminGroupID"></param>
        /// <returns></returns>
        OperateResult AddAdmin(string name, string trueName, string password, int adminGroupID);

        /// <summary>
        /// 获取管理员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperateResult<DbAdmin> GetAdminById(int id);

        /// <summary>
        /// 编辑管理员
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trueName"></param>
        /// <param name="adminGroupID"></param>
        /// <returns></returns>
        OperateResult EditAdmin(int id, string trueName, int adminGroupID);

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperateResult DeleteAdmin(int id);

        /// <summary>
        /// 获取登录日志
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        OperateResult<Page<DbAdminLoginLog>> AdminLoginLogQuery(int page, int pageSize);

        /// <summary>
        /// 获取数据库权限
        /// </summary>
        /// <param name="adminGroupId"></param>
        /// <returns></returns>
        OperateResult<List<DbAdminDatabasePass>> GetDatabasePass(int adminGroupId);

        /// <summary>
        /// 设置数据库权限
        /// </summary>
        /// <param name="adminGroupId"></param>
        /// <param name="databases"></param>
        /// <returns></returns>
        OperateResult SetDatabasePass(int adminGroupId,int[] databases);

        /// <summary>
        /// 获取sql查询日志
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        OperateResult<Page<DbSqlQueryLog>> SqlQueryLogQuery(int page, int pageSize);

    }


}
