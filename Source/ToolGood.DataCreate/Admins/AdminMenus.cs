/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Datas;

namespace ToolGood.DataCreate
{
    class AdminMenus
    {
        public static void CreateMenu(SqlHelper helper)
        {
            var menuIndex = 1;
            var adminDesktop = new DbSysAdminMenu() { MenuName = "基础配置", MenuCode = "AdminTopDesktop", Url = "", Buttons = "navbar|show", ParentId = 0, OrderNum = (menuIndex++), };
            helper.Insert(adminDesktop);
            var toolsDesktop = new DbSysAdminMenu() { MenuName = "功能配置", MenuCode = "ToolsDesktop", Url = "", Buttons = "navbar|show", ParentId = 0, OrderNum = (menuIndex++), };
            helper.Insert(toolsDesktop);
            var sqlDesktop = new DbSysAdminMenu() { MenuName = "SQL online", MenuCode = "SqlOnlineDesktop", Url = "/Sqls", Buttons = "navbar|show", ParentId = 0, OrderNum = (menuIndex++), };
            helper.Insert(sqlDesktop);
            var developmentDesktop = new DbSysAdminMenu() { MenuName = "开发环境", MenuCode = "DevelopmentDesktop", Url = "/Developments", Buttons = "navbar|show", ParentId = 0, OrderNum = 999999, };
            helper.Insert(developmentDesktop);


            {
                var topdesktop = adminDesktop;

                var desktop = new DbSysAdminMenu() { MenuName = "数据库管理", MenuCode = "DatabaseDesktop", Buttons = "navbar|show", ParentId = topdesktop.Id, OrderNum = (menuIndex++), };
                helper.Insert(desktop);
                {
                    helper.Insert(new DbSysAdminMenu() { MenuName = "数据库连接", MenuCode = "DatabaseConn", Url = "/Admins/Database/ConnList", Buttons = "navbar|show|edit|delete", ParentId = desktop.Id, OrderNum = (menuIndex++), });
                    helper.Insert(new DbSysAdminMenu() { MenuName = "数据库权限", MenuCode = "DatabasePower", Url = "/Admins/Database/PowerList", Buttons = "navbar|show|edit|delete", ParentId = desktop.Id, OrderNum = (menuIndex++), });
                    helper.Insert(new DbSysAdminMenu() { MenuName = "查询日志配置", MenuCode = "DatabaseLog", Url = "/Admins/Database/LogSetting", Buttons = "navbar|show|edit|delete", ParentId = desktop.Id, OrderNum = (menuIndex++), });
                }
                desktop = new DbSysAdminMenu() { MenuName = "用户数据", MenuCode = "DatabaseDesktop", Buttons = "navbar|show", ParentId = topdesktop.Id, OrderNum = (menuIndex++), };
                helper.Insert(desktop);
                {
                    helper.Insert(new DbSysAdminMenu() { MenuName = "SQL文档", MenuCode = "SqlDoc", Url = "/Admins/SqlDoc/List", Buttons = "navbar|show|edit|delete", ParentId = desktop.Id, OrderNum = (menuIndex++), });
                    helper.Insert(new DbSysAdminMenu() { MenuName = "SQL临时文档", MenuCode = "SqlDocTemp", Url = "/Admins/SqlDoc/TempList", Buttons = "navbar|show|edit|delete", ParentId = desktop.Id, OrderNum = (menuIndex++), });
                }

                desktop = new DbSysAdminMenu() { MenuName = "系统管理", MenuCode = "AdminDesktop", Buttons = "navbar|show", ParentId = topdesktop.Id, OrderNum = (menuIndex++), };
                helper.Insert(desktop);
                {
                    helper.Insert(new DbSysAdminMenu() { MenuName = "管理员列表", MenuCode = "Admin", Url = "/Admins/Admin/List", Buttons = "navbar|show|edit|delete|password", ParentId = desktop.Id, OrderNum = (menuIndex++), });
                    helper.Insert(new DbSysAdminMenu() { MenuName = "角色列表", MenuCode = "AdminGroup", Url = "/Admins/AdminGroup/List", Buttons = "navbar|show|edit|delete", ParentId = desktop.Id, OrderNum = (menuIndex++), });
                    helper.Insert(new DbSysAdminMenu() { MenuName = "机器码列表", MenuCode = "MachineCode", Url = "/Admins/MachineCode/List", Buttons = "navbar|show|edit|delete", ParentId = desktop.Id, OrderNum = (menuIndex++), });
                    helper.Insert(new DbSysAdminMenu() { MenuName = "发件邮箱配置", MenuCode = "SendEmail", Url = "/Admins/Emails/SendEmail", Buttons = "navbar|show|edit|delete", ParentId = desktop.Id, OrderNum = (menuIndex++), });
                }

                desktop = new DbSysAdminMenu() { MenuName = "安全中心", MenuCode = "SecurityDesktop", Buttons = "navbar|show", ParentId = topdesktop.Id, OrderNum = (menuIndex++), };
                helper.Insert(desktop);
                {
                    helper.Insert(new DbSysAdminMenu() { MenuName = "安全配置", MenuCode = "SecuritySetting", Url = "/Admins/Securitys/SecuritySetting", Buttons = "navbar|show|edit", ParentId = desktop.Id, OrderNum = (menuIndex++), });
                    helper.Insert(new DbSysAdminMenu() { MenuName = "菜单管理模式", MenuCode = "AdminMenu", Url = "/Admins/Securitys/MenuList", Buttons = "navbar|show|edit|delete", ParentId = desktop.Id, OrderNum = (menuIndex++), });


                    helper.Insert(new DbSysAdminMenu() { MenuName = "IP黑名单", MenuCode = "IpBanlist", Url = "/Admins/Securitys/IpBanlist", Buttons = "navbar|show|edit|delete", ParentId = desktop.Id, OrderNum = (menuIndex++), });
                    helper.Insert(new DbSysAdminMenu() { MenuName = "IP白名单", MenuCode = "IpAllowlist", Url = "/Admins/Securitys/IpAllowlist", Buttons = "navbar|show|edit|delete", ParentId = desktop.Id, OrderNum = (menuIndex++), });

                    helper.Insert(new DbSysAdminMenu() { MenuName = "登录日志", MenuCode = "AdminLoginLog", Url = "/Admins/Securitys/LoginList", Buttons = "navbar|show", ParentId = desktop.Id.ToSafeInt32(0), OrderNum = (menuIndex++), });
                }

            }
            {
                var topdesktop = toolsDesktop;
                var desktop = new DbSysAdminMenu() { MenuName = "代码生成管理", MenuCode = "CodeGensDesktop", Buttons = "navbar|show", ParentId = topdesktop.Id, OrderNum = (menuIndex++), };
                helper.Insert(desktop);
                {
                    helper.Insert(new DbSysAdminMenu() { MenuName = "数据库模板", MenuCode = "TableTpl", Url = "/Admins/CodeGens/TableTplList", Buttons = "navbar|show|edit|delete", ParentId = desktop.Id, OrderNum = (menuIndex++), });
                    helper.Insert(new DbSysAdminMenu() { MenuName = "存储过程模板", MenuCode = "ProcedureTpl", Url = "/Admins/CodeGens/ProcedureTplList", Buttons = "navbar|show|edit|delete", ParentId = desktop.Id, OrderNum = (menuIndex++), });
                }
            }



            #region DbSysAdminMenuButton
            {
                var index = 1;
                var t = "navbar=导航栏&Show=显示&edit=编辑&delete=删除&run=执行&password=初始密码&databasepower=数据库权限";


                //"&Start=启动&ReStart=重启&Pause=暂停&Stop=停止&Close=关闭" +
                //"&Audit=审核&Revoke=撤销&Invalid=作废&Lock=锁定&UnLock=解锁&redo=重做&Pass=通过&notPass=不通过" +
                //"&Message=短信&search=搜索" +
                //"&Copy=复制&Replace=替换&Build=生成&Print=打印" +
                //"&Instal=安装&UnLoad=卸载&Backup=备份&Restore=还原&export=导出&Import=导入" +
                //"&Authorize=授权&Online=上线&Offline=下线";
                var ts = t.ToLower().Split('&');
                foreach (var s in ts) {
                    var sp = s.Split('=');
                    helper.Insert(new DbSysAdminMenuButton() {
                        ButtonCode = sp[0].ToLower(),
                        ButtonName = sp[1],
                        OrderNum = (index++),
                    });
                }
            }
            #endregion
        }

    }
}
