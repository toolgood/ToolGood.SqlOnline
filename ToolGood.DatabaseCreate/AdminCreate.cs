using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.Datas;
using ToolGood.Infrastructure;
using ToolGood.ReadyGo3;

namespace ToolGood.DatabaseCreate
{
    public class AdminCreate
    {

        #region Admin
        public static void CreateAdminTable(SqlHelper helper)
        {
            var table = helper._TableHelper;
            //admin
            table.CreateTable(typeof(DbAdmin));
            table.CreateTable(typeof(DbAdminGroup));
            table.CreateTable(typeof(DbAdminLoginLog));
            table.CreateTable(typeof(DbAdminMenu));
            table.CreateTable(typeof(DbAdminMenuPass));
            table.CreateTable(typeof(DbSetting));
            

            table.CreateTable(typeof(DbAdminDatabasePass));
            table.CreateTable(typeof(DbDatabaseInfo));
            table.CreateTable(typeof(DbDatabaseInfoHistory));
            table.CreateTable(typeof(DbSqlQueryLog));
        }

        public static void CreateAdminData(SqlHelper helper)
        {
            helper._Config.Insert_DateTime_Default_Now = true;
            helper._Config.Insert_String_Default_NotNull = true;

            #region Admin
            helper.Insert(new DbAdmin() {
                Name = "admin",
                Password = CreatePassword("admin", "12345"),
                TrueName = "超级管理员",
                GroupId = 1,

            });
            #endregion

            #region AdminGroup
            helper.Insert(new DbAdminGroup() {
                Name = "超级管理员",
                Sort = 1,
            });
            #endregion

            #region AdminMenu
            {

                var menuIndex = 1000;
                {
                    var adminDesktop = new DbAdminMenu() { Name = "管理面板", Code = "AdminDesktop", Url = "", Actions = "navbar", ParentId = 0, Sort = menuIndex++, };
                    helper.Insert(adminDesktop);
                    var index = 1;
                    var desktop = new DbAdminMenu() { Name = "管理列表", Code = "AdminDesktop-1", Url = "", Actions = "navbar", ParentId = adminDesktop.Id, Sort = index++, };
                    helper.Insert(desktop);
                    var pid = desktop.Id;
                    helper.Insert(new DbAdminMenu() { Name = "数据库管理", Code = "Admin-Database", Url = "/Admin/Database/index", Actions = "navbar|show|add|edit|delete", ParentId = pid, Sort = index++, });
                    helper.Insert(new DbAdminMenu() { Name = "菜单管理", Code = "Admin-Menu", Url = "/Admin/Menu/index", Actions = "navbar|show|add|edit|delete", ParentId = pid, Sort = index++, });
                    helper.Insert(new DbAdminMenu() { Name = "角色管理", Code = "Admin-Group", Url = "/Admin/Group/index", Actions = "navbar|show|add|edit|delete", ParentId = pid, Sort = index++, });
                    helper.Insert(new DbAdminMenu() { Name = "管理员列表", Code = "Admin-Member", Url = "/Admin/Member/index", Actions = "navbar|show|add|edit|delete|ChangePassword", ParentId = pid, Sort = index++, });

                    var log = new DbAdminMenu() { Name = "日志管理", Code = "AdminDesktop-2", Url = "", Actions = "navbar", ParentId = adminDesktop.Id, Sort = index++, };
                    helper.Insert(log);
                    pid = log.Id;
                    helper.Insert(new DbAdminMenu() { Name = "登录日志", Code = "Log-AdminLoginLog", Url = "/Admin/Log/AdminLoginLog", Actions = "navbar|show|add|edit|delete", ParentId = pid, Sort = index++, });

                    helper.Insert(new DbAdminMenu() { Name = "SQL查询日志", Code = "Log-SqlQueryLog", Url = "/Admin/Log/SqlQueryLog", Actions = "navbar|show|add|edit|delete", ParentId = pid, Sort = index++, });


                    desktop = new DbAdminMenu() { Name = "帮助反馈", Code = "AdminDesktop-3", Url = "", Actions = "navbar", ParentId = adminDesktop.Id, Sort = index++, };
                    helper.Insert(desktop);
                    pid = desktop.Id;

                    helper.Insert(new DbAdminMenu() { Name = "关于", Code = "Help-About", Url = "/Admin/Help/About", Actions = "navbar|show|add|edit|delete", ParentId = pid, Sort = index++, });
                    helper.Insert(new DbAdminMenu() { Name = "建议反馈", Code = "Help-Feedback", Url = "/Admin/Help/Feedback", Actions = "navbar|show|add|edit|delete", ParentId = pid, Sort = index++, });
                    helper.Insert(new DbAdminMenu() { Name = "帮助文档", Code = "Help-Doc", Url = "/Admin/Help/Doc", Actions = "navbar|show|add|edit|delete", ParentId = pid, Sort = index++, });
                }

            }

            #endregion

            helper.Insert(new DbSetting() {
                AddingTime = DateTime.Now,
                Category = "system",
                Key = "token",
                Value = DateTime.Now.ToString("yyyyMMddHHmmss"),
                Describe = "token值",
                LastUpdateTime = DateTime.Now,
                Sort = 0,
            });

        }
        public static void CreateMenuPass(SqlHelper helper)
        {
            List<DbAdminMenuPass> passList = new List<DbAdminMenuPass>();
            var menus = helper.Select<DbAdminMenu>();

            foreach (var item in menus) {
                var acs = item.Actions.Split('|');
                foreach (var ac in acs) {
                    passList.Add(new DbAdminMenuPass {
                        AdminGroupId = 1,
                        MenuId = item.Id,
                        ActionName = ac,
                        Code = item.Code
                    });
                }
            }
            helper.InsertList(passList);
        }


        public static string CreatePassword(string user, string password, bool isMd5 = false)
        {
            if (isMd5) {
                return Hash.GetMd5String(user + "|ToolGood|" + password);
            }
            return Hash.GetMd5String(user + "|ToolGood|" + Hash.GetMd5String(password));
        }

        #endregion
    }
}
