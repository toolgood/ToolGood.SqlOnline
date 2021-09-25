/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Linq;
using ToolGood.Common.Utils;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Datas;

namespace ToolGood.DataCreate
{
    class AdminDatas
    {
        public static void CreateData(SqlHelper helper)
        {
            var salt = Guid.NewGuid().ToString().Replace("-", "");


            #region DbSysAdminGroup
            helper.Insert(new DbSysAdminGroup() {
                Name = "超级管理员",
                Comment = "",
                OrderNum = 1,
                IsSystem = true
            });
            helper.Insert(new DbSysAdminGroup() {
                Name = "开发人员",
                Comment = "",
                OrderNum = 1,
                IsSystem = false
            });
            helper.Insert(new DbSysAdminGroup() {
                Name = "报表开发人员",
                Comment = "",
                OrderNum = 1,
                IsSystem = false
            });
            helper.Insert(new DbSysAdminGroup() {
                Name = "测试人员",
                Comment = "",
                OrderNum = 1,
                IsSystem = false
            });
            helper.Insert(new DbSysAdminGroup() {
                Name = "运维人员",
                Comment = "",
                OrderNum = 1,
                IsSystem = false
            });
            helper.Insert(new DbSysAdminGroup() {
                Name = "运营人员",
                Comment = "",
                OrderNum = 1,
                IsSystem = false
            });
            #endregion

            #region DbSysAdmin
            helper.Insert(new DbSysAdmin() {
                Name = "admin",
                Salt = salt,
                Password = DataUtil.CreatePassword("admin", salt, "a123456"),
                ManagerPassword = DataUtil.CreatePassword("admin", salt, "a123456789"),
                TrueName = "管理员",
            });

            helper.Insert(new DbSysAdmin() {
                Name = "test001",
                Salt = salt,
                Password = DataUtil.CreatePassword("test001", salt, "a123456"),
                ManagerPassword = DataUtil.CreatePassword("test001", salt, "a123456789"),
                TrueName = "测试人员1",
            });

            helper.Insert(new DbSysAdmin() {
                Name = "test002",
                Salt = salt,
                Password = DataUtil.CreatePassword("test002", salt, "a123456"),
                ManagerPassword = DataUtil.CreatePassword("test002", salt, "a123456789"),
                TrueName = "测试人员2",
            });
            helper.Insert(new DbSysAdmin() {
                Name = "test003",
                Salt = salt,
                Password = DataUtil.CreatePassword("test003", salt, "a123456"),
                ManagerPassword = DataUtil.CreatePassword("test003", salt, "a123456789"),
                TrueName = "测试人员3",
            });

            helper.Insert(new DbSysAdmin() {
                Name = "test004",
                Salt = salt,
                Password = DataUtil.CreatePassword("test004", salt, "a123456"),
                ManagerPassword = DataUtil.CreatePassword("test004", salt, "a123456789"),
                TrueName = "测试人员4",
            });

            #endregion

            #region DbSysAdmin_Group
            helper.Insert(new DbSysAdmin_Group { AdminId = 1, GroupId = 1 });
            helper.Insert(new DbSysAdmin_Group { AdminId = 2, GroupId = 4 });
            helper.Insert(new DbSysAdmin_Group { AdminId = 3, GroupId = 4 });
            helper.Insert(new DbSysAdmin_Group { AdminId = 4, GroupId = 4 });
            helper.Insert(new DbSysAdmin_Group { AdminId = 5, GroupId = 4 });
            #endregion

            #region DbSysAdminMenu.ParentIds
            {
                var menus = helper.Select<DbSysAdminMenu>();
                for (int i = 0; i < menus.Count; i++) {
                    var menu = menus[i];
                    if (menu.ParentId == 0) {
                        menu.ParentIds = "-0-";// + menu.Id + "-";
                    } else {
                        var pmenu = menus.Where(q => q.Id == menu.ParentId).First();
                        menu.ParentIds = pmenu.ParentIds + menu.ParentId + "-";
                    }
                    menu.Buttons = menu.Buttons.ToLower();
                    helper.Save(menu);
                }
            }
            #endregion

            #region AdminMenuPass
            {
                var menus = helper.Select<DbSysAdminMenu>();
                var btns = helper.Select<DbSysAdminMenuButton>();

                foreach (var item in menus) {
                    var acs = item.Buttons.Split('|');
                    foreach (var ac in acs) {
                        helper.Insert(new DbSysAdminGroup_Menu() {
                            GroupId = 1,
                            MenuId = item.Id,
                            MenuCode = item.MenuCode,
                            ButtonCode = ac,
                            ButtonId = btns.Where(q => q.ButtonCode == ac.ToLower()).First().Id
                        });
                    }
                }
                // 默认 安全中心，安全模式，设置初始密码
                // 设置邮箱 邮箱验证
            }
            {
                helper.Insert(new DbSysAdminMenuCheck() {
                    AddingAdminId = 0,
                    AddingTime = DateTime.Now,
                    MenuCode = "Admin",
                    ButtonCode = "password",
                });
                helper.Insert(new DbSysAdminMenuCheck() {
                    AddingAdminId = 0,
                    AddingTime = DateTime.Now,
                    MenuCode = "SendEmail",
                    ButtonCode = "show",
                });
                helper.Insert(new DbSysAdminMenuCheck() {
                    AddingAdminId = 0,
                    AddingTime = DateTime.Now,
                    MenuCode = "SecuritySetting",
                    ButtonCode = "show",
                });
                helper.Insert(new DbSysAdminMenuCheck() {
                    AddingAdminId = 0,
                    AddingTime = DateTime.Now,
                    MenuCode = "AdminMenu",
                    ButtonCode = "edit",
                });

                helper.Execute("update SysAdminMenuCheck set MenuId=(select Id from SysAdminMenu as m where m.MenuCode=SysAdminMenuCheck.MenuCode  ) ");

            }

            #endregion

            #region 安全中心
            helper.Insert(new DbSysSettingValue() {
                CategoryName = "数据",
                Code = "DatabaseVersion",
                SettingName = "数据库版本",
                Value = "2021-09-25",
                Comment = "",
                ModifyTime = DateTime.Now,
                ModifyAdminId = 0,
            });

            helper.Insert(new DbSysSettingValue() {
                CategoryName = "后台",
                Code = "UseDevelopment",
                SettingName = "开发环境",
                Value = "0",
                Comment = "（0）不使用，（1）使用开发环境",
                ModifyTime = DateTime.Now,
                ModifyAdminId = 0,
            });
            helper.Insert(new DbSysSettingValue() {
                CategoryName = "后台",
                Code = "IpFilterType",
                SettingName = "IP地址过滤类型",
                Value = "0",
                Comment = "（0）不使用，（1）使用白名单，（2）使用黑名单",
                ModifyTime = DateTime.Now,
                ModifyAdminId = 0,
            });
            helper.Insert(new DbSysSettingValue() {
                CategoryName = "后台",
                Code = "Logo",
                SettingName = "Logo文本",
                Value = "SQL ONLINE",
                Comment = "正上角LOGO文字",
                ModifyTime = DateTime.Now,
                ModifyAdminId = 0,
            });

            helper.Insert(new DbSysSettingValue() {
                CategoryName = "账号",
                Code = "LoginPassword",
                SettingName = "默认登录密码",
                Value = "a123456",
                Comment = "密码长度必须大于3位，小于20位",
                ModifyTime = DateTime.Now,
                ModifyAdminId = 0,
            });
            helper.Insert(new DbSysSettingValue() {
                CategoryName = "账号",
                Code = "ManagerPassword",
                SettingName = "默认管理密码",
                Value = "a123456789",
                Comment = "密码长度必须大于3位，小于20位",
                ModifyTime = DateTime.Now,
                ModifyAdminId = 0,
            });
             
            helper.Insert(new DbSysSettingValue() {
                CategoryName = "账号",
                Code = "CookieTimes",
                SettingName = "Cookie保存时间",
                Value = "10800",
                Comment = "数字，单位：秒,最小10分钟，默认180分钟",
                ModifyTime = DateTime.Now,
                ModifyAdminId = 0,
            });

            helper.Insert(new DbSysSettingValue() {
                CategoryName = "水印",
                Code = "UseWatermark",
                SettingName = "使用水印",
                Value = "1",
                Comment = "功能：（0）不启用，（1）启用",
                ModifyTime = DateTime.Now,
                ModifyAdminId = 0,
            });
            helper.Insert(new DbSysSettingValue() {
                CategoryName = "水印",
                Code = "WatermarkText",
                SettingName = "水印格式",
                Value = "{trueName} {yyyy}-{MM}-{dd}",
                Comment = "{name}账号名称，{trueName}真实名称，{jobNo}工号，{yyyy}{yy}年，{MM}{M}月，{dd}日，{HH}{H}时,{mm}{m}分，{ss}{s}秒",
                ModifyTime = DateTime.Now,
                ModifyAdminId = 0,
            });

            helper.Insert(new DbSysSettingValue() {
                CategoryName = "安全",
                Code = "UseMachineCode",
                SettingName = "使用机器码",
                Value = "0",
                Comment = "0为不使用，1使用",
                ModifyTime = DateTime.Now,
                ModifyAdminId = 0,
            });
            helper.Insert(new DbSysSettingValue() {
                CategoryName = "安全",
                Code = "FirstLoginUseMachineCode",
                SettingName = "第一次登录记录机器码",
                Value = "0",
                Comment = "0为不使用，1使用",
                ModifyTime = DateTime.Now,
                ModifyAdminId = 0,
            });

            #endregion

            #region 发件邮箱
            helper.Insert(new DbSysSettingValue() {
                CategoryName = "发件邮箱",
                Code = "EmailSendHost",
                SettingName = "域名",
                Value = "",
                Comment = "发件邮箱的域名",
                ModifyTime = DateTime.Now,
                ModifyAdminId = 0,
            });
            helper.Insert(new DbSysSettingValue() {
                CategoryName = "发件邮箱",
                Code = "EmailSendPort",
                SettingName = "端口号",
                Value = "",
                Comment = "端口号，数字",
                ModifyTime = DateTime.Now,
                ModifyAdminId = 0,
            });
            helper.Insert(new DbSysSettingValue() {
                CategoryName = "发件邮箱",
                Code = "EmailSendSSL",
                SettingName = "是否使用加密",
                Value = "0",
                Comment = "数字，1开启，0禁用",
                ModifyTime = DateTime.Now,
                ModifyAdminId = 0,
            });
            helper.Insert(new DbSysSettingValue() {
                CategoryName = "发件邮箱",
                Code = "EmailSendUser",
                SettingName = "账号",
                Value = "",
                Comment = "发件邮箱的账号",
                ModifyTime = DateTime.Now,
                ModifyAdminId = 0,
            });
            helper.Insert(new DbSysSettingValue() {
                CategoryName = "发件邮箱",
                Code = "EmailSendPassword",
                SettingName = "密码",
                Value = "",
                Comment = "发件邮箱的密码",
                ModifyTime = DateTime.Now,
                ModifyAdminId = 0,
            });
            helper.Insert(new DbSysSettingValue() {
                CategoryName = "发件邮箱",
                Code = "EmailSendCount",
                SettingName = "尝试次数",
                Value = "0",
                Comment = "发件失败，重新发邮件尝试次数",
                ModifyTime = DateTime.Now,
                ModifyAdminId = 0,
            });
            helper.Insert(new DbSysSettingValue() {
                CategoryName = "发件邮箱",
                Code = "EmailSendInterval",
                SettingName = "间隔秒数",
                Value = "5",
                Comment = "数字，如设置多次间隔，使用逗号分隔。默认5秒。",
                ModifyTime = DateTime.Now,
                ModifyAdminId = 0,
            });

            #endregion


        }
    }
}
