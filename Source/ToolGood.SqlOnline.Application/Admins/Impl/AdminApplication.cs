/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ToolGood.Common;
using ToolGood.Common.Utils;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Datas;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon;

namespace ToolGood.SqlOnline.Application.Admins
{
    public class AdminApplication : ApplicationBase, IAdminApplication
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAdminLogApplication _adminLogApplication;
        public AdminApplication(IHttpContextAccessor httpContextAccessor, IAdminLogApplication adminLogApplication)
        {
            _httpContextAccessor = httpContextAccessor;
            _adminLogApplication = adminLogApplication;
        }
        private AdminApplication() { }
        public static AdminApplication Application { get { return new AdminApplication(); } }

        #region 管理员操作
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DbSysAdmin> AdminLogin(Req<AdminLoginDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.TName == null) { throw new ArgumentException("Data.UserName is null"); }
            if (request.Data.TPwd == null) { throw new ArgumentException("Data.Password is null"); }
            request.OperatorName = request.Data.TName;

            var ip = _httpContextAccessor.HttpContext.GetRealIP();

            var helper = GetWriteHelper();
            var loginCount = await helper.Count_Async<DbSysAdminLoginLog>("where (Name=@0 or Ip=@1) and AddingTime>@2", request.Data.TName, ip, DateTime.Now.AddMinutes(-30));
            if (loginCount > 10) {
                request.Message = "登录超过上限，请稍后登录!";
                await _adminLogApplication.WriteLoginLog("登录超过上限，请稍后登录!", request.Data.MachineCode, false, request);
                return null;
            }
            var admin = await helper.FirstOrDefault_Async<DbSysAdmin>("where Name=@0 and IsDelete=0", request.Data.TName);
            if (admin == null) {
                request.Message = "用户名不存在或密码错误!";
                await _adminLogApplication.WriteLoginLog($"用户名[{request.Data.TName}]不存在!密码:{request.Data.TPwd}", request.Data.MachineCode, false, request);
                return null;
            }

            #region 判断 是否为非本地IP，判断是否开启MAC地址验证，判断是否首次登录记录MAC地址
            if (_httpContextAccessor.HttpContext.IsLocalhost() == false) {
                var settingValue = await helper.FirstOrDefault_Async<DbSysSettingValue>("where Code='UseMachineCode'");
                if (settingValue != null && settingValue.Value.Trim() == "1") {
                    var count = await helper.FirstOrDefault_Async<int>(@"select Count(*) from SysAdmin_MachineCode where AdminId=@0 and MachineCode=@1", admin.Id, request.Data.MachineCode);
                    if (count == 0) {
                        settingValue = await helper.FirstOrDefault_Async<DbSysSettingValue>("where Code='FirstLoginUseMachineCode'");
                        if (settingValue == null || settingValue.Value.Trim() != "1") { request.Message = "Machine code未授权!"; return null; }

                        count = await helper.FirstOrDefault_Async<int>(@"select Count(*) from SysAdmin_MachineCode where AdminId=@0", admin.Id);
                        if (count > 0) { request.Message = "Machine code未授权!"; return null; }
                        await helper.Insert_Async(new DbSysAdmin_MachineCode() { AdminId = admin.Id, MachineCode = request.Data.MachineCode });
                    }
                }
            }
            #endregion

            if (admin.IsFrozen == 1) {
                request.Message = "用户名不存在或密码错误!";
                await _adminLogApplication.WriteLoginLog($"用户名[{request.Data.TName}]账号冻结！", request.Data.MachineCode, false, request);
                return null;
            }
            if (admin.Password != CreatePassword(request.Data.TName, admin.Salt, request.Data.TPwd)) {
                request.Message = "用户名不存在或密码错误!";
                await _adminLogApplication.WriteLoginLog($"用户名[{request.Data.TName}]密码错误!密码:{request.Data.TPwd}", request.Data.MachineCode, false, request);
                return null;
            }

            await _adminLogApplication.WriteLoginLog("登录成功", request.Data.MachineCode, true, request);
            return admin;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public async Task<bool> ChangePassword(Req<AdminChangePwdDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.AdminId == 0) { throw new ArgumentException("Data.AdminId is null"); }
            if (request.Data.OldPassword == null) { throw new ArgumentException("Data.Password is null"); }
            if (request.Data.NewPassword == null) { throw new ArgumentException("Data.NewPassword is null"); }
            if (request.Data.ConfirmPassword == null) { throw new ArgumentException("Data.RepeatPassword is null"); }

            const string title = "[修改密码]";
            if (request.Data.NewPassword != request.Data.ConfirmPassword) {
                request.Message = "两次密码不一致！";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.AdminId}]{ request.Message}", request);
                return false;
            }
            var helper = GetWriteHelper();
            var admin = await helper.FirstOrDefault_Async<DbSysAdmin>(new { Id = request.Data.AdminId, IsDelete = false });
            if (admin == null) {
                request.Message = "用户名或密码错误！";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.AdminId}]:{ request.Message}", request);
                return false;
            }
            var oldp = CreatePassword(admin.Name, admin.Salt, request.Data.OldPassword);
            var newp = CreatePassword(admin.Name, admin.Salt, request.Data.NewPassword);
            //if (oldp != admin.Password) {
            if (oldp != admin.ManagerPassword) {
                request.Message = "密码错误！";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.AdminId}]:{ request.Message}", request);
                return false;
            }
            await helper.Update_Async<DbSysAdmin>(new { Password = newp, ModifyTime = DateTime.Now, ModifyAdminId = request.OperatorId }, new { Id = request.Data.AdminId, IsDelete = false });
            request.Message = "修改成功";
            await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.AdminId}]:{ request.Message}", request);
            return true;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public async Task<bool> ChangePassword(Req<AdminChangePwdAllDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.AdminId == 0) { throw new ArgumentException("Data.AdminId is null"); }
            if (request.Data.OldPassword == null) { throw new ArgumentException("Data.Password is null"); }
            if (request.Data.NewPassword == null) { throw new ArgumentException("Data.NewPassword is null"); }
            if (request.Data.RepeatPassword == null) { throw new ArgumentException("Data.RepeatPassword is null"); }
            if (request.Data.NewManagerPassword == null) { throw new ArgumentException("Data.NewManagerPassword is null"); }
            if (request.Data.RepeatManagerPassword == null) { throw new ArgumentException("Data.RepeatManagerPassword is null"); }


            const string title = "[修改密码]";
            if (request.Data.NewPassword != request.Data.RepeatPassword) {
                request.Message = "两次登录密码不一致！";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.AdminId}]{ request.Message}", request);
                return false;
            }
            if (request.Data.NewManagerPassword != request.Data.RepeatManagerPassword) {
                request.Message = "两次管理密码不一致！";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.AdminId}]{ request.Message}", request);
                return false;
            }
            var helper = GetWriteHelper();
            var admin = await helper.FirstOrDefault_Async<DbSysAdmin>(new { Id = request.Data.AdminId, IsDelete = false });
            if (admin == null) {
                request.Message = "用户名或密码错误！";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.AdminId}]:{ request.Message}", request);
                return false;
            }
            var oldp = CreatePassword(admin.Name, admin.Salt, request.Data.OldPassword);
            if (oldp != admin.ManagerPassword) {
                request.Message = "管理密码错误！";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.AdminId}]:{ request.Message}", request);
                return false;
            }
            var newp = CreatePassword(admin.Name, admin.Salt, request.Data.NewPassword);
            var newp2 = CreatePassword(admin.Name, admin.Salt, request.Data.NewManagerPassword);
            await helper.Update_Async<DbSysAdmin>(new { Password = newp, ManagerPassword = newp2, ModifyTime = DateTime.Now, ModifyAdminId = request.OperatorId }, new { Id = request.Data.AdminId, IsDelete = false });
            await helper.Update_Async<DbSysAdmin>(new { IsFrozen = 0 }, new { Id = request.Data.AdminId, IsDelete = false, IsFrozen = 2 });
            request.Message = "修改成功";
            await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.AdminId}]:{ request.Message}", request);
            return true;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public async Task<bool> ChangeManagerPassword(Req<AdminChangePwdDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.AdminId == 0) { throw new ArgumentException("Data.AdminId is null"); }
            if (request.Data.OldPassword == null) { throw new ArgumentException("Data.Password is null"); }
            if (request.Data.NewPassword == null) { throw new ArgumentException("Data.NewPassword is null"); }
            if (request.Data.ConfirmPassword == null) { throw new ArgumentException("Data.RepeatPassword is null"); }

            const string title = "[修改密码]";
            if (request.Data.NewPassword != request.Data.ConfirmPassword) {
                request.Message = "两次密码不一致！";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.AdminId}]{ request.Message}", request);
                return false;
            }
            var helper = GetWriteHelper();
            var admin = await helper.FirstOrDefault_Async<DbSysAdmin>(new { Id = request.Data.AdminId, IsDelete = false });
            if (admin == null) {
                request.Message = "用户名或密码错误！";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.AdminId}]:{ request.Message}", request);
                return false;
            }
            var oldp = CreatePassword(admin.Name, admin.Salt, request.Data.OldPassword);
            var newp = CreatePassword(admin.Name, admin.Salt, request.Data.NewPassword);
            if (oldp != admin.ManagerPassword) {
                request.Message = "密码错误！";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.AdminId}]:{ request.Message}", request);
                return false;
            }
            await helper.Update_Async<DbSysAdmin>(new { ManagerPassword = newp, ModifyTime = DateTime.Now, ModifyAdminId = request.OperatorId }, new { Id = request.Data.AdminId, IsDelete = false });
            request.Message = "修改成功";
            await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.AdminId}]:{ request.Message}", request);
            return true;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ChangePassword(Req<AdminChangePwdForceDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.AdminId == 0) { throw new ArgumentException("Data.AdminId is null"); }
            if (request.Data.NewPassword == null) { throw new ArgumentException("Data.NewPassword is null"); }


            const string title = "[强制修改密码]";
            var helper = GetWriteHelper();
            var admin = await helper.FirstOrDefault_Async<DbSysAdmin>(new { Id = request.Data.AdminId, IsDelete = false });
            if (admin == null) {
                request.Message = "用户不存在！";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.AdminId}]:{ request.Message}", request);
                return false;
            } else {
                var newp = CreatePassword(admin.Name, admin.Salt, request.Data.NewPassword);
                await helper.Update_Async<DbSysAdmin>(new { Password = newp, ModifyTime = DateTime.Now, ModifyAdminId = request.OperatorId }, new { Id = request.Data.AdminId, IsDelete = false });
                request.Message = "修改成功";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.AdminId}]:{ request.Message}", request);
            }
            return true;
        }

        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddAdmin(Req<AdminAddDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.Name == null) { throw new ArgumentException("Data.Name is null"); }
            if (request.Data.Password == null) { throw new ArgumentException("Data.Password is null"); }

            const string title = "[添加管理员]";
            var helper = GetWriteHelper();
            var count = await helper.Where<DbSysAdmin>(q => q.IsDelete == false).Where(q => q.Name == request.Data.Name).SelectCount_Async();
            if (count > 0) {
                request.Message = "有相同用户名!";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.Name}]:{ request.Message}", request);
                return false;
            }
            var admin = new DbSysAdmin() {
                Name = request.Data.Name,
                TrueName = request.Data.TrueName,
                Salt = RandomUtil.GetRandomString(12),
                JobNo = request.Data.JobNo,
                Email = request.Data.Email,
                Phone = request.Data.Phone,

                AddingTime = DateTime.Now,
                AddingAdminId = request.OperatorId,
                ModifyTime = DateTime.Now,
                ModifyAdminId = request.OperatorId,
            };
            admin.Password = CreatePassword(request.Data.Name, admin.Salt, request.Data.Password);
            admin.ManagerPassword = CreatePassword(request.Data.Name, admin.Salt, request.Data.ManagerPassword);
            await helper.Insert_Async(admin);
            if (request.Data.Groups != null) {
                List<DbSysAdmin_Group> groups = new List<DbSysAdmin_Group>();
                foreach (var item in request.Data.Groups) {
                    groups.Add(new DbSysAdmin_Group() { AdminId = admin.Id, GroupId = item });
                }
                helper.InsertList(groups);
            }

            request.Message = "成功!";
            await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.Name}]:{ request.Message}", request);
            return true;
        }

        /// <summary>
        /// 修改管理员
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditAdmin(Req<AdminEditDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }

            const string title = "[修改管理员]";
            var helper = GetWriteHelper();
            var admin = await helper.Where<DbSysAdmin>(q => q.IsDelete == false).Where(q => q.Id == request.Data.Id).FirstOrDefault_Async();
            if (admin == null) {
                request.Message = "失败，不存在该用户";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.Id}]:{ request.Message}", request);
                return false;
            }
            admin.Email = request.Data.Email;
            admin.Phone = request.Data.Phone;
            admin.TrueName = request.Data.TrueName;
            admin.JobNo = request.Data.JobNo;
            admin.ModifyTime = DateTime.Now;
            admin.ModifyAdminId = request.OperatorId;
            admin.IsFrozen = request.Data.IsFrozen;
            await helper.Update_Async(admin);

            if (request.Data.EditGroups) {
                await helper.Delete_Async<DbSysAdmin_Group>(new { AdminId = admin.Id });
                if (request.Data.Groups != null) {
                    List<DbSysAdmin_Group> groups = new List<DbSysAdmin_Group>();
                    foreach (var item in request.Data.Groups) {
                        groups.Add(new DbSysAdmin_Group() { AdminId = admin.Id, GroupId = item });
                    }
                    helper.InsertList(groups);
                }
            }

            request.Message = "成功!";
            await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.Id}]:{ request.Message}", request);
            return true;
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAdmin(Req<AdminIdDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.Id == null) { throw new ArgumentException("Data.Id is null"); }

            const string title = "[删除管理员]";
            if (request.OperatorId == request.Data.Id) {
                request.Message = "不能自己删除自己！";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.Id}]:{ request.Message}", request);
                return false;
            }


            var helper = GetWriteHelper();
            var count = await helper.Update_Async<DbSysAdmin>(new { IsDelete = true, ModifyTime = DateTime.Now, ModifyAdminId = request.OperatorId }
                                                            , new { IsDelete = false, Id = request.Data.Id.Value });
            if (count == 0) {
                request.Message = "失败，不存在该用户";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.Id}]:{ request.Message}", request);
                return false;
            }
            await helper.Delete_Async<DbSysAdmin_Group>(new { AdminId = request.Data.Id });
            request.Message = "成功!";
            await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.Id}]:{ request.Message}", request);
            return true;
        }

        private string CreatePassword(string username, string salt, string password)
        {
            return DataUtil.CreatePassword(username, salt, password);
        }

        /// <summary>
        /// 核对密码，保证是本人操作
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<bool> CheckPassword(Req<AdminCheckPasswordDto> request)
        {
            return CheckPassword(request.Data.AdminId, request.Data.Password, request);
        }

        /// <summary>
        /// 核对密码，保证是本人操作
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> CheckPassword(int adminId, string password, IRequest request)
        {
            if (string.IsNullOrEmpty(password)) { return false; }

            const string title = "[核对密码]";
            var helper = GetWriteHelper();
            var admin = await helper.FirstOrDefault_Async<DbSysAdmin>(new { Id = adminId, IsDelete = false });
            if (admin == null) {
                request.Message = "用户错误！";
                await _adminLogApplication.WriteOperationLog($"{title}[{adminId}]:{ request.Message}", request);
                return false;
            }
            var oldp = CreatePassword(admin.Name, admin.Salt, password);
            if (oldp != admin.ManagerPassword) {
                request.Message = "密码错误！";
                await _adminLogApplication.WriteOperationLog($"{title}[{adminId}]:{ request.Message}", request);
                return false;
            }
            request.Message = "核对成功,进入管理员模式";
            await _adminLogApplication.WriteOperationLog($"{title}[{adminId}]:{ request.Message}", request);
            return true;
        }


        /// <summary>
        /// 获取管理员
        /// </summary>
        /// <returns></returns>
        public Task<List<DbSysAdmin>> GetAdminAll()
        {
            var helper = GetReadHelper();
            return helper.Where<DbSysAdmin>(q => q.IsDelete == false)
                    .OrderBy(q => q.Id, OrderType.Desc)
                    .Select_Async();
        }

        public Task<List<AdminSimpleDto>> GetAdminSimpleAll()
        {
            var helper = GetReadHelper();
            return helper.Where<DbSysAdmin>(q => q.IsDelete == false)
                    .Where(q => q.IsFrozen == 0)
                    .OrderBy(q => q.Id, OrderType.Desc)
                    .Select_Async<AdminSimpleDto>();
        }



        //public Task<List<DbSysAdmin>> GetAdminAllWithDelete()
        //{
        //    var helper = GetReadHelper();
        //    return helper.Where<DbSysAdmin>()
        //            .OrderBy(q => q.Id, OrderType.Desc)
        //            .Select_Async();
        //}

        /// <summary>
        /// 获取 页面信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="groupId"></param>
        /// <param name="isFrozen"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<Page<AdminDto>> GetAdminPage(Req<GetAdminListDto> request)
        //string name, string truename, string phone, int? groupId, int? isFrozen, string field, string order, int page = 1, int pageSize = 10)
        {
            var name = request.Data.Name;
            var truename = request.Data.TrueName;
            var phone = request.Data.Phone;
            var groupId = request.Data.GroupId;
            var isFrozen = request.Data.IsFrozen;
            var field = request.Data.Field;
            var order = request.Data.Order;
            var page = request.Data.PageIndex;
            var pageSize = request.Data.PageSize;


            var helper = GetReadHelper();
            var whereHelper = helper.Where<DbSysAdmin>();
            whereHelper.Where("t1.IsDelete=0");
            whereHelper.IfSet(name).Where("t1.Name like @@", "%" + name + "%");
            whereHelper.IfSet(truename).Where(q => q.TrueName.Contains(truename));
            whereHelper.IfSet(phone).Where(q => q.Phone.Contains(phone));
            whereHelper.IfPositiveInteger(groupId).Where($"Exits (select * FROM SysAdmin_Group t ON t.AdminId=Id AND t.GroupId={groupId})");
            whereHelper.IfNotNull(isFrozen).Where(q => q.IsFrozen == isFrozen.Value);
            whereHelper.IfSet(field).OrderBy(field, order);
            whereHelper.IfNotSet(field).OrderBy(q => q.Id, OrderType.Desc);
            var pages = await whereHelper.Page_Async<AdminDto>(page, pageSize, "t1.Id, t1.Name,t1.TrueName,t1.Phone,t1.Email,t1.IsFrozen,t1.AddingTime");

            var ids = pages.Items.Select(q => q.Id).ToList();

            if (ids.Count > 0) {
                var idsSQL = string.Join(",", ids);
                const string sql = @"SELECT t1.AdminId,t2.Name as GroupName
FROM SysAdmin_Group t1 
INNER JOIN SysAdminGroup t2 ON t1.GroupId=t2.Id
WHERE t2.IsDelete=0 AND t1.AdminId IN ({0})
";
                var groups = await helper.Select_Async<RelationshipDto>(string.Format(sql, idsSQL));
                foreach (var item in pages.Items) {
                    item.GroupNames = groups.Where(q => q.AdminId == item.Id).Select(q => q.GroupName).ToList();
                }
            }
            return pages;
        }

        /// <summary>
        /// 获取 管理员信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<DbSysAdmin> GetAdminById(int id)
        {
            var helper = GetReadHelper();
            var whereHelper = helper.Where<DbSysAdmin>(q => q.IsDelete == false);
            return whereHelper.Where(q => q.Id == id).FirstOrDefault_Async();
        }



        #endregion

        #region 管理员组操作

        /// <summary>
        /// 添加管理组
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddAdminGroup(Req<AdminGroupEditDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.Name == null) { throw new ArgumentException("Data.Name is null"); }
            request.Data.Menus.RemoveAll(q => q.Pass == false);

            const string title = "[添加管理组]";

            var group = new DbSysAdminGroup() {
                Name = request.Data.Name,
                Comment = request.Data.Comment,
                AddingTime = DateTime.Now,
                OrderNum = request.Data.OrderNum,
                AddingAdminId = request.OperatorId,
                ModifyTime = DateTime.Now,
                ModifyAdminId = request.OperatorId,
            };
            var helper = GetWriteHelper();
            await helper.Insert_Async(group);

            if (request.Data.Menus != null) {
                List<DbSysAdminGroup_Menu> list = new List<DbSysAdminGroup_Menu>();
                for (int i = 0; i < request.Data.Menus.Count; i++) {
                    var menu = request.Data.Menus[i];
                    list.Add(new DbSysAdminGroup_Menu() {
                        GroupId = group.Id,
                        MenuId = menu.MenuId,
                        MenuCode = menu.MenuCode,
                        ButtonId = menu.ButtonId,
                        ButtonCode = menu.ButtonCode
                    });
                }
                await helper.InsertList_Async(list);
            }
            request.Message = "成功!";
            await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.Name}]:{ request.Message}", request);
            return true;
        }

        /// <summary>
        /// 修改管理组
        /// </summary>
        /// <param name="request"></param>
        public async Task<bool> EditAdminGroup(Req<AdminGroupEditDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.Name == null) { throw new ArgumentException("Data.Name is null"); }
            request.Data.Menus.RemoveAll(q => q.Pass == false);

            const string title = "[修改管理组]";
            var helper = GetWriteHelper();


            await helper.Update_Async<DbSysAdminGroup>(new { Name = request.Data.Name, OrderNum = request.Data.OrderNum, Comment = request.Data.Comment, ModifyTime = DateTime.Now, ModifyAdminId = request.OperatorId }, new { ID = request.Data.Id });
            await helper.Delete_Async<DbSysAdminGroup_Menu>(new { GroupID = request.Data.Id });

            if (request.Data.Menus != null) {
                List<DbSysAdminGroup_Menu> list = new List<DbSysAdminGroup_Menu>();
                for (int i = 0; i < request.Data.Menus.Count; i++) {
                    var menu = request.Data.Menus[i];
                    list.Add(new DbSysAdminGroup_Menu() {
                        GroupId = request.Data.Id,
                        MenuId = menu.MenuId,
                        MenuCode = menu.MenuCode,
                        ButtonId = menu.ButtonId,
                        ButtonCode = menu.ButtonCode
                    });
                }
                await helper.InsertList_Async(list);
            }
            request.Message = "成功!";
            await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.Id}]:{ request.Message}", request);
            return true;
        }

        /// <summary>
        /// 删除管理组
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAdminGroup(Req<AdminIdDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.Id == null) { throw new ArgumentException("Data.Id is null"); }

            const string title = "[删除管理组]";

            var helper = GetWriteHelper();
            var count = await helper.Update_Async<DbSysAdminGroup>(new { IsDelete = true, ModifyTime = DateTime.Now, ModifyAdminId = request.OperatorId }
                                                            , new { Id = request.Data.Id, IsDelete = false, IsSystem = false });
            if (count > 0) {
                await helper.Delete_Async<DbSysAdmin_Group>(new { GroupId = request.Data.Id });
                await helper.Delete_Async<DbSysAdminGroup_Menu>(new { GroupId = request.Data.Id });

                request.Message = "成功!";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.Id}]:{ request.Message}", request);
                return true;
            } else {
                request.Message = "失败，角色为系统配置，或角色不存在。";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.Id}]:{ request.Message}", request);
                return false;
            }
        }

        /// <summary>
        /// 获取管理组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<DbSysAdminGroup> GetAdminGroupById(int id)
        {
            var helper = GetReadHelper();
            return helper.FirstOrDefault_Async<DbSysAdminGroup>(new { Id = id, IsDelete = false });
        }

        /// <summary>
        /// 获取所有管理组
        /// </summary>
        /// <returns></returns>
        public Task<List<DbSysAdminGroup>> GetAdminGroupAll()
        {
            var helper = GetReadHelper();
            return helper.Where<DbSysAdminGroup>(q => q.IsDelete == false)
                    .OrderBy(q => q.OrderNum)
                    .OrderBy(q => q.Name)
                    .Select_Async();
        }

        /// <summary>
        /// 获取所有管理组
        /// </summary>
        /// <returns></returns>
        public Task<Page<AdminGroupDto>> GetAdminGroupPage(Req<GetAdminGroupListDto> request)
        {
            var helper = GetReadHelper();
            return helper.Where<DbSysAdminGroup>().Where(q => q.IsDelete == false)
                             .IfSet(request.Data.Name).Where(q => q.Name.Contains(request.Data.Name))
                             .IfSet(request.Data.Field).OrderBy(request.Data.Field, request.Data.Order)
                             .IfNotSet(request.Data.Field).OrderBy(q => q.Id, OrderType.Desc)
                             .Page_Async<AdminGroupDto>(request.Data.PageIndex, request.Data.PageSize, "*,(select Count(*) FROM SysAdmin_Group t WHERE t.GroupId=Id)");
        }

        #endregion


        #region 菜单操作

        public Task<List<DbSysAdminMenu>> GetMenuAll()
        {
            var helper = GetReadHelper();
            return helper.Where<DbSysAdminMenu>(q => q.IsDelete == false)
                   .OrderBy(q => q.ParentIds)
                   .OrderBy(q => q.OrderNum)
                   .OrderBy(q => q.MenuName)
                   .Select_Async();
        }

        public async Task<bool> GetMenuCheck(string menuCode, string btnCode)
        {
            var helper = GetReadHelper();
            return await helper.Where<DbSysAdminMenuCheck>(q => q.IsDelete == false)
                        .Where(q => q.MenuCode == menuCode)
                        .Where(q => q.ButtonCode == btnCode)
                        .SelectCount_Async() > 0;
        }

        public Task<List<DbSysAdminMenuCheck>> GetMenuChecksAll()
        {
            var helper = GetReadHelper();
            return helper.Where<DbSysAdminMenuCheck>(q => q.IsDelete == false)
                    .Select_Async();
        }

        public async Task<bool> EditMenuMode(Req<AdminModeEditDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.Menus == null) { throw new ArgumentException("Data.Menus is null"); }
            request.Data.Menus.RemoveAll(q => q.Pass == false);

            var helper = GetWriteHelper();
            const string title = "[编辑管理员模式]";

            await helper.Update_Async<DbSysAdminMenuCheck>(new { IsDelete = true }, new { IsDelete = false });
            if (request.Data.Menus != null) {
                List<DbSysAdminMenuCheck> checks = new List<DbSysAdminMenuCheck>();
                foreach (var item in request.Data.Menus) {
                    checks.Add(new DbSysAdminMenuCheck {
                        AddingAdminId = request.OperatorId,
                        AddingTime = DateTime.Now,
                        MenuId = item.MenuId,
                        MenuCode = item.MenuCode,
                        ButtonCode = item.ButtonCode,
                    });
                }
                await helper.InsertList_Async(checks);
            }


            request.Message = "成功!";
            await _adminLogApplication.WriteOperationLog($"{title}:{ request.Message}", request);
            return true;
        }


        /// <summary>
        /// 是否有权限
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="menuCode"></param>
        /// <param name="btnCode"></param>
        /// <returns></returns>
        public async Task<bool> IsPass(int adminId, string menuCode, string btnCode)
        {
            var helper = GetReadHelper();
            var sql = @"SELECT COUNT(1) FROM SysAdmin t1
INNER JOIN SysAdmin_Group t2 ON t2.AdminId=t1.Id
INNER JOIN SysAdminGroup_Menu t3 ON t3.GroupId=t2.GroupId
WHERE t1.IsDelete=0 AND t1.IsFrozen=0 AND t1.Id=@0 AND t3.MenuCode=@1 AND t3.ButtonCode=@2
";
            return (await helper.Count_Async(sql, adminId, menuCode, btnCode)) > 0;
        }


        /// <summary>
        /// 获取 Admin SessionID
        /// </summary>
        /// <returns></returns>
        public Task<List<DbSysAdmin>> GetAdminCookieList()
        {
            var helper = GetReadHelper();
            var sql = @"select ID,LastBrowserPassword,LastSessionID from SysAdmin";
            return helper.Select_Async<DbSysAdmin>(sql);
        }

        /// <summary>
        /// 设置 SessionId
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public async Task<bool> SetAdminCookie(int adminId, string sessionId, string password)
        {
            var helper = GetWriteHelper();
            return (await helper.Update_Async<DbSysAdmin>(new { LastSessionID = sessionId, LastBrowserPassword = password }, new { Id = adminId })) > 0;
        }



        public Task<List<DbSysAdminMenu>> GetTopMenu(int adminId)
        {
            var helper = GetReadHelper();
            var sql = @"SELECT DISTINCT t4.* FROM SysAdmin t1
INNER JOIN SysAdmin_Group t2 ON t2.AdminId=t1.Id
INNER JOIN SysAdminGroup_Menu t3 ON t3.GroupId=t2.GroupId
INNER JOIN SysAdminMenu t4 ON t4.Id=t3.MenuId
WHERE t1.IsDelete=0 AND t1.IsFrozen=0 and t4.IsDelete=0 AND t3.ButtonCode='navbar' AND t1.Id=@0 AND t4.ParentId=0
ORDER BY t4.OrderNum ASC
";
            return helper.Select_Async<DbSysAdminMenu>(sql, adminId);
        }


        /// <summary>
        /// 获取侧边菜单
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<TreeAdminMenuDto> GetSidebarMenu(int adminId, int menuId, string menuIds)
        {
            var helper = GetReadHelper();
            var sql = @"SELECT DISTINCT t4.* FROM SysAdmin t1
INNER JOIN SysAdmin_Group t2 ON t2.AdminId=t1.Id
INNER JOIN SysAdminGroup_Menu t3 ON t3.GroupId=t2.GroupId
INNER JOIN SysAdminMenu t4 ON t4.Id=t3.MenuId
WHERE t1.IsDelete=0 AND t1.IsFrozen=0 and t4.IsDelete=0 AND t3.ButtonCode='navbar' AND t1.Id=@0 And t4.ParentIds like @1
ORDER BY t4.OrderNum ASC
";
            var menus = await helper.Select_Async<DbSysAdminMenu>(sql, adminId, menuIds + "%");
            menus.RemoveAll(q => q.Buttons.Split('|').Contains("navbar") == false);
            TreeAdminMenuDto dto = new TreeAdminMenuDto();
            dto.Id = menuId;
            BuildTreeAdminMenu(dto, menus);
            return dto;
        }
        private bool BuildTreeAdminMenu(TreeAdminMenuDto treeAdminMenu, List<DbSysAdminMenu> menus)
        {
            var tmenus = menus.Where(q => q.ParentId == treeAdminMenu.Id).ToList();
            foreach (var menu in tmenus) {
                TreeAdminMenuDto dto = new TreeAdminMenuDto() {
                    Id = menu.Id,
                    Name = menu.MenuName,
                    Url = menu.Url,
                };
                treeAdminMenu.Items.Add(dto);
                var isopen = BuildTreeAdminMenu(dto, menus);
                if (isopen) { dto.IsOpen = true; }
                if (dto.IsOpen) { treeAdminMenu.IsOpen = true; }
            }
            return treeAdminMenu.IsOpen;
        }
        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Task<DbSysAdminMenu> GetAdminMenu(string code)
        {
            var helper = GetReadHelper();
            return helper.FirstOrDefault_Async<DbSysAdminMenu>(new { MenuCode = code, IsDelete = 0 });
        }

        public Task<List<DbSysAdminMenuButton>> GetMenuButtonAll()
        {
            var helper = GetReadHelper();
            return helper.Where<DbSysAdminMenuButton>(q => q.IsDelete == false)
                .OrderBy(q => q.OrderNum)
                .OrderBy(q => q.ButtonName)
                .Select_Async();
        }

        #endregion

        #region 配置类别操作
        public async Task<bool> EditSetting(Req<SettingValueEditDto> request)
        {
            const string title = "[修改配置]";

            var helper = GetWriteHelper();

            var count = await helper.Update_Async<DbSysSettingValue>(new {
                SettingName = request.Data.SettingName,
                Value = request.Data.Value,
                Comment = request.Data.Comment,
                CategoryName = request.Data.CategoryName,
                ModifyTime = DateTime.Now,
                ModifyAdminId = request.OperatorId,
            }, new { IsDelete = false, Id = request.Data.Id });
            if (count == 0) {
                request.Message = "失败！";
                await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.Id}]:{ request.Message}", request);
                return false;
            }
            request.Message = "成功!";
            await _adminLogApplication.WriteOperationLog($"{title}[{request.Data.Id}]:{ request.Message}", request);
            return true;
        }

        public Task<Page<DbSysSettingValue>> GetSettingValuePage(Req<GetSettingListDto> request)
        // string categoryName, string search, int page, int pageSize)
        {
            var categoryName = request.Data.CategoryName;
            var search = request.Data.Search;
            var page = request.Data.PageIndex;
            var pageSize = request.Data.PageSize;

            var helper = GetReadHelper();
            return helper.Where<DbSysSettingValue>(q => q.IsDelete == false)
                    .IfSet(categoryName).Where(q => q.CategoryName == categoryName)
                    .IfSet(search).Where(q => q.Code.Contains(search) || q.SettingName.Contains(search) || q.Comment.Contains(search))
                    .OrderBy(q => q.CategoryName)
                    .OrderBy(q => q.Code)
                    .Page_Async(page, pageSize);
        }

        public Task<List<string>> GetSettingCategoryNameAll()
        {
            var helper = GetReadHelper();
            return helper.Select_Async<string>("select distinct CategoryName From SysSettingValue where IsDelete=0");
        }

        public Task<DbSysSettingValue> GetSettingValueById(int id)
        {
            var helper = GetReadHelper();
            return helper.FirstOrDefault_Async<DbSysSettingValue>("where Id=@0 and IsDelete=0", id);
            //return helper.Where<DbSysSettingValue>(q => q.IsDelete == false).Where(q => q.Id == id).FirstOrDefault_Async();
        }

        public Task<DbSysSettingValue> GetSettingValueByCode(string code)
        {
            var helper = GetReadHelper();
            return helper.FirstOrDefault_Async<DbSysSettingValue>("where Code=@0 and IsDelete=0", code);
        }

        public Task<List<DbSysSettingValue>> GetSettingValueByCategoryName(string name)
        {
            var helper = GetReadHelper();
            return helper.Select_Async<DbSysSettingValue>("where CategoryName=@0 and IsDelete=0", name);
        }



        #endregion

        #region 菜单-管理组-管理员 映射关系
        /// <summary>
        /// 更新菜单权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateMenuPassByGroupId(Req<RelationshipDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.GroupId == 0) { throw new ArgumentException("Data.GroupId is null"); }

            var helper = GetWriteHelper();
            await helper.Delete_Async<DbSysAdminGroup_Menu>(new { GroupId = request.Data.GroupId });

            if (request.Data.Items != null) {
                List<DbSysAdminGroup_Menu> group_Menus = new List<DbSysAdminGroup_Menu>();
                foreach (var item in request.Data.Items) {
                    DbSysAdminGroup_Menu menu = new DbSysAdminGroup_Menu() {
                        MenuId = item.MenuId,
                        MenuCode = item.MenuCode,
                        ButtonCode = item.ButtonCode,
                        GroupId = request.Data.GroupId,
                        ButtonId = item.ButtonId
                    };
                    group_Menus.Add(menu);
                }
                await helper.InsertList_Async(group_Menus);
            }


            const string title = "[更新菜单权限依据管理组ID]";
            request.Message = "成功!";
            await _adminLogApplication.WriteOperationLog($"{title}:{ request.Message}", request);
            return true;
        }
        /// <summary>
        /// 更新组权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateGroupPassByMenuId(Req<RelationshipDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.MenuId == 0) { throw new ArgumentException("Data.MenuId is null"); }
            if (request.Data.Items == null) { throw new ArgumentException("Data.Items is null"); }

            var helper = GetWriteHelper();
            await helper.Delete_Async<DbSysAdminGroup_Menu>(new { MenuId = request.Data.MenuId });

            if (request.Data.Items != null) {
                List<DbSysAdminGroup_Menu> group_Menus = new List<DbSysAdminGroup_Menu>();
                foreach (var item in request.Data.Items) {
                    DbSysAdminGroup_Menu menu = new DbSysAdminGroup_Menu() {
                        MenuId = request.Data.MenuId,
                        MenuCode = item.MenuCode,
                        ButtonCode = item.ButtonCode,
                        GroupId = item.GroupId,
                        ButtonId = item.ButtonId
                    };
                    group_Menus.Add(menu);
                }
                await helper.InsertList_Async(group_Menus);
            }

            const string title = "[更新管理组菜单权限依据菜单ID]";
            request.Message = "成功!";
            await _adminLogApplication.WriteOperationLog($"{title}:{ request.Message}", request);
            return true;
        }
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateGroupPassByAdminId(Req<RelationshipDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.AdminId == 0) { throw new ArgumentException("Data.MenuId is null"); }
            if (request.Data.Items == null) { throw new ArgumentException("Data.Items is null"); }

            var helper = GetWriteHelper();
            await helper.Delete_Async<DbSysAdmin_Group>(new { AdminId = request.Data.AdminId });

            if (request.Data.Items != null) {
                List<DbSysAdmin_Group> group_Menus = new List<DbSysAdmin_Group>();
                foreach (var item in request.Data.Items) {
                    DbSysAdmin_Group menu = new DbSysAdmin_Group() {
                        AdminId = request.Data.AdminId,
                        GroupId = item.GroupId,
                    };
                    group_Menus.Add(menu);
                }
                await helper.InsertList_Async(group_Menus);
            }


            const string title = "[更新管理组关系依据管理员ID]";
            request.Message = "成功!";
            await _adminLogApplication.WriteOperationLog($"{title}:{ request.Message}", request);
            return true;
        }
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAdminPassByGroupId(Req<RelationshipDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.GroupId == 0) { throw new ArgumentException("Data.GroupId is null"); }
            if (request.Data.Items == null) { throw new ArgumentException("Data.Items is null"); }

            var helper = GetWriteHelper();
            await helper.Delete_Async<DbSysAdmin_Group>(new { GroupId = request.Data.GroupId });

            if (request.Data.Items != null) {
                List<DbSysAdmin_Group> group_Menus = new List<DbSysAdmin_Group>();
                foreach (var item in request.Data.Items) {
                    DbSysAdmin_Group menu = new DbSysAdmin_Group() {
                        AdminId = item.AdminId,
                        GroupId = request.Data.GroupId,
                    };
                    group_Menus.Add(menu);
                }
                await helper.InsertList_Async(group_Menus);
            }

            const string title = "[更新管理员关系依据管理组ID]";
            request.Message = "成功!";
            await _adminLogApplication.WriteOperationLog($"{title}:{ request.Message}", request);
            return true;
        }



        /// <summary>
        /// 获取菜单 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<List<RelationshipDto>> GetMenusByGroupId(int id)
        {
            var helper = GetReadHelper();
            var sql = @"select t1.GroupId,t1.MenuId,t1.ButtonId,t1.ButtonCode,t2.MenuName,t2.MenuCode,t3.ButtonName
FROM SysAdminGroup_Menu t1
INNER JOIN SysAdminMenu t2 ON t1.MenuId=t2.Id
INNER JOIN SysAdminMenuButton t3 ON t1.ButtonId=t3.Id
WHERE t2.IsDelete=0 AND t3.IsDelete=0 AND t1.GroupId=@0
";
            return helper.Select_Async<RelationshipDto>(sql, id);
            //List<RelationshipDto> outList = new List<RelationshipDto>();
            //var ids = list.Select(q => q.GroupId).Distinct().ToList();
            //foreach (var idItem in ids) {
            //    outList.Add(new RelationshipDto() {
            //        GroupId = idItem,
            //        Items = list.Where(q => q.GroupId == idItem).ToList()
            //    });
            //}
            //return outList;
        }
        /// <summary>
        /// 获取管理组
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<List<RelationshipDto>> GetGroupsByMenuId(int id)
        {
            var helper = GetReadHelper();
            var sql = @"select t1.GroupId,t1.MenuId,t1.ButtonId,t1.ButtonCode,t2.MenuName,t2.MenuCode,t3.ButtonName
FROM SysAdminGroup_Menu t1
INNER JOIN SysAdminMenu t2 ON t1.MenuId=t2.Id
INNER JOIN SysAdminMenuButton t3 ON t1.ButtonId=t3.Id
WHERE t2.IsDelete=0 AND t3.IsDelete=0 AND t1.MenuId=@0
";
            return helper.Select_Async<RelationshipDto>(sql, id);
            //List<RelationshipDto> outList = new List<RelationshipDto>();
            //var ids = list.Select(q => q.MenuId).Distinct().ToList();
            //foreach (var idItem in ids) {
            //    outList.Add(new RelationshipDto() {
            //        MenuId = idItem,
            //        Items = list.Where(q => q.MenuId == idItem).ToList()
            //    });
            //}
            //return outList;
        }

        /// <summary>
        /// 获取管理组
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<List<RelationshipDto>> GetGroupsByAdminId(int id)
        {
            var helper = GetReadHelper();
            var sql = @"SELECT t1.AdminId,t3.TrueName AdminName,t1.GroupId,t2.Name GroupName
FROM SysAdmin_Group t1
INNER JOIN SysAdminGroup t2 ON t2.Id=t1.GroupId
INNER JOIN SysAdmin t3 ON t3.Id=t1.AdminId
WHERE t2.IsDelete=0 AND t3.IsDelete=0 AND t1.AdminId=@0
";
            return helper.Select_Async<RelationshipDto>(sql, id);

        }

        /// <summary>
        /// 获取管理员
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<List<RelationshipDto>> GetAdminsByGroupId(int id)
        {
            var helper = GetReadHelper();
            var sql = @"SELECT t1.AdminId,t3.TrueName AdminName,t1.GroupId,t2.Name GroupName
FROM SysAdmin_Group t1
INNER JOIN SysAdminGroup t2 ON t2.Id=t1.GroupId
INNER JOIN SysAdmin t3 ON t3.Id=t1.AdminId
WHERE t2.IsDelete=0 AND t3.IsDelete=0 AND t1.GroupId=@0
";
            return helper.Select_Async<RelationshipDto>(sql, id);
        }

        public Task<List<RelationshipDto>> GetMenuCountByGroup()
        {
            var helper = GetReadHelper();
            var sql = @"select t1.GroupId,COUNT(*) count,COUNT(distinct t1.MenuId) count2
FROM SysAdminGroup_Menu t1
INNER JOIN SysAdminMenu t2 ON t1.MenuId=t2.Id
INNER JOIN SysAdminMenuButton t3 ON t1.ButtonId=t3.Id
WHERE t2.IsDelete=0 AND t3.IsDelete=0 
GROUP BY t1.GroupId
";
            return helper.Select_Async<RelationshipDto>(sql);
        }

        public Task<List<RelationshipDto>> GetGroupCountByMenu()
        {
            var helper = GetReadHelper();
            var sql = @"select t1.MenuId,COUNT(*) count,COUNT(distinct t1.GroupId) count2
FROM SysAdminGroup_Menu t1
INNER JOIN SysAdminMenu t2 ON t1.MenuId=t2.Id
INNER JOIN SysAdminMenuButton t3 ON t1.ButtonId=t3.Id
WHERE t2.IsDelete=0 AND t3.IsDelete=0 
GROUP BY t1.MenuId 
";
            return helper.Select_Async<RelationshipDto>(sql);
        }

        public Task<List<RelationshipDto>> GetGroupCountByAdmin()
        {
            var helper = GetReadHelper();
            var sql = @"SELECT t1.AdminId,COUNT(*) count
FROM SysAdmin_Group t1
INNER JOIN SysAdminGroup t2 ON t2.Id=t1.GroupId
INNER JOIN SysAdmin t3 ON t3.Id=t1.AdminId
WHERE t2.IsDelete=0 AND t3.IsDelete=0 
GROUP BY t1.AdminId
";
            return helper.Select_Async<RelationshipDto>(sql);
        }

        public Task<List<RelationshipDto>> GetAdminCountByGroup()
        {
            var helper = GetReadHelper();
            var sql = @"SELECT t1.GroupId,COUNT(*) count
FROM SysAdmin_Group t1
INNER JOIN SysAdminGroup t2 ON t2.Id=t1.GroupId
INNER JOIN SysAdmin t3 ON t3.Id=t1.AdminId
WHERE t2.IsDelete=0 AND t3.IsDelete=0 
GROUP BY t1.GroupId
";
            return helper.Select_Async<RelationshipDto>(sql);
        }




        /// <summary>
        /// 获取菜单 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IAdminButtonPass> GetButtonPassByAdminId(int id, string menuCode)
        {
            var helper = GetReadHelper();
            var sql = @"SELECT t1.AdminId,t3.TrueName AdminName,t1.GroupId,t2.Name GroupName,t4.MenuId,t4.MenuCode,t5.MenuName,t4.ButtonId,t4.ButtonCode,t6.ButtonName
FROM SysAdmin_Group t1
INNER JOIN SysAdminGroup t2 ON t2.Id=t1.GroupId
INNER JOIN SysAdmin t3 ON t3.Id=t1.AdminId
INNER JOIN SysAdminGroup_Menu t4 ON t4.GroupId=t1.GroupId
INNER JOIN SysAdminMenu t5 ON t4.MenuId=t5.Id
INNER JOIN SysAdminMenuButton t6 ON t4.ButtonId=t6.Id
WHERE t2.IsDelete=0 AND t3.IsDelete=0 AND t5.IsDelete=0 AND t6.IsDelete=0 AND t3.IsFrozen=0 AND t1.AdminId=@0 AND t5.MenuCode=@1
";
            var list = await helper.Select_Async<RelationshipDto>(sql, id, menuCode);
            RelationshipDto relationship = new RelationshipDto();
            relationship.Items = list;
            relationship.AdminId = id;
            return relationship;
        }

        #endregion


        public async Task<bool> EditEmailSend(Req<EmailSendDto> request)
        {
            var helper = GetWriteHelper();
            const string sql = @"update SysSettingValue set Value=@0 where Code='EmailSendHost';
update SysSettingValue set Value=@1 where Code='EmailSendPort';
update SysSettingValue set Value=@2 where Code='EmailSendSSL';
update SysSettingValue set Value=@3 where Code='EmailSendUser';
update SysSettingValue set Value=@4 where Code='EmailSendPassword';
update SysSettingValue set Value=@5 where Code='EmailSendCount';
update SysSettingValue set Value=@6 where Code='EmailSendInterval';";

            var count = await helper.Execute_Async(sql
                    , request.Data.EmailSendHost
                    , request.Data.EmailSendPort
                    , request.Data.EmailSendSSL
                    , request.Data.EmailSendUser
                    , request.Data.EmailSendPassword
                    , request.Data.EmailSendCount
                    , request.Data.EmailSendInterval
                    );
            return count > 0;
        }

        public async Task<EmailSendDto> GetEmailSendInfo()
        {
            var helper = GetReadHelper();
            const string sql = "select Code,Value from SysSettingValue where Code in ('EmailSendHost','EmailSendPort','EmailSendSSL','EmailSendUser','EmailSendPassword','EmailSendCount','EmailSendInterval')";

            var list = await helper.Select_Async<DbSysSettingValue>(sql);
            EmailSendDto dto = new EmailSendDto();
            dto.EmailSendHost = list.Where(q => q.Code == "EmailSendHost").FirstOrDefault().SelectNull(q => q.Value);
            dto.EmailSendPort = list.Where(q => q.Code == "EmailSendPort").FirstOrDefault().SelectNull(q => q.Value);
            dto.EmailSendSSL = list.Where(q => q.Code == "EmailSendSSL").FirstOrDefault().SelectNull(q => q.Value).ToSafeInt32(0);
            dto.EmailSendUser = list.Where(q => q.Code == "EmailSendUser").FirstOrDefault().SelectNull(q => q.Value);
            dto.EmailSendPassword = list.Where(q => q.Code == "EmailSendPassword").FirstOrDefault().SelectNull(q => q.Value);
            dto.EmailSendCount = list.Where(q => q.Code == "EmailSendCount").FirstOrDefault().SelectNull(q => q.Value).ToSafeInt32(0);
            dto.EmailSendInterval = list.Where(q => q.Code == "EmailSendInterval").FirstOrDefault().SelectNull(q => q.Value).ToSafeInt32(0);

            return dto;
        }


        public async Task<bool> AddMachineCode(string code)
        {
            var helper = GetWriteHelper();
            const string sql = "INSERT INTO SysMachineCode (MachineCode,AddingTime) SELECT @0,@1 WHERE NOT EXISTS (SELECT Id FROM SysMachineCode where MachineCode=@0);";
            var count = await helper.Execute_Async(sql, code, DateTime.Now);
            return count > 0;
        }

        public async Task<bool> AddAdminMachineCode(Req<AdminMachineCodeEditDto> request)
        {
            var helper = GetWriteHelper();

            await helper.Insert_Async(new DbSysAdmin_MachineCode() {
                AdminId = request.Data.AdminId,
                MachineCode = request.Data.MachineCode
            });
            return true;
        }

        public async Task<bool> DeleteAdminMachineCode(Req<AdminIdDto> request)
        {
            var helper = GetWriteHelper();
            await helper.DeleteById_Async<DbSysAdmin_MachineCode>(request.Data.Id);
            return true;
        }

        public Task<Page<AdminMachineCodeDto>> GetAdminMachineCodePage(Req<AdminMachineCodeSearchDto> request)
        {
            const string selectSql = "select amc.Id,mc.MachineCode,a.Id AdminId,a.Name AdminName,a.JobNo AdminJobNo,a.TrueName AdminTrueName,a.Phone AdminPhone";
            const string tableSql = "from SysMachineCode mc left join SysAdmin_MachineCode amc on mc.MachineCode=amc.MachineCode left join SysAdmin a on a.Id=amc.AdminId";
            string whereSql = "where 1=1 ";

            var helper = GetWriteHelper();
            SqlParameterCollection sqlParameters = new SqlParameterCollection();

            if (string.IsNullOrEmpty(request.Data.MachineCode) == false) {
                whereSql += " and mc.MachineCode=@MachineCode ";
                sqlParameters.Add("MachineCode", request.Data.MachineCode);
            }
            if (string.IsNullOrEmpty(request.Data.AdminName) == false) {
                whereSql += " and a.Name like @AdminName ";
                sqlParameters.Add("AdminName", "%" + request.Data.AdminName + "%");
            }
            if (string.IsNullOrEmpty(request.Data.AdminJobNo) == false) {
                whereSql += " and a.JobNo like @AdminJobNo ";
                sqlParameters.Add("AdminJobNo", "%" + request.Data.AdminJobNo + "%");
            }
            if (string.IsNullOrEmpty(request.Data.AdminTrueName) == false) {
                whereSql += " and a.TrueName like @AdminTrueName ";
                sqlParameters.Add("AdminTrueName", "%" + request.Data.AdminTrueName + "%");
            }
            var orderSql = " mc.Id asc ";
            if (string.IsNullOrWhiteSpace(request.Data.Field) == false) {
                orderSql = $" {request.Data.Field} {request.Data.Order}";
            }

            return helper.SQL_Page_Async<AdminMachineCodeDto>(request.Data.PageIndex, request.Data.PageSize, selectSql, tableSql
                , orderSql, whereSql, sqlParameters);
        }



    }

}
