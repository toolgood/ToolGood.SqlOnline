using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.Application;
using ToolGood.ApplicationCommon;
using ToolGood.Infrastructure;

namespace ToolGood.Server.Areas.Admin.Controllers
{
    [Area(KeywordString.AdminArea)]
    public class HomeController : AdminBaseController
    {
        private readonly IAdminApplication _adminApplication;
        private readonly IAdminMenuApplication _adminMenuApplication;

        public HomeController(IAdminApplication adminApplication, IAdminMenuApplication adminMenuApplication)
        {
            _adminApplication = adminApplication;
            _adminMenuApplication = adminMenuApplication;
        }

        [HttpGet]
        public IActionResult Index(int mid = 0)
        {
            var topMenus = _adminMenuApplication.GetTopAdminMenu(Admin.GroupId, mid).Value;
            var topMenu = topMenus.FirstOrDefault(q => q.Activity);
            if (topMenu!=null && string.IsNullOrEmpty(topMenu.Url)==false) {
                return Redirect(topMenu.Url);
            }
            int topId = topMenu?.Id ?? 0;
            var treeMenu = _adminMenuApplication.GetAdminMenu(Admin.GroupId, topId).Value;

            ViewData["topMenu"] = topMenus;
            ViewData["treeMenu"] = treeMenu;

            return View();
        }

        [HttpGet]
        public IActionResult HomeIndex()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string oldPassword, string newPassword, string newPassword2)
        {
            if (newPassword != newPassword2) {
                return Error("新密码不一致！");
            }
            if (string.IsNullOrEmpty(oldPassword)) {
                return Error("旧密码不能为空！");
            }
            if (string.IsNullOrEmpty(newPassword)) {
                return Error("新密码不能为空！");
            }
            if (newPassword.Length < 4) {
                return Error("新密码长度不能小于4！");
            }
            var result = _adminApplication.ChangePassword(Admin.Name, oldPassword, newPassword);
            if (result.IsSuccess) {
                return Success("密码修改成功！");
            }
            return Error("旧密码不一致！");
        }
    }
}