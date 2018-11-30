using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.Datas;
using ToolGood.Infrastructure;
using ToolGood.Repository;
using ToolGood.ReadyGo3;

namespace ToolGood.Application.Impl
{
    public class AdminApplication : IAdminApplication
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAdminGroupRepository _adminGroupRepository;
        private readonly IAdminLoginLogRepository _adminLoginLogRepository;
        private readonly IAdminMenuPassRepository _adminMenuPassRepository;
        private readonly IAdminDatabasePassRepository _adminDatabasePassRepository;
        private readonly ISqlQueryLogRepository _sqlQueryLogRepository;

        private static string _rsaKey;


        public AdminApplication(IAdminRepository adminRepository, IAdminGroupRepository adminGroupRepository, IAdminLoginLogRepository adminLoginLogRepository
            , IAdminMenuPassRepository adminMenuPassRepository, IAdminDatabasePassRepository adminDatabasePassRepository
            , ISqlQueryLogRepository sqlQueryLogRepository)
        {
            _adminRepository = adminRepository;
            _adminGroupRepository = adminGroupRepository;
            _adminLoginLogRepository = adminLoginLogRepository;
            _adminMenuPassRepository = adminMenuPassRepository;
            _adminDatabasePassRepository = adminDatabasePassRepository;
            _sqlQueryLogRepository = sqlQueryLogRepository;
        }

        public OperateResult GetRsaInfo(out string exponent, out string modulus)
        {
            try {
                if (_rsaKey == null) {
                    Rsa.CreateKey(out string keys);
                    _rsaKey = keys;
                }
                Rsa.GetParams(_rsaKey, out exponent, out modulus);
                return OperateResult.Success();
            } catch (Exception ex) {
                exponent = null;
                modulus = null;
                return OperateResult.Error(ex);
            }
        }

        public OperateResult<DbAdmin> Login(string user, string password, string sessionId, string ip)
        {
            return OperateResult.Execute(() => {
                var loginCount = _adminLoginLogRepository.Where(q => q.Name == user).Where(q => q.AddingTime > DateTime.Now.AddMinutes(-30)).SelectCount();
                if (loginCount > 100) {
                    _adminLoginLogRepository.Insert(new DbAdminLoginLog() { Name = user, Message = "登录超过上限，请稍后登录!", SessionID = sessionId, AddingTime = DateTime.Now, Ip = ip, IpAddress = ip });
                    return OperateResult.Failed<DbAdmin>("登录超过上限，请稍后登录！");
                }
                var admin = _adminRepository.Where(q => q.Name == user).FirstOrDefault();
                if (admin == null) {
                    _adminLoginLogRepository.Insert(new DbAdminLoginLog() { Name = user, Message = "用户名不存在!", SessionID = sessionId, AddingTime = DateTime.Now, Ip = ip });
                    return OperateResult.Failed<DbAdmin>("用户名或密码错误！");
                }
                if (admin.Password != CreatePassword(user, password)) {
                    _adminLoginLogRepository.Insert(new DbAdminLoginLog() { Name = user, Message = "密码错误!", SessionID = sessionId, AddingTime = DateTime.Now, Ip = ip });
                    return OperateResult.Failed<DbAdmin>("用户名或密码错误！");
                }
                _adminLoginLogRepository.Insert(new DbAdminLoginLog() { Name = user, Message = "登入成功!", SessionID = sessionId, AddingTime = DateTime.Now, Ip = ip, Success = true });

                return OperateResult.Success(admin);
            }, "AdminApplication-Login");
        }

        public OperateResult<Dictionary<string, string>> RsaDecryptToDict(string data)
        {
            return OperateResult.Execute(() => {
                try {
                    var text = Rsa.PrivateDecrypt(_rsaKey, data);
                    var dict = new Dictionary<string, string>();
                    var sp = text.Split('&');
                    foreach (var s in sp) {
                        var sp2 = s.Split('=');
                        if (sp2.Length > 1) {
                            dict[System.Web.HttpUtility.UrlDecode(sp2[0])] = System.Web.HttpUtility.UrlDecode(sp2[1]);
                        } else {
                            dict[System.Web.HttpUtility.UrlDecode(sp2[0])] = null;
                        }
                    }
                    return OperateResult.Success(dict);
                } catch (Exception) {
                    return OperateResult.Failed<Dictionary<string, string>>("数据出错了！");
                }
            }, "AdminApplication-RsaDecryptToDict");
        }

        public OperateResult ChangePassword(string name, string oldPassword, string newPassword)
        {
            return OperateResult.Execute(() => {
                var oldp = CreatePassword(name, oldPassword);
                var newp = CreatePassword(name, newPassword);

                var b = _adminRepository.ChangePassword(name, oldp, newp);
                if (b) {
                    return OperateResult.Success("密码修改成功");
                }
                return OperateResult.Failed("密码修改失败");
            }, "AdminApplication-ChangePassword");
        }
        public OperateResult ChangePassword(int id, string newPassword)
        {
            return OperateResult.Execute(() => {
                var admin = _adminRepository.SingleById(id);
                admin.Password = CreatePassword(admin.Name, newPassword);
                _adminRepository.Update(admin);
                return OperateResult.Success("密码修改成功");
            }, "AdminApplication-ChangePassword");
        }


        public OperateResult<List<DbAdminGroup>> GetAdminGroupAll()
        {
            return OperateResult.Execute(() => {
                var list = _adminGroupRepository.Where(q => q.IsDelete == false).OrderBy(q => q.Sort).Select();
                return OperateResult.Success(list);
            }, "AdminApplication-GetAdminGroupAll");

        }

        public OperateResult<int> AddAdminGroup(string name, string describe, int sort, string[] ans)
        {
            return OperateResult.Execute(() => {
                var group = new DbAdminGroup() {
                    Name = name,
                    Description = describe,
                    AddingTime = DateTime.Now,
                    Sort = sort,
                };
                _adminGroupRepository.Insert(group);

                if (ans != null) {
                    List<DbAdminMenuPass> list = new List<DbAdminMenuPass>();
                    for (int i = 0; i < ans.Length; i++) {
                        var an = ans[i].Split('.');
                        list.Add(new DbAdminMenuPass() {
                            AdminGroupId = group.Id,
                            MenuId = int.Parse(an[0]),
                            Code = an[1],
                            ActionName = an[2]
                        });
                    }
                    _adminMenuPassRepository.InsertList(list);
                }
                return OperateResult.Success(group.Id);
            }, "AdminApplication-AddAdminGroup");
        }

        public OperateResult EditAdminGroup(int id, string name, string describe, int sort, string[] ans)
        {
            return OperateResult.Execute(() => {
                _adminGroupRepository.Update(new UpAdminGroup { Name = name, Sort = sort, Description = describe }, new UpAdminGroup { Id = id });
                _adminMenuPassRepository.Delete(new UpAdminMenuPass { AdminGroupId = id });
                if (ans != null) {
                    List<DbAdminMenuPass> list = new List<DbAdminMenuPass>();
                    for (int i = 0; i < ans.Length; i++) {
                        var an = ans[i].Split('.');
                        list.Add(new DbAdminMenuPass() {
                            AdminGroupId = id,
                            MenuId = int.Parse(an[0]),
                            Code = an[1],
                            ActionName = an[2]
                        });
                    }
                    _adminMenuPassRepository.InsertList(list);
                }
                return OperateResult.Success();
            }, "AdminApplication-EditAdminGroup");
        }

        public OperateResult DeleteAdminGroup(int id)
        {
            return OperateResult.Execute(() => {
                _adminGroupRepository.Update(new UpAdminGroup { IsDelete = true }, new UpAdminGroup { Id = id });
                return OperateResult.Success();
            }, "AdminApplication-GetAdminGroupById");
        }


        public OperateResult<DbAdminGroup> GetAdminGroupById(int id)
        {
            return OperateResult.Execute(() => {
                var list = _adminGroupRepository.SingleById(id);
                return OperateResult.Success(list);
            }, "AdminApplication-GetAdminGroupById");
        }

        public OperateResult<List<DbAdminMenuPass>> GetAdminMenuPassListByGroupId(int id)
        {
            return OperateResult.Execute(() => {
                var list = _adminMenuPassRepository.Where(q => q.AdminGroupId == id).Select();
                return OperateResult.Success(list);
            }, "AdminApplication-GetAdminMenuPassListByGroupId");
        }

        public OperateResult<List<DbAdmin>> GetAdminAll()
        {
            return OperateResult.Execute(() => {
                var list = _adminRepository.Where(q => q.IsDelete == false).Select();
                return OperateResult.Success(list);
            }, "AdminApplication-GetAdminAll");
        }

        public OperateResult AddAdmin(string name, string trueName, string password, int adminGroupID)
        {
            return OperateResult.Execute(() => {
                var count = _adminRepository.Where(q => q.IsDelete == false).Where(q => q.Name == name).SelectCount();
                if (count > 0) {
                    return OperateResult.Failed("有相同用户名");
                }
                var admin = new DbAdmin() {
                    Name = name,
                    TrueName = trueName,
                    Password = CreatePassword(name, password),
                    GroupId = adminGroupID,
                    AddingTime = DateTime.Now,
                };
                _adminRepository.Insert(admin);
                return OperateResult.Success();
            }, "AdminApplication-AddAdmin");
        }

        public OperateResult<DbAdmin> GetAdminById(int id)
        {
            return OperateResult.Execute(() => {
                var list = _adminRepository.Where(q => q.IsDelete == false).Where(q => q.Id == id).Single();
                return OperateResult.Success(list);
            }, "AdminApplication-GetAdminById");
        }

        public OperateResult EditAdmin(int id, string trueName, int adminGroupID)
        {
            return OperateResult.Execute(() => {
                var admin = _adminRepository.SingleById(id);
                admin.TrueName = trueName;
                admin.GroupId = adminGroupID;
                _adminRepository.Update(admin);
            }, "AdminApplication-EditAdmin");
        }
        public OperateResult DeleteAdmin(int id)
        {
            return OperateResult.Execute(() => {
                _adminRepository.Update(new UpAdmin { IsDelete = true }, new UpAdmin { Id = id });
            }, "AdminApplication-DeleteAdmin");
        }

        public OperateResult<Page<DbAdminLoginLog>> AdminLoginLogQuery(int page, int pageSize)
        {
            return OperateResult.Execute(() => {
                var r = _adminLoginLogRepository.Where().OrderBy(q => q.Id, ReadyGo3.OrderType.Desc).Page(page, pageSize);
                return OperateResult.Success(r);
            }, "AdminApplication-DeleteAdmin");
        }

        /// <summary>
        /// 获取数据库权限
        /// </summary>
        /// <param name="adminGroupId"></param>
        /// <returns></returns>
        public OperateResult<List<DbAdminDatabasePass>> GetDatabasePass(int adminGroupId)
        {
            return OperateResult.Execute(() => {
                return _adminDatabasePassRepository.Where(q => q.AdminGroupId == adminGroupId).Select();
            }, "AdminApplication-GetDatabasePass");
        }

        /// <summary>
        /// 设置数据库权限
        /// </summary>
        /// <param name="adminGroupId"></param>
        /// <param name="databases"></param>
        /// <returns></returns>
        public OperateResult SetDatabasePass(int adminGroupId, int[] databases)
        {
            return OperateResult.Execute(() => {
                _adminDatabasePassRepository.Where(q => q.AdminGroupId == adminGroupId).Delete();
                List<DbAdminDatabasePass> passes = new List<DbAdminDatabasePass>();
                foreach (var item in databases) {
                    passes.Add(new DbAdminDatabasePass() {
                        AdminGroupId = adminGroupId,
                        DatabaseInfoId = item
                    });
                }
                _adminDatabasePassRepository.InsertList(passes);
            }, "AdminApplication-SetDatabasePass");
        }



        /// <summary>
        /// 获取sql查询日志
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public OperateResult<Page<DbSqlQueryLog>> SqlQueryLogQuery(int page, int pageSize)
        {
            return OperateResult.Execute(() => {
                return _sqlQueryLogRepository.Where(q => q.IsDelete == false)
                       .OrderBy(q => q.Id, OrderType.Desc)
                       .Page(page, pageSize);
            }, "AdminApplication-SqlQueryLogQuery");
        }



        private string CreatePassword(string user, string password, bool isMd5 = false)
        {
            if (isMd5) {
                return Hash.GetMd5String(user + "|ToolGood|" + password);
            }
            return Hash.GetMd5String(user + "|ToolGood|" + Hash.GetMd5String(password));
        }
    }
}
