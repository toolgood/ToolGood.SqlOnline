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
    public class MenuController : AdminBaseController
    {
        private readonly IAdminMenuApplication _adminMenuApplication;

        public MenuController(IAdminMenuApplication adminMenuApplication)
        {
            _adminMenuApplication = adminMenuApplication;
        }

        [HttpGet]
        [AdminAuth("Admin-Menu", ActionPermissionType.Show)]
        public IActionResult Index()
        {
            var menus = _adminMenuApplication.GetAdminMenuAll().Value;
            ViewData["menus"] = menus;
            return View();
        }

        [HttpGet]
        [AdminAuth("Admin-Menu", ActionPermissionType.Add)]
        public IActionResult Add()
        {
            var r = _adminMenuApplication.GetParentAdminMenu().Value;
            ViewData["menus"] = r;
            return View();
        }

        [HttpPost]
        [AdminAuth("Admin-Menu", ActionPermissionType.Add)]
        public IActionResult Add(int pid, string name, string code, string url, string actions, int sort)
        {
            if (string.IsNullOrEmpty(name)) {
                return Error("请输入栏目名称！");
            }
            if (name.Length > 20) {
                return Error("栏目名称过长！");
            }
            if (string.IsNullOrEmpty(code)) {
                return Error("请输入Code！");
            }
            if (code.Length > 40) {
                return Error("Code过长！");
            }
            if (string.IsNullOrEmpty(actions)) {
                return Error("请选择操作！");
            }
            if (actions.Length > 100) {
                return Error("操作过长！");
            }
            var r = _adminMenuApplication.AddMenu(pid, name, code, url, actions, sort);
            if (r.IsSuccess) {
                return Success();
            }
            return Error(r.Message);
        }

        [HttpGet]
        [AdminAuth("Admin-Menu", ActionPermissionType.Edit)]
        public IActionResult Edit(int id)
        {
            var r = _adminMenuApplication.GetParentAdminMenu().Value;
            ViewData["menus"] = r;
            var m = _adminMenuApplication.GetMenuById(id).Value;
            ViewData["menu"] = m;
            return View();
        }

        [HttpPost]
        [AdminAuth("Admin-Menu", ActionPermissionType.Edit)]
        public IActionResult Edit(int id, int pid, string name, string code, string url, string actions, int sort)
        {
            if (string.IsNullOrEmpty(name)) {
                return Error("请输入栏目名称！");
            }
            if (name.Length > 20) {
                return Error("栏目名称过长！");
            }
            if (string.IsNullOrEmpty(code)) {
                return Error("请输入Code！");
            }
            if (code.Length > 40) {
                return Error("Code过长！");
            }
            if (string.IsNullOrEmpty(actions)) {
                return Error("请选择操作！");
            }
            if (actions.Length > 100) {
                return Error("操作过长！");
            }
            var r = _adminMenuApplication.EditMenu(id, pid, name, code, url, actions, sort);

            if (r.IsSuccess) {
                return Success();
            }
            return Error(r.Message);
        }

        [HttpPost]
        [AdminAuth("Admin-Menu", ActionPermissionType.Delete)]
        public IActionResult Delete(int id)
        {
            var r = _adminMenuApplication.DeleteMenu(id);
            if (r.IsSuccess) {
                return Success();
            }
            return Error(r.Message);
        }


        [HttpPost]
        [AdminAuth("Admin-Menu", ActionPermissionType.Edit)]
        public IActionResult AdminMenuChangeSort(int[] ids, int[] sorts)
        {
            if (ids==null || sorts==null || ids.Length==0 || ids.Length!=sorts.Length) {
                return Error("菜单个数不正确");
            }
            var r = _adminMenuApplication.MenuChangeSort(ids,sorts);
            if (r.IsSuccess) {
                return Success();
            }
            return Error(r.Message);
        }


    }
}