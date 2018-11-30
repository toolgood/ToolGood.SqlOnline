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
    public class GroupController : AdminBaseController
    {
        private readonly IAdminApplication _adminApplication;
        private readonly IAdminMenuApplication _adminMenuApplication;
        private readonly IDatabaseInfoApplication _databaseInfoApplication;

        public GroupController(IAdminApplication adminApplication, IAdminMenuApplication adminMenuApplication, IDatabaseInfoApplication databaseInfoApplication)
        {
            _adminApplication = adminApplication;
            _adminMenuApplication = adminMenuApplication;
            _databaseInfoApplication = databaseInfoApplication;
        }

        [HttpGet]
        [AdminAuth("Admin-Group", ActionPermissionType.Show)]
        public IActionResult Index()
        {
            var groups = _adminApplication.GetAdminGroupAll().Value;
            ViewData["groups"] = groups;
            return View();
        }

        [HttpGet]
        [AdminAuth("Admin-Group", ActionPermissionType.Add)]
        public IActionResult Add()
        {
            ViewData["AdminMenu"] = _adminMenuApplication.GetAdminMenuAll().Value;
            ViewData["Database"] = _databaseInfoApplication.GetNameAndIdOfDatabaseInfo().Value;
            return View();
        }

        [HttpPost]
        [AdminAuth("Admin-Group", ActionPermissionType.Add)]
        public IActionResult Add(string name, string describe, int sort, string[] ans, int[] databases)
        {
            if (string.IsNullOrEmpty(name)) {
                return Error("请输入角色名！");
            }
            if (name.Length > 20) {
                return Error("角色名过长！");
            }
            if (describe != null && describe.Length > 200) {
                return Error("描述过长！");
            }
            var r = _adminApplication.AddAdminGroup(name, describe, sort, ans);
            if (r.IsSuccess) {
                _adminApplication.SetDatabasePass(r.Value, databases);
                ActionCheck.Update();
                return Success();
            }
            return Error(r.Message);
        }


        [HttpGet]
        [AdminAuth("Admin-Group", ActionPermissionType.Edit)]
        public IActionResult Edit(int id)
        {
            ViewData["AdminMenu"] = _adminMenuApplication.GetAdminMenuAll().Value;
            var group = _adminApplication.GetAdminGroupById(id).Value;
            ViewData["AdminGroup"] = group;
            var pass = _adminApplication.GetAdminMenuPassListByGroupId(id).Value;
            var permissionAll = new ActionPermissionDictionary();
            foreach (var item in pass) {
                permissionAll.SetAllow(item.Code, item.ActionName);
            }
            ViewData["ActionAuth"] = permissionAll;
            ViewData["Database"] = _databaseInfoApplication.GetNameAndIdOfDatabaseInfo().Value;
            ViewData["DatabasePass"] = _adminApplication.GetDatabasePass(id).Value;

            return View();
        }

        [HttpPost]
        [AdminAuth("Admin-Group", ActionPermissionType.Edit)]
        public IActionResult Edit(int id, string name, string describe, int sort, string[] ans, int[] databases)
        {
            if (string.IsNullOrEmpty(name)) {
                return Error("请输入角色名！");
            }
            if (name.Length > 20) {
                return Error("角色名过长！");
            }
            if (describe != null && describe.Length > 200) {
                return Error("描述过长！");
            }
            var r = _adminApplication.EditAdminGroup(id, name, describe, sort, ans);
            if (r.IsSuccess) {
                _adminApplication.SetDatabasePass(id, databases);
                ActionCheck.Update();
                return Success();
            }
            return Error(r.Message);
        }


        [HttpPost]
        [AdminAuth("Admin-Group", ActionPermissionType.Delete)]
        public IActionResult Delete(int id)
        {
            if (id == 1) {
                return Error("无法删除此角色");
            }

            var r = _adminApplication.DeleteAdminGroup(id);
            if (r.IsSuccess) {
                return Success();
            }
            return Error(r.Message);
        }

    }
}