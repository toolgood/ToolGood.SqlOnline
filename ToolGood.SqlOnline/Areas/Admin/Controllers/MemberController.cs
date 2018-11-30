using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.Application;
using ToolGood.ApplicationCommon;
using ToolGood.Infrastructure;

namespace ToolGood.Server.Areas.Admin.Controllers
{
    [Area(KeywordString.AdminArea)]
    public class MemberController : AdminBaseController
    {
        private readonly IAdminApplication _adminApplication;
        private readonly IAdminMenuApplication _adminMenuApplication;

        public MemberController(IAdminApplication adminApplication, IAdminMenuApplication adminMenuApplication)
        {
            _adminApplication = adminApplication;
            _adminMenuApplication = adminMenuApplication;
        }

        [HttpGet]
        [AdminAuth("Admin-Member", ActionPermissionType.Show)]
        public IActionResult Index()
        {
            ViewData["adminList"] = _adminApplication.GetAdminAll().Value;
            return View();
        }

        [HttpGet]
        [AdminAuth("Admin-Member", ActionPermissionType.Add)]
        public IActionResult Add()
        {
            ViewData["groups"] = _adminApplication.GetAdminGroupAll().Value;
            return View();
        }

        [HttpPost]
        [AdminAuth("Admin-Member", ActionPermissionType.Add)]
        public IActionResult Add(string name, string trueName, string password, int adminGroupID)
        {
            if (string.IsNullOrEmpty(name)) {
                return Error("用户名不能为空！");
            }
            if (name.Length < 4 || name.Length > 20) {
                return Error("用户名长度在4到20之间！");
            }
            if (Regex.IsMatch(name, "^[a-zA-Z0-9_]+$")==false) {
                return Error("用户名要为英文、数字、下划线！");
            }
            if (string.IsNullOrEmpty(trueName)) {
                return Error("真实名字不能为空！");
            }
            if (trueName.Length > 20) {
                return Error("真实名字长度小于20！");
            }
            if (string.IsNullOrEmpty(password)) {
                return Error("密码不能为空！");
            }
            if (password.Length < 4) {
                return Error("密码长度不能小于4！");
            }

            var r = _adminApplication.AddAdmin(name, trueName, password, adminGroupID);
            if (r.IsSuccess) {
                return Success();
            }
            return Error(r.Message);
        }

        [HttpGet]
        [AdminAuth("Admin-Member", ActionPermissionType.Edit)]
        public IActionResult Edit(int id)
        {
            ViewData["groups"] = _adminApplication.GetAdminGroupAll().Value;
            ViewData["admin"] = _adminApplication.GetAdminById(id).Value;
            return View();
        }

        [HttpPost]
        [AdminAuth("Admin-Member", ActionPermissionType.Edit)]
        public IActionResult Edit(int id, string trueName, int adminGroupID)
        {
            if (string.IsNullOrEmpty(trueName)) {
                return Error("真实名字不能为空！");
            }
            if (trueName.Length > 20) {
                return Error("真实名字长度小于20！");
            }

            var r = _adminApplication.EditAdmin(id, trueName, adminGroupID);
            if (r.IsSuccess) {
                return Success();
            }
            return Error(r.Message);
        }

        [HttpPost]
        [AdminAuth("Admin-Member", ActionPermissionType.Delete)]
        public IActionResult Delete(int id)
        {
            if (id == 1) {
                return Error("无法删除此账号");
            }
            var r = _adminApplication.DeleteAdmin(id);
            if (r.IsSuccess) {
                return Success();
            }
            return Error(r.Message);
        }

        [HttpGet]
        [AdminAuth("Admin-Member", ActionPermissionType.ChangePassword)]
        public IActionResult AdminChangePassword(int id)
        {
            ViewData["admin"] = _adminApplication.GetAdminById(id).Value;
            return View();
        }

        [HttpPost]
        [AdminAuth("Admin-Member", ActionPermissionType.ChangePassword)]
        public IActionResult AdminChangePassword(int id, string password)
        {
            if (string.IsNullOrEmpty(password)) {
                return Error("密码不能为空！");
            }
            if (password.Length < 4) {
                return Error("密码长度不能小于4！");
            }
            var r = _adminApplication.ChangePassword(id, password);
            if (r.IsSuccess) {
                return Success();
            }
            return Error(r.Message);
        }


    }
}